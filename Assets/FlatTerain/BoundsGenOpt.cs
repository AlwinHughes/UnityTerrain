using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsGenOpt : ScriptableObject {

  [Range(-5f, 5f)]
  public float floor;
  [Range(-5f, 5f)]
  public float ceiling;

  public bool enabled = true;

  public BoundsGenOpt (float f, float c) {
    floor = f;
    ceiling = c;
  }

}
