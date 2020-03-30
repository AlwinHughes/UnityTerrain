using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax {
  

  public float min { get; private set;}
  public float max { get; private set;}

  public MinMax() {
    resetMinMax();
  }

  public void addValues(float[] arr) {
    for(int i = 0; i < arr.Length; i++) {
      if( arr[i] > max) 
        max = arr[i];
      else if (arr[i] < min) 
        min = arr[i];
    }
  }

  public void resetMinMax() {
    min = float.MaxValue;
    max = float.MinValue;
  }

  public void log() {
    Debug.Log("max: " + max + " min: " + min);
  }
}
