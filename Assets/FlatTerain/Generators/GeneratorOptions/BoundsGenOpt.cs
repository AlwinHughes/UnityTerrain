using System.Collections;
using UnityEngine;
using System;

[CreateAssetMenu()]
[Serializable]
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
