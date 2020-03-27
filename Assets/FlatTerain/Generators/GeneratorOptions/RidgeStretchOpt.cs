using UnityEngine;
using System;

[CreateAssetMenu()]
[Serializable]
public class RidgeStretchOpt : ScriptableObject {

  [Range(0f, 10f)]
  public float scale_x;
  [Range(0f, 10f)]
  public float scale_y;
  [Range(0f, 5f)]
  public float power;

  public bool enabled = true;

  public bool multiply = true;

  public RidgeStretchOpt(float x, float y, float power) {
    scale_x = x;
    scale_y =y;
    this.power = power;
  }


}
