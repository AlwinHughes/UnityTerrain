using UnityEngine;
using System;

[CreateAssetMenu()]
[Serializable]
public class RidgeGenOpt : ScriptableObject {

  [Range(0f, 5f)]
  public float power;
  [Range(0f, 5f)]
  public float scale;

  public bool enabled = true;

  public RidgeGenOpt(float power, float scale) {
    this.power = power;
    this.scale = scale;
  }
}
