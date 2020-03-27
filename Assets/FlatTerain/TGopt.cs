using UnityEngine;
using System;
using UnityEditor;

[CreateAssetMenu()]
[Serializable]
public class TGopt : ScriptableObject {

  public GeneratorType[] types;

  public ScriptableObject[] options;

  public Editor[] editors;

  public bool[] foldouts;

  public TGopt(GeneratorType[] t, ScriptableObject[] o) {
    types = t;
    options = o;
    foldouts = new bool[types.Length];
    editors = new Editor[types.Length];
  }

}
