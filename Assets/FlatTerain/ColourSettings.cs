using System;
using UnityEngine;

[CreateAssetMenu()]
[Serializable]
public class ColourSettings : ScriptableObject {

  public Gradient col_grad;

  public Material material;
}
