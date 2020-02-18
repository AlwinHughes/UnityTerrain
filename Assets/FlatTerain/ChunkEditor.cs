using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Chunk))]
public class ChunkEditor : Editor {

  Chunk chunk;
  Editor terrain_editor;

  public override void OnInspectorGUI() {
    DrawDefaultInspector();
    chunk = (Chunk) target;

      DrawSettingsEditor(chunk.terrain_options, chunk.onTerrainOptionsChange, ref chunk.terrain_option_foldout, ref terrain_editor);
    //DrawSettingsEditor(chunk.terrain_options, chunk.onTerrainOptionsChange, ref chunk.terrain_option_foldout, terrain_editor);

    
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
