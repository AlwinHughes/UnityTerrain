using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TerrainGenerator {

  //editor stuff
  public ScriptableObject gen_opts;
  public bool fold_out = true;
  public bool draw_editor = false;


  protected float[,] noise_grid;

  public virtual void generateTerrain(NoiseOptions options) { }

  public virtual void applyTerrain(ref float[] existing_noise) {
    //default behaviour is to add new noise to old noise

    for(int i = 0; i < noise_grid.GetLength(0); i++) {
      for(int j = 0; j < noise_grid.GetLength(1); j++) {
        existing_noise[i + noise_grid.GetLength(0) * j] += noise_grid[i,j];
      }
    }
  }

  protected float[,] add2DArr(float[,] a1, float[,] a2) {

    for(int i = 0; i < a1.GetLength(0); i++) {
      for(int j = 0; j < a1.GetLength(1); j++) {
        a1[i,j] += a2[i,j];
      }
    }
    return a1;
  }

  protected float getMax(float[] arr) {
    float max = arr[0];
    for(int i = 1; i < arr.Length; i++) {
      if(arr[i] > max) {
        max = arr[i];
      }
    }
    return max;
  }

  protected float getMin(float[] arr) {
    float min = arr[0];
    for(int i = 1; i < arr.Length; i++) {
      if(arr[i] < min) {
        min = arr[i];
      }
    }
    return min;
  }

  protected float getSum(float[] arr) {
    float s = 0;
    for(int i = 0; i < arr.Length; i++) {
      s +=arr[i];
    }
    return s;
  }


}
