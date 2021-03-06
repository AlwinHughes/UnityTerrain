﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Chunk))]
public class ChunkEditor : Editor {

  Chunk chunk;
  Editor noise_editor;
  Editor col_editor;

  public override void OnInspectorGUI() {
    DrawDefaultInspector();

    chunk = (Chunk) target;


    if(GUILayout.Button("Update Col")) {
      chunk.onColChange();
    }

    DrawSettingsEditor(chunk.noise_options, chunk.onTerrainOptionsChange, ref chunk.noise_option_foldout, ref noise_editor);

    DrawSettingsEditor(chunk.col_set, chunk.onColChange, ref chunk.col_set_foldout, ref col_editor);


    for(int i = 0; i < chunk.tgopt.options.Length; i++) {
      DrawSettingsEditor(chunk.tgopt.options[i], chunk.onTerrainOptionsChange, ref chunk.tgopt.foldouts[i], ref chunk.tgopt.editors[i]);
    }

    /*
    for(int i = 0; i < chunk.generators.Length; i++) {

      if(chunk.generators[i].draw_editor) {
        DrawSettingsEditor(chunk.generators[i].gen_opts, chunk.onTerrainOptionsChange, ref chunk.generators[i].fold_out, ref chunk.generators[i].editor);
      }

    }
    */


    if(GUILayout.Button("Transform")) {
      chunk.transformNoiseGrid();
      chunk.constructMesh();
    }

    if(GUILayout.Button("Reset")) {
      chunk.resetNoiseGrid();
      chunk.constructMesh();
    }
  }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if (settings != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                        {
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }
    }

  void DrawSettingsEditor() {

  }

  private void OnEnable() {
    chunk = (Chunk) target;
  }

}
