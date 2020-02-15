using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGrid {


  public static float[,] genNoise(int width, int height, float max, float min, float scale) {

    float[,] noise_grid = new float[width, height];
    float v_scale = max - min;

    for(int i = 0; i < width; i++) {
      for(int j = 0; j < height; j++) {
        noise_grid[i,j] = min + v_scale * Mathf.PerlinNoise(i * scale / width, j * scale/height);
        //Debug.Log(noise_grid[i,j]);
      }
    }


    return noise_grid;
  }

  public static float[,] genNoise(int width, int height, float scale) {
    return genNoise(width, height, 1,0,scale);
  }
}
