using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyConformOpt : ScriptableObject {

  public Edge edge;

  public float[] fixed_points;

  public bool enabled = true;

  public Slope slope;

  public MultiplyConformOpt(Edge e, float[] fixed_points, Slope s) {
    this.edge = e;
    this.fixed_points = fixed_points;
    this.slope = s;
  }

}
