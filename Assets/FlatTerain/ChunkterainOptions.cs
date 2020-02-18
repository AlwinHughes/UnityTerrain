using System.Collections;
using System;
using UnityEngine;

public class ChunkterainOptions : ScriptableObject {

  private int num_octaves;
  public int numOctaves;/* {
    get { return num_octaves; }
    set {
      num_octaveh = value;
      this.resizeArrays(value);
    }
  }
  */

  private void resizeArrays(int n) {
    int[] temp = scales.Clone() as int[];
    scales = new float[n];

    int min_length = Math.Min(n, scales.Length);
    for(int i = 0; n < Math.Min(n, scales.Length);i++){
      scales[i] = temp[i];
    }

    temp = amplitudes.Clone() as int[];
    amplitudes = new float[n];
    for(int i = 0; n < Math.Min(n, scales.Length);i++){
      amplitudes[i] = temp[i];
    }
  }


  [Range(2,100)]
  public int res;
  public float[] scales;
  public float[] amplitudes;

  public ChunkterainOptions(float[] scales, float[] amplitudes, int res) {
    this.scales = scales;
    this.amplitudes = amplitudes;
    this.res = res;
    if(scales.Length != amplitudes.Length) {
      throw new Exception("array lenghts do not match");
    }
    this.numOctaves = scales.Length;
  }

}
