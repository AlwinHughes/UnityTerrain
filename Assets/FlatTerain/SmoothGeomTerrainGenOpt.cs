using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothGeomTerrainGenOpt : ScriptableObject {

  [Range(0f, 5f)]
  public float amplitude_ratio;
  [Range(0f, 5f)]
  public float scale_ratio;
  [Range(0, 10)]
  public int num_octaves = 3;

  public bool enabled = true;

  public SmoothGeomTerrainGenOpt(float a, float s, int n) {
    amplitude_ratio = a;
    scale_ratio = s;
    num_octaves = n;
  }

}
