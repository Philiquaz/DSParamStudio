using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Numerics;
using ImGuiNET;
using SoulsFormats;
using System.Xml;

namespace StudioCore.MsbEditor
{
    public class ParamMetaData
    {
        public static XmlNode GetXmlNode(XmlDocument xml, XmlNode parent, string child)
        {
            XmlNode node = parent.SelectSingleNode(child);
            if (node == null)
                node = parent.AppendChild(xml.CreateElement("child"));
            return node;
        }
        public static XmlAttribute GetXmlAttribute(XmlDocument xml, XmlNode node, string name)
        {
            XmlAttribute attribute = node.Attributes[name];
            if (attribute == null)
                attribute = node.Attributes.Append(xml.CreateAttribute(name));
            return attribute;
        }
        public static XmlAttribute GetXmlProperty(string property, XmlDocument xml, params string[] path)
        {
            XmlNode currentNode = xml;
            foreach(string s in path)
                currentNode = GetXmlNode(xml, currentNode, s);
            return GetXmlAttribute(xml, currentNode, property);
        }

        private static Dictionary<PARAMDEF, ParamMetaData> _ParamMetas = new Dictionary<PARAMDEF, ParamMetaData>();
        
        internal Dictionary<string, ParamEnum> enums = new Dictionary<string, ParamEnum>();

        private const int XML_VERSION = 0;
        internal XmlDocument _xml;
        private string _path;

        /// <summary>
        /// Max value of trailing digits used for offset, +1
        /// </summary>
        public int OffsetSize {get; set;}
        
        /// <summary>
        /// Whether row 0 is a dummy to be ignored
        /// </summary>
        public bool Row0Dummy {get; set;}

        /// <summary>
        /// Provides a reordering of fields for display purposes only
        /// </summary>
        public List<string> AlternateOrder {get; set;}

        public static ParamMetaData Get(PARAMDEF def)
        {
            return _ParamMetas[def];
        }

        private static void Add(PARAMDEF key, ParamMetaData meta)
        {
            _ParamMetas.Add(key, meta);
        }

        private ParamMetaData(PARAMDEF def)
        {
            Add(def, this);
            foreach (PARAMDEF.Field f in def.Fields)
                new FieldMetaData(f);
            // Blank Metadata
        }

        private ParamMetaData(XmlDocument xml, string path, PARAMDEF def)
        {
            _xml = xml;
            _path = path;
            XmlNode root = xml.SelectSingleNode("PARAMMETA");
            int xmlVersion = int.Parse(root.Attributes["XmlVersion"].InnerText);
            if (xmlVersion != XML_VERSION)
            {
                throw new InvalidDataException($"Mismatched XML version; current version: {XML_VERSION}, file version: {xmlVersion}");
            }
            Add(def, this);
            
            XmlNode self = root.SelectSingleNode("Self");
            if (self != null)
            {
                XmlAttribute Off = self.Attributes["OffsetSize"];
                if (Off != null)
                {
                    OffsetSize = int.Parse(Off.InnerText);
                }
                XmlAttribute R0 = self.Attributes["Row0Dummy"];
                if (R0 != null)
                {
                    Row0Dummy = true;
                }
                XmlAttribute AltOrd = self.Attributes["AlternativeOrder"];
                if (AltOrd != null)
                {
                    AlternateOrder = new List<string>(AltOrd.InnerText.Split(',', StringSplitOptions.RemoveEmptyEntries));
                    for (int i = 0; i < AlternateOrder.Count; i++)
                        AlternateOrder[i] = AlternateOrder[i].Trim();
                }
            }

            foreach (XmlNode node in root.SelectNodes("Enums/Enum"))
            {
                ParamEnum en = new ParamEnum(node);
                enums.Add(en.name, en);
            }
 
            Dictionary<string, int> nameCount = new Dictionary<string, int>();
            foreach (PARAMDEF.Field f in def.Fields)
            {
                try
                {
                    string name = FixName(f.InternalName);
                    int c = nameCount.GetValueOrDefault(name, 0);
                    XmlNodeList nodes = root.SelectNodes($"Field/{name}");
                    //XmlNode pairedNode = root.SelectSingleNode($"Field/{}");
                    XmlNode pairedNode = nodes[c];
                    nameCount[name] = c + 1;

                    if (pairedNode == null)
                    {
                        new FieldMetaData(f);
                        continue;
                    }
                    new FieldMetaData(this, pairedNode, f);
                }
                catch
                {
                    new FieldMetaData(f);
                }
            }
        }

        public void Commit()
        {
            if (OffsetSize != 0)
                GetXmlProperty("OffsetSize", _xml, "PARAMMETA", "Self").InnerText = OffsetSize.ToString(); //doesn't commit return to 0
            if (Row0Dummy)
                GetXmlProperty("Row0Dummy", _xml, "PARAMMETA", "Self").InnerText = ""; //doesn't commit un-dummying
            if (AlternateOrder != null)
                GetXmlProperty("AlternativeOrder", _xml, "PARAMMETA", "Self").InnerText = String.Join(',', AlternateOrder).Replace("-,", "-,\n"); //small cleanliness detail
        }

        public void Save()
        {
            if(_xml == null)
                return;
            XmlWriterSettings writeSettings = new XmlWriterSettings();
            writeSettings.Indent = true;
            writeSettings.NewLineHandling = NewLineHandling.None;
            _xml.Save(XmlWriter.Create(_path, writeSettings));
        }

