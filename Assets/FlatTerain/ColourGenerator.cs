using System;
using UnityEngine;

[Serializable]
public class ColourGenerator {


  public ColourSettings col_set;

  public ColourGenerator(ColourSettings cs) {
    this.col_set = cs;
  }

  public void updateCol(MinMax mm) {
    col_set.material.SetVector("_heightMinMax", new Vector4(mm.min, mm.max));
  }



}
