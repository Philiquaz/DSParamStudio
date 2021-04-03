using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using SoulsFormats;

namespace StudioCore
{
    /// <summary>
    /// Generic asset description for a generic game asset
    /// </summary>
    public class AssetDescription
    {
        /// <summary>
        /// Pretty UI friendly name for an asset. Usually the file name without an extention i.e. c1234
        /// </summary>
        public string AssetName = null;

        /// <summary>
        /// Absolute path of where the full asset is located. If this asset exists in a mod override directory,
        /// then this path points to that instead of the base game asset.
        /// </summary>
        public string AssetPath = null;

        public string AssetArchiveVirtualPath = null;

        /// <summary>
        /// Virtual friendly path for this asset to use with the resource manager
        /// </summary>
        public string AssetVirtualPath = null;

        /// <summary>
        /// Where applicable, the numeric asset ID. Usually applies to chrs, objs, and various map pieces
        /// </summary>
        public int AssetID;

        public override int GetHashCode()
        {
            if (AssetVirtualPath != null)
            {
                return AssetVirtualPath.GetHashCode();
            }
            else if (AssetPath != null)
            {
                return AssetPath.GetHashCode();
            }
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is AssetDescription ad)
            {
                if (AssetVirtualPath != null)
                {
                    return AssetVirtualPath.Equals(ad.AssetVirtualPath);
                }
                if (AssetPath != null)
                {
                    return AssetPath.Equals(ad.AssetPath);
                }
            }
            return base.Equals(obj);
        }
    }

    /// <summary>
    /// Exposes an interface to retrieve game assets from the various souls games. Also allows layering
    /// of an additional mod directory on top of the game assets.
    /// </summary>
    public class AssetLocator
    {

        public static readonly string GameExecutatbleFilter =
            "Windows executable (*.EXE) |*.EXE*|" +
            "Playstation executable (*.BIN) |*.BIN*|" +
            "All Files|*.*";

        public static readonly string JsonFilter =
            "Project file (project.json) |PROJECT.JSON";

        public GameType Type { get; private set; } = GameType.Undefined;

        /// <summary>
        /// The game interroot where all the game assets are
        /// </summary>
        public string GameRootDirectory { get; private set; } = null;

        /// <summary>
        /// An optional override mod directory where modded files are stored
        /// </summary>
        public string GameModDirectory { get; private set; } = null;

        public AssetLocator()
        {
        }

        public string GetAssetPath(string relpath)
        {
            if (GameModDirectory != null)
            {
                var modpath = $@"{GameModDirectory}\{relpath}";
                if (File.Exists(modpath))
                {
                    return modpath;
                }
            }
            return $@"{GameRootDirectory}\{relpath}";
        }

        public GameType GetGameTypeForExePath(string exePath)
        {
            GameType type = GameType.Undefined;
            if (exePath.ToLower().Contains("darksouls.exe"))
            {
                type = GameType.DarkSoulsPTDE;
            }
            else if (exePath.ToLower().Contains("darksoulsremastered.exe"))
            {
                type = GameType.DarkSoulsRemastered;
            }
            else if (exePath.ToLower().Contains("darksoulsii.exe"))
            {
                type = GameType.DarkSoulsIISOTFS;
            }
            else if (exePath.ToLower().Contains("darksoulsiii.exe"))
            {
                type = GameType.DarkSoulsIII;
            }
            else if (exePath.ToLower().Contains("eboot.bin"))
            {
                var path = Path.GetDirectoryName(exePath);
                if (Directory.Exists($@"{path}\dvdroot_ps4"))
                {
                    type = GameType.Bloodborne;
                }
                else
                {
                    type = GameType.DemonsSouls;
                }
            }
            else if (exePath.ToLower().Contains("sekiro.exe"))
            {
                type = GameType.Sekiro;
            }
            return type;
        }

        public bool CheckFilesExpanded(string gamepath, GameType game)
        {
            if (game == GameType.DarkSoulsPTDE || game == GameType.DarkSoulsIII || game == GameType.Sekiro)
            {
                if (!Directory.Exists($@"{gamepath}\map"))
                {
                    return false;
                }
                if (!Directory.Exists($@"{gamepath}\obj"))
                {
                    return false;
                }
            }
            if (game == GameType.DarkSoulsIISOTFS)
            {
                if (!Directory.Exists($@"{gamepath}\map"))
                {
                    return false;
                }
                if (!Directory.Exists($@"{gamepath}\model\obj"))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Sets the game root directory by giving a path to the game exe/eboot.bin. Autodetects the game type.
        /// </summary>
        /// <param name="exePath">Path to an exe/eboot.bin</param>
        /// <returns>true if the game was autodetected</returns>
        public bool SetGameRootDirectoryByExePath(string exePath)
        {
            GameRootDirectory = Path.GetDirectoryName(exePath);
            if (exePath.ToLower().Contains("darksouls.exe"))
            {
                Type = GameType.DarkSoulsPTDE;
            }
            else if (exePath.ToLower().Contains("darksoulsremastered.exe"))
            {
                Type = GameType.DarkSoulsRemastered;
            }
            else if (exePath.ToLower().Contains("darksoulsii.exe"))
            {
                Type = GameType.DarkSoulsIISOTFS;
            }
            else if (exePath.ToLower().Contains("darksoulsiii.exe"))
            {
                Type = GameType.DarkSoulsIII;
            }
            else if (exePath.ToLower().Contains("eboot.bin"))
            {
                if (Directory.Exists($@"{GameRootDirectory}\dvdroot_ps4"))
                {
                    Type = GameType.Bloodborne;
                    GameRootDirectory = GameRootDirectory + $@"\dvdroot_ps4";
                }
                else
                {
                    Type = GameType.DemonsSouls;
                }
            }
            else if (exePath.ToLower().Contains("sekiro.exe"))
            {
                Type = GameType.Sekiro;
            }
            else
            {
                GameRootDirectory = null;
            }

            // Invalidate various caches
            GameModDirectory = null;

            return true;
        }

        public void SetModProjectDirectory(string dir)
        {
            GameModDirectory = dir;
        }

        public void SetFromProjectSettings(MsbEditor.ProjectSettings settings, string moddir)
        {
            Type = settings.GameType;
            GameRootDirectory = settings.GameRoot;
            GameModDirectory = moddir;
        }

        public bool FileExists(string relpath)
        {
            if (GameModDirectory != null && File.Exists($@"{GameModDirectory}\{relpath}"))
            {
                return true;
            }
            else if (File.Exists($@"{GameRootDirectory}\{relpath}"))
            {
                return true;
            }
            return false;
        }

        public string GetOverridenFilePath(string relpath)
        {
            if (GameModDirectory != null && File.Exists($@"{GameModDirectory}\{relpath}"))
            {
                return $@"{GameModDirectory}\{relpath}";
            }
            else if (File.Exists($@"{GameRootDirectory}\{relpath}"))
            {
                return $@"{GameRootDirectory}\{relpath}";
            }
            return null;
        }

        public AssetDescription GetEnglishItemMsgbnd(bool writemode = false)
        {
            string path = $@"msg\engus\item.msgbnd.dcx";
            if (Type == GameType.DemonsSouls)
            {
                path = $@"msg\na_english\item.msgbnd.dcx";
            }
            else if (Type == GameType.DarkSoulsPTDE)
            {
                path = $@"msg\ENGLISH\item.msgbnd";
            }
            else if (Type == GameType.DarkSoulsRemastered)
            {
                path = $@"msg\ENGLISH\item.msgbnd.dcx";
            }
            else if (Type == GameType.DarkSoulsIISOTFS)
            {
                // DS2 does not have an msgbnd but loose fmg files instead
                path = $@"menu\text\english";
                AssetDescription ad2 = new AssetDescription();
                ad2.AssetPath = writemode ? path : $@"{GameRootDirectory}\{path}";
                return ad2;
            }
            else if (Type == GameType.DarkSoulsIII)
            {
                path = $@"msg\engus\item_dlc2.msgbnd.dcx";
            }
            AssetDescription ad = new AssetDescription();
            if (writemode)
            {
                ad.AssetPath = path;
                return ad;
            }
            if (GameModDirectory != null && File.Exists($@"{GameModDirectory}\{path}") || (writemode && GameModDirectory != null))
            {
                ad.AssetPath = $@"{GameModDirectory}\{path}";
            }
            else if (File.Exists($@"{GameRootDirectory}\{path}"))
            {
                ad.AssetPath = $@"{GameRootDirectory}\{path}";
            }
            return ad;
        }

        public string GetParamAssetsDir()
        {
            string game;
            switch (Type)
            {
                case GameType.DemonsSouls:
                    game = "DES";
                    break;
                case GameType.DarkSoulsPTDE:
                    game = "DS1";
                    break;
                case GameType.DarkSoulsRemastered:
                    game = "DS1R";
                    break;
                case GameType.DarkSoulsIISOTFS:
                    game = "DS2S";
                    break;
                case GameType.Bloodborne:
                    game = "BB";
                    break;
                case GameType.DarkSoulsIII:
                    game = "DS3";
                    break;
                case GameType.Sekiro:
                    game = "SDT";
                    break;
                default:
                    throw new Exception("Game type not set");
            }
            return  $@"Assets\Paramdex\{game}";
        }

        public string GetParamdefDir()
        {
            return $@"{GetParamAssetsDir()}\Defs";
        }

        public string GetParammetaDir()
        {
            return $@"{GetParamAssetsDir()}\Meta";
        }
        
        public string GetParamNamesDir()
        {
            return $@"{GetParamAssetsDir()}\Names";
        }

        public PARAMDEF GetParamdefForParam(string paramType)
        {
            PARAMDEF pd = PARAMDEF.XmlDeserialize($@"{GetParamdefDir()}\{paramType}.xml");
            MsbEditor.ParamMetaData meta = MsbEditor.ParamMetaData.XmlDeserialize($@"{GetParammetaDir()}\{paramType}.xml", pd);
            return pd;
        }

        public AssetDescription GetDS2GeneratorParam(string mapid, bool writemode=false)
        {
            AssetDescription ad = new AssetDescription();
            var path = $@"Param\generatorparam_{mapid}";
            if (GameModDirectory != null && File.Exists($@"{GameModDirectory}\{path}.param") || (writemode && GameModDirectory != null))
            {
                ad.AssetPath = $@"{GameModDirectory}\{path}.param";
            }
            else if (File.Exists($@"{GameRootDirectory}\{path}.param"))
            {
                ad.AssetPath = $@"{GameRootDirectory}\{path}.param";
            }
            ad.AssetName = mapid + "_generators";
            return ad;
        }

        public AssetDescription GetDS2GeneratorLocationParam(string mapid, bool writemode = false)
        {
            AssetDescription ad = new AssetDescription();
            var path = $@"Param\generatorlocation_{mapid}";
            if (GameModDirectory != null && File.Exists($@"{GameModDirectory}\{path}.param") || (writemode && GameModDirectory != null))
            {
                ad.AssetPath = $@"{GameModDirectory}\{path}.param";
            }
            else if (File.Exists($@"{GameRootDirectory}\{path}.param"))
            {
                ad.AssetPath = $@"{GameRootDirectory}\{path}.param";
            }
            ad.AssetName = mapid + "_generator_locations";
            return ad;
        }

        public AssetDescription GetDS2GeneratorRegistParam(string mapid, bool writemode = false)
        {
            AssetDescription ad = new AssetDescription();
            var path = $@"Param\generatorregistparam_{mapid}";
            if (GameModDirectory != null && File.Exists($@"{GameModDirectory}\{path}.param") || (writemode && GameModDirectory != null))
            {
                ad.AssetPath = $@"{GameModDirectory}\{path}.param";
            }
            else if (File.Exists($@"{GameRootDirectory}\{path}.param"))
            {
                ad.AssetPath = $@"{GameRootDirectory}\{path}.param";
            }
            ad.AssetName = mapid + "_generator_registrations";
            return ad;
        }

        public AssetDescription GetDS2EventParam(string mapid, bool writemode = false)
        {
            AssetDescription ad = new AssetDescription();
            var path = $@"Param\eventparam_{mapid}";
            if (GameModDirectory != null && File.Exists($@"{GameModDirectory}\{path}.param") || (writemode && GameModDirectory != null))
            {
                ad.AssetPath = $@"{GameModDirectory}\{path}.param";
            }
            else if (File.Exists($@"{GameRootDirectory}\{path}.param"))
            {
                ad.AssetPath = $@"{GameRootDirectory}\{path}.param";
            }
            ad.AssetName = mapid + "_event_params";
            return ad;
        }

        public AssetDescription GetDS2EventLocationParam(string mapid, bool writemode = false)
        {
            AssetDescription ad = new AssetDescription();
            var path = $@"Param\eventlocation_{mapid}";
            if (GameModDirectory != null && File.Exists($@"{GameModDirectory}\{path}.param") || (writemode && GameModDirectory != null))
            {
                ad.AssetPath = $@"{GameModDirectory}\{path}.param";
            }
            else if (File.Exists($@"{GameRootDirectory}\{path}.param"))
            {
                ad.AssetPath = $@"{GameRootDirectory}\{path}.param";
            }
            ad.AssetName = mapid + "_event_locations";
            return ad;
        }

        public AssetDescription GetDS2ObjInstanceParam(string mapid, bool writemode = false)
        {
            AssetDescription ad = new AssetDescription();
            var path = $@"Param\mapobjectinstanceparam_{mapid}";
            if (GameModDirectory != null && File.Exists($@"{GameModDirectory}\{path}.param") || (writemode && GameModDirectory != null))
            {
                ad.AssetPath = $@"{GameModDirectory}\{path}.param";
            }
            else if (File.Exists($@"{GameRootDirectory}\{path}.param"))
            {
                ad.AssetPath = $@"{GameRootDirectory}\{path}.param";
            }
            ad.AssetName = mapid + "_object_instance_params";
            return ad;
        }

        public AssetDescription GetNullAsset()
        {
            var ret = new AssetDescription();
            ret.AssetPath = "null";
            ret.AssetName = "null";
            ret.AssetArchiveVirtualPath = "null";
            ret.AssetVirtualPath = "null";
            return ret;
        }

        /// <summary>
        /// Converts a virtual path to an actual filesystem path. Only resolves virtual paths up to the bnd level,
        /// which the remaining string is output for additional handling
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public string VirtualToRealPath(string virtualPath, out string bndpath)
        {
            var pathElements = virtualPath.Split('/');
            var mapRegex = new Regex(@"^m\d{2}_\d{2}_\d{2}_\d{2}$");

            // Parse the virtual path with a DFA and convert it to a game path
            int i = 0;
            if (pathElements[i].Equals("map"))
            {
                i++;
                if (pathElements[i].Equals("tex"))
                {
                    i++;
                    if (Type == GameType.DarkSoulsIISOTFS)
                    {
                        var mid = pathElements[i];
                        i++;
                        var id = pathElements[i];
                        if (id == "tex")
                        {
                            bndpath = "";
                            return GetAssetPath($@"model\map\t{mid.Substring(1)}.tpfbhd");
                        }
                    }
                    else if (Type == GameType.DemonsSouls)
                    {
                        var mid = pathElements[i];
                        i++;
                        bndpath = "";
                        return GetAssetPath($@"map\{mid}\{mid}_{pathElements[i]}.tpf.dcx");
                    }
                    else
                    {
                        var mid = pathElements[i];
                        i++;
                        bndpath = "";
                        if (pathElements[i] == "env")
                        {
                            if (Type == GameType.DarkSoulsRemastered)
                            {
                                return GetAssetPath($@"map\{mid}\GI_EnvM_{mid}.tpf.dcx");
                            }
                            return GetAssetPath($@"map\{mid}\{mid}_envmap.tpf.dcx");
                        }
                        return GetAssetPath($@"map\{mid}\{mid}_{pathElements[i]}.tpfbhd");
                    }
                }
                else if (mapRegex.IsMatch(pathElements[i]))
                {
                    var mapid = pathElements[i];
                    i++;
                    if (pathElements[i].Equals("model"))
                    {
                        i++;
                        bndpath = "";
                        if (Type == GameType.DarkSoulsPTDE)
                        {
                            return GetAssetPath($@"map\{mapid}\{pathElements[i]}.flver");
                        }
                        else if (Type == GameType.DarkSoulsRemastered)
                        {
                            return GetAssetPath($@"map\{mapid}\{pathElements[i]}.flver.dcx");
                        }
                        else if (Type == GameType.DarkSoulsIISOTFS)
                        {
                            return GetAssetPath($@"model\map\{mapid}.mapbhd");
                        }
                        else if (Type == GameType.Bloodborne || Type == GameType.DemonsSouls)
                        {
                            return GetAssetPath($@"map\{mapid}\{pathElements[i]}.flver.dcx");
                        }
                        return GetAssetPath($@"map\{mapid}\{pathElements[i]}.mapbnd.dcx");
                    }
                    else if (pathElements[i].Equals("hit"))
                    {
                        i++;
                        var hittype = pathElements[i];
                        i++;
                        if (Type == GameType.DarkSoulsPTDE || Type == GameType.DemonsSouls)
                        {
                            bndpath = "";
                            return GetAssetPath($@"map\{mapid}\{pathElements[i]}");
                        }
                        else if (Type == GameType.DarkSoulsIISOTFS)
                        {
                            bndpath = "";
                            return GetAssetPath($@"model\map\h{mapid.Substring(1)}.hkxbhd");
                        }
                        else if (Type == GameType.DarkSoulsIII || Type == GameType.Bloodborne)
                        {
                            bndpath = "";
                            if (hittype == "lo")
                            {
                                return GetAssetPath($@"map\{mapid}\l{mapid.Substring(1)}.hkxbhd");
                            }
                            return GetAssetPath($@"map\{mapid}\h{mapid.Substring(1)}.hkxbhd");
                        }
                        bndpath = "";
                        return null;
                    }
                    else if (pathElements[i].Equals("nav"))
                    {
                        i++;
                        if (Type == GameType.DarkSoulsPTDE || Type == GameType.DemonsSouls || Type == GameType.DarkSoulsRemastered)
                        {
                            if (i < pathElements.Length)
                            {
                                bndpath = $@"{pathElements[i]}";
                            }
                            else
                            {
                                bndpath = "";
                            }
                            if (Type == GameType.DarkSoulsRemastered)
                            {
                                return GetAssetPath($@"map\{mapid}\{mapid}.nvmbnd.dcx");
                            }
                            return GetAssetPath($@"map\{mapid}\{mapid}.nvmbnd");
                        }
                        else if (Type == GameType.DarkSoulsIII)
                        {
                            bndpath = "";
                            return GetAssetPath($@"map\{mapid}\{ mapid}.nvmhktbnd.dcx");
                        }
                        bndpath = "";
                        return null;
                    }
                }
            }
            else if (pathElements[i].Equals("chr"))
            {
                i++;
                var chrid = pathElements[i];
                i++;
                if (pathElements[i].Equals("model"))
                {
                    bndpath = "";
                    if (Type == GameType.DarkSoulsPTDE)
                    {
                        return GetOverridenFilePath($@"chr\{chrid}.chrbnd");
                    }
                    else if (Type == GameType.DarkSoulsIISOTFS)
                    {
                        return GetOverridenFilePath($@"model\chr\{chrid}.bnd");
                    }
                    else if (Type == GameType.DemonsSouls)
                    {
                        return GetOverridenFilePath($@"chr\{chrid}\{chrid}.chrbnd.dcx");
                    }
                    return GetOverridenFilePath($@"chr\{chrid}.chrbnd.dcx");
                }
                else if (pathElements[i].Equals("tex"))
                {
                    bndpath = "";
                    if (Type == GameType.DarkSoulsIII || Type == GameType.Sekiro)
                    {
                        return GetOverridenFilePath($@"chr\{chrid}.texbnd.dcx");
                    }
                    else if (Type == GameType.Bloodborne)
                    {
                        return GetOverridenFilePath($@"chr\{chrid}_2.tpf.dcx");
                    }
                }
            }
            else if (pathElements[i].Equals("obj"))
            {
                i++;
                var objid = pathElements[i];
                i++;
                if (pathElements[i].Equals("model"))
                {
                    bndpath = "";
                    if (Type == GameType.DarkSoulsPTDE)
                    {
                        return GetOverridenFilePath($@"obj\{objid}.objbnd");
                    }
                    else if (Type == GameType.DarkSoulsIISOTFS)
                    {
                        return GetOverridenFilePath($@"model\obj\{objid}.bnd");
                    }
                    return GetOverridenFilePath($@"obj\{objid}.objbnd.dcx");
                }
            }

            bndpath = virtualPath;
            return null;
        }

        public string GetBinderVirtualPath(string virtualPathToBinder, string binderFilePath)
        {
            var filename = Path.GetFileNameWithoutExtension($@"{binderFilePath}.blah");
            if (filename.Length > 0)
            {
                filename = $@"{virtualPathToBinder}/{filename}";
            }
            else
            {
                filename = virtualPathToBinder;
            }
            return filename;
        }
    }
}
