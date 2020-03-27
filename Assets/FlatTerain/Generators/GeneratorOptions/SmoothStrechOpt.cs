using UnityEngine;
using System;

[CreateAssetMenu()]
[Serializable]
public class SmoothStrechOpt : ScriptableObject {

  [Range(0f, 10f)]
  public float scale_x;
  [Range(0f, 10f)]
  public float scale_y;

  public bool enabled = true;

  public SmoothStrechOpt(float x, float y) {
    scale_x = x;
    scale_y =y; 
  }

}
