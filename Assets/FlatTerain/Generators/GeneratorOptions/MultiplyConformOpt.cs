using System;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyConformOpt : ScriptableObject {

  public Edge edge;

  public float[] fixed_points;

  public bool enabled = true;

  public Slope slope;

  public MultiplyConformOpt(float[] fixed_points, Edge e, Slope s) {
    this.edge = e;
    this.fixed_points = fixed_points;
    this.slope = s;
  }

}
