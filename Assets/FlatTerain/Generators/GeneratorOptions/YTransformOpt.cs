using UnityEngine;
using System;
using UnityEngine;

[CreateAssetMenu()][Serializable]
public class YTransformOpt : ScriptableObject
{

  public bool take_average = false;
  public bool enabled = true;

  public YTransformOpt(bool take_average) {
    this.take_average = take_average;
  }

}
