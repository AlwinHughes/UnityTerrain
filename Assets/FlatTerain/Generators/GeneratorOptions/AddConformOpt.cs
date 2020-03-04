using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AddConformOpt : ScriptableObject {

  public float[] fixed_points;

  public Edge edge;

  public bool enabled = true;

  public AddConformOpt(float[] fixed_points, Edge edge) {
    this.fixed_points = fixed_points;
    this.edge = edge;
  }
}
