using System.Collections;
using System;
using UnityEngine;

public static class NoiseGrid {


  public static float[,] genNoise(int width, int height, float max, float min, float scale) {

    float[,] noise_grid = new float[width, height];
    float v_scale = max - min;

    for(int i = 0; i < width; i++) {
      for(int j = 0; j < height; j++) {
        noise_grid[i,j] = min + v_scale * (Mathf.PerlinNoise(i * scale / width, j * scale/height) + 1f) * 0.5f ;
        //Debug.Log(noise_grid[i,j]);
      }
    }

    return noise_grid;
  }

  public static float[,] genNoise(int width, int height, float scale) {
    return genNoise(width, height, 1,0,scale);
  }

  public static float[,] genNoise(ChunkterainOptions options) {

    float[,] noise_grid = new float[options.res, options.res];

    for(int i = 0; i < options.numOctaves; i++) {
      noise_grid = add2DArr(noise_grid, genNoise(options.res, options.res, options.amplitudes[i], 0, options.scales[i]));
    }

    scale(ref noise_grid, 1f/getMax(noise_grid));

    return noise_grid;
  }

  private static float getMax(float[,] arr) {
    float max = arr[0,0];
    for(int i = 0; i < arr.GetLength(0); i++) {
      for(int j = 0; j < arr.GetLength(1); j++) {
        if(max < arr[i,j])
          max = arr[i,j];
      }
    }
    return max;
  }

  private static void scale(ref float[,] arr, float scale_by) {
    for(int i = 0; i < arr.GetLength(0); i++) {
      for(int j = 0; j < arr.GetLength(1); j++) {
        arr[i,j] *= scale_by;
      }
    }
  }

  private static float[,] add2DArr(float[,] a1, float[,] a2) {

    for(int i = 0; i < a1.GetLength(0); i++) {
      for(int j = 0; j < a1.GetLength(1); j++) {
        a1[i,j] += a2[i,j];
      }
    }
    return a1;
  }
}
