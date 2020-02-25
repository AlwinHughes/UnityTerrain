﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeomRidgeGenOpt : ScriptableObject {

  [Range(0f, 5f)]
  public float power;
  [Range(1f, 5f)]
  public float scale_ratio;

  [Range(1f, 5f)]
  public float scale;
  
  [Range(0f, 1f)]
  public float amplitude_ratio;

  [Range(0, 10)]
  public int num_octaves;

  public bool enabled = true;

  public GeomRidgeGenOpt(float power, float scale_ratio, float scale, float amplitude_ratio, int num_octaves) {
    this.power = power;
    this.scale_ratio = scale_ratio;
    this.amplitude_ratio = amplitude_ratio;
    this.num_octaves = num_octaves;
    this.scale = scale;
  }
}
