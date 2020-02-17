using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Chunk))]
public class ChunkEditor : Editor {

  Chunk chunk;

  public override void OnInspectorGUI() {
    DrawDefaultInspector();
    chunk = (Chunk) target;
    
    if(GUILayout.Button("Transform")) {
      chunk.transformNoiseGrid();
      chunk.constructMesh();
    }

    if(GUILayout.Button("Reset")) {
      chunk.resetNoiseGrid();
      chunk.constructMesh();
    }
  }

  void DrawSettingsEditor() {

  }

  private void OnEnable() {
    chunk = (Chunk) target;
  }

}
