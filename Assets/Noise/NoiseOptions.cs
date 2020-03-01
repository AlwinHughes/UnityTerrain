using System.Collections;
using UnityEngine;
using System;

public class NoiseOptions : ScriptableObject {

  [Range(0f, 10f)]
  public float scale;
  [Range(2, 200)]
  public int res;
  [Range(0f, 20f)]
  public float amplitude;

  public NoiseOptions(float scale, int res, float amplitude) {
    this.scale = scale;
    this.res = res;
    this.amplitude = amplitude;
  }

  public NoiseOptions(NoiseOptions o) {
    this.scale = o.scale;
    this.res = o.res;
    this.amplitude = o.amplitude;
  }

}
