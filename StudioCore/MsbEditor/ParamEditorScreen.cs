﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Linq;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.Utilities;
using ImGuiNET;
using SoulsFormats;

namespace StudioCore.MsbEditor
{
    /// <summary>
    /// Interface for decorating param rows with additional information (such as english
    /// strings sourced from FMG files)
    /// </summary>
    public interface IParamDecorator
    {
        public void DecorateParam(PARAM.Row row);

        public void DecorateContextMenu(PARAM.Row row);
    }

    public class FMGItemParamDecorator : IParamDecorator
    {
        private FMGBank.ItemCategory _category = FMGBank.ItemCategory.None;

        private Dictionary<int, FMG.Entry> _entryCache = new Dictionary<int, FMG.Entry>();

        public FMGItemParamDecorator(FMGBank.ItemCategory cat)
        {
            _category = cat;
        }

        public void DecorateParam(PARAM.Row row)
        {
            if (!_entryCache.ContainsKey((int)row.ID))
            {
                _entryCache.Add((int)row.ID, FMGBank.LookupItemID((int)row.ID, _category));
            }
            var entry = _entryCache[(int)row.ID];
            if (entry != null)
            {
                ImGui.SameLine();
                ImGui.TextColored(new Vector4(1.0f, 1.0f, 0.0f, 1.0f), $@" <{entry.Text}>");
            }
        }

        public void DecorateContextMenu(PARAM.Row row)
        {
            if (!_entryCache.ContainsKey((int)row.ID))
            {
                return;
            }
            if (ImGui.BeginPopupContextItem(row.ID.ToString()))
            {
                if (ImGui.Selectable($@"Goto {_category.ToString()} Text"))
                {
                    EditorCommandQueue.AddCommand($@"text/select/{_category.ToString()}/{row.ID}");
                }
                ImGui.EndPopup();
            }
        }
    }

    public class ParamEditorScreen : EditorScreen
    {
        public ActionManager EditorActionManager = new ActionManager();

        private string _activeParam = null;
        private PARAM.Row _activeRow = null;

        private PropertyEditor _propEditor = null;

        private Dictionary<string, IParamDecorator> _decorators = new Dictionary<string, IParamDecorator>();

        public ParamEditorScreen(Sdl2Window window, GraphicsDevice device)
        {
            _propEditor = new PropertyEditor(EditorActionManager);

            _decorators.Add("EquipParamAccessory", new FMGItemParamDecorator(FMGBank.ItemCategory.Rings));
            _decorators.Add("EquipParamGoods", new FMGItemParamDecorator(FMGBank.ItemCategory.Goods));
            _decorators.Add("EquipParamProtector", new FMGItemParamDecorator(FMGBank.ItemCategory.Armor));
            _decorators.Add("EquipParamWeapon", new FMGItemParamDecorator(FMGBank.ItemCategory.Weapons));
        }