        public static void SaveAll()
        {
            foreach(KeyValuePair<PARAMDEF.Field, FieldMetaData> field in FieldMetaData._FieldMetas)
            {
                field.Value.Commit(FixName(field.Key.InternalName)); //does not handle shared names
            }
            foreach(ParamMetaData param in _ParamMetas.Values)
            {
                param.Commit();
                param.Save();
            }
        }

        public static ParamMetaData XmlDeserialize(string path, PARAMDEF def)
        {
            if (!File.Exists(path))
            {
                return new ParamMetaData(def);
            }
            var mxml = new XmlDocument();
            try
            {
                mxml.Load(path);
                return new ParamMetaData(mxml, path, def);
            }
            catch
            {
                return new ParamMetaData(def);
            }
        }

        internal static string FixName(string internalName)
        {
            string name = Regex.Replace(internalName, @"[^a-zA-Z0-9_]", "");
            if (Regex.IsMatch(name, $@"^\d"))
                name = "_" + name;
            return name;
        }
    }

    public class FieldMetaData
    {
        internal static Dictionary<PARAMDEF.Field, FieldMetaData> _FieldMetas = new Dictionary<PARAMDEF.Field, FieldMetaData>();

        private ParamMetaData _parent;

        /// <summary>
        /// Name of another Param that a Field may refer to.
        /// </summary>
        public List<string> RefTypes { get; set; }

        /// <summary>
        /// Name linking fields from multiple params that may share values.
        /// </summary>
        public string VirtualRef {get; set;}

        /// <summary>
        /// Set of generally acceptable values, named
        /// </summary>
        public ParamEnum EnumType {get; set;}

        /// <summary>
        /// Alternate name for a field not provided by source defs or paramfiles.
        /// </summary>
        public string AltName {get; set;}

        /// <summary>
        /// A big tooltip to explain the field to the user
        /// </summary>
        public string Wiki {get; set;}

        /// <summary>
        /// Is this u8 field actually a boolean?
        /// </summary>
        public bool IsBool {get; set;}

        public static FieldMetaData Get(PARAMDEF.Field def)
        {
            return _FieldMetas[def];
        }

        private static void Add(PARAMDEF.Field key, FieldMetaData meta)
        {
            _FieldMetas.Add(key, meta);
        }

        public FieldMetaData(PARAMDEF.Field field)
        {
            Add(field, this);
            // Blank Metadata
        }

        public FieldMetaData(ParamMetaData parent, XmlNode fieldMeta, PARAMDEF.Field field)
        {
            _parent = parent;
            Add(field, this);
            RefTypes = null;
            VirtualRef = null;
            XmlAttribute Ref = fieldMeta.Attributes["Refs"];
            if (Ref != null)
                RefTypes = new List<string>(Ref.InnerText.Split(","));
            XmlAttribute VRef = fieldMeta.Attributes["VRef"];
            if (VRef != null)
                VirtualRef = VRef.InnerText;
            XmlAttribute Enum = fieldMeta.Attributes["Enum"];
            if (Enum != null)
                EnumType = parent.enums.GetValueOrDefault(Enum.InnerText, null);
            XmlAttribute AlternateName = fieldMeta.Attributes["AltName"];
            if (AlternateName != null)
                AltName = AlternateName.InnerText;
            XmlAttribute WikiText = fieldMeta.Attributes["Wiki"];
            if (WikiText != null)
                Wiki = WikiText.InnerText.Replace("\\n", "\n");
            XmlAttribute IsBoolean = fieldMeta.Attributes["IsBool"];
            if (IsBoolean != null)
                IsBool = true;
        }

        public void Commit(string field)
        {
            if (_parent == null || _parent._xml == null)
                return;
            if (RefTypes != null)
                ParamMetaData.GetXmlProperty("Refs", _parent._xml, "PARAMMETA", "Field", field).InnerText = String.Join(',', RefTypes); //doesn't handle total removal
            if (VirtualRef != null)
                ParamMetaData.GetXmlProperty("Vref",  _parent._xml, "PARAMMETA", "Field", field).InnerText = VirtualRef; //doesn't handle nulling
            if (EnumType != null)
                ParamMetaData.GetXmlProperty("Enum",  _parent._xml, "PARAMMETA", "Field", field).InnerText = EnumType.name; //doesn't handle nulling
            if (AltName != null)
                ParamMetaData.GetXmlProperty("AltName",  _parent._xml, "PARAMMETA", "Field", field).InnerText = AltName; //doesn't handle return to null
            if (Wiki != null)
                ParamMetaData.GetXmlProperty("Wiki",  _parent._xml, "PARAMMETA", "Field", field).InnerText = Wiki; //doesn't handle return to null
            if (IsBool)
                ParamMetaData.GetXmlProperty("IsBool",  _parent._xml, "PARAMMETA", "Field", field).InnerText = ""; //cannot unset bool status
        }
    }

    public class ParamEnum
    {
        public string name;
        public Dictionary<string, string> values = new Dictionary<string, string>(); // using string as an intermediate type. first string is value, second is name.
        
        public ParamEnum(XmlNode enumNode)
        {
            name = enumNode.Attributes["Name"].InnerText;
            foreach (XmlNode option in enumNode.SelectNodes("Option"))
            {
                values.Add(option.Attributes["Value"].InnerText, option.Attributes["Name"].InnerText);
            }
        }
    }
}