        public void OnGUI(string[] initcmd)
        {
            // Docking setup
            //var vp = ImGui.GetMainViewport();
            var wins = ImGui.GetWindowSize();
            var winp = ImGui.GetWindowPos();
            winp.Y += 20.0f;
            wins.Y -= 20.0f;
            ImGui.SetNextWindowPos(winp);
            ImGui.SetNextWindowSize(wins);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.0f);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0.0f, 0.0f));
            ImGui.PushStyleVar(ImGuiStyleVar.ChildBorderSize, 0.0f);
            ImGuiWindowFlags flags = ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove;
            flags |= ImGuiWindowFlags.MenuBar | ImGuiWindowFlags.NoDocking;
            flags |= ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus;
            flags |= ImGuiWindowFlags.NoBackground;
            //ImGui.Begin("DockSpace_MapEdit", flags);
            ImGui.PopStyleVar(4);
            //var dsid = ImGui.GetID("DockSpace_ParamEdit");
            //ImGui.DockSpace(dsid, new Vector2(0, 0));

            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("File"))
                {
                    if (ImGui.MenuItem("Set Interroot..", "CTRL+I") || InputTracker.GetControlShortcut(Key.I))
                    {
                        
                    }
                    ImGui.EndMenu();
                }
                if (ImGui.BeginMenu("Edit"))
                {
                    if (ImGui.MenuItem("Undo", "CTRL+Z", false, EditorActionManager.CanUndo()))
                    {
                        EditorActionManager.UndoAction();
                    }
                    if (ImGui.MenuItem("Redo", "Ctrl+Y", false, EditorActionManager.CanRedo()))
                    {
                        EditorActionManager.RedoAction();
                    }
                    if (ImGui.MenuItem("Delete", "Delete", false, Selection.IsSelection()))
                    {
                    }
                    if (ImGui.MenuItem("Duplicate", "Ctrl+D", false, Selection.IsSelection()))
                    {
                    }
                    ImGui.EndMenu();
                }
                ImGui.EndMainMenuBar();
            }

            // Keyboard shortcuts
            if (EditorActionManager.CanUndo() && InputTracker.GetControlShortcut(Key.Z))
            {
                EditorActionManager.UndoAction();
            }
            if (EditorActionManager.CanRedo() && InputTracker.GetControlShortcut(Key.Y))
            {
                EditorActionManager.RedoAction();
            }
            

            //ImGui.SetNextWindowSize(new Vector2(300, 500), ImGuiCond.FirstUseEver);
            //ImGui.SetNextWindowPos(new Vector2(20, 20), ImGuiCond.FirstUseEver);

            if (ParamBank.Params == null)
            {
                return;
            }

            bool doFocus = false;
            // Parse select commands
            if (initcmd != null && initcmd[0] == "select")
            {
                if (initcmd.Length > 1 && ParamBank.Params.ContainsKey(initcmd[1]))
                {
                    doFocus = true;
                    _activeParam = initcmd[1];
                    if (initcmd.Length > 2)
                    {
                        var p = ParamBank.Params[_activeParam];
                        int id;
                        var parsed = int.TryParse(initcmd[2], out id);
                        if (parsed)
                        {
                            var r = p.Rows.FirstOrDefault(r => r.ID == id);
                            if (r != null)
                            {
                                _activeRow = r;
                            }
                        }
                    }
                }
            }

            ImGui.Columns(3);
            ImGui.BeginChild("params");
            foreach (var param in ParamBank.Params)
            {
                if (ImGui.Selectable(param.Key, param.Key == _activeParam))
                {
                    _activeParam = param.Key;
                    _activeRow = null;
                }
                if (doFocus && param.Key == _activeParam)
                {
                    ImGui.SetScrollHereY();
                }
            }
            ImGui.EndChild();
            ImGui.NextColumn();
            ImGui.BeginChild("rows");
            if (_activeParam == null)
            {
                ImGui.Text("Select a param to see rows");
            }
            else
            {
                IParamDecorator decorator = null;
                if (_decorators.ContainsKey(_activeParam))
                {
                    decorator = _decorators[_activeParam];
                }
                var p = ParamBank.Params[_activeParam];
                foreach (var r in p.Rows)
                {
                    if (ImGui.Selectable($@"{r.ID} {r.Name}", _activeRow == r))
                    {
                        _activeRow = r;
                    }
                    if (decorator != null)
                    {
                        decorator.DecorateContextMenu(r);
                        decorator.DecorateParam(r);
                    }
                    if (doFocus && _activeRow == r)
                    {
                        ImGui.SetScrollHereY();
                    }
                }
            }
            ImGui.EndChild();
            ImGui.NextColumn();
            ImGui.BeginChild("columns");
            if (_activeRow == null)
            {
                ImGui.Text("Select a row to see properties");
            }
            else
            {
                _propEditor.PropEditorParamRow(_activeRow);
            }
            ImGui.EndChild();
        }
    }
}