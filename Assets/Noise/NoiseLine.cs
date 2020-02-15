using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseLine {

  public static float[] getNoiseLine(float start, float end, int num_points, float scale) {

    float[] line = new float[num_points];
    line[0] = start;
    line[num_points - 1] = end;

    for(int i = 1; i < num_points; i++){
      float x = i/num_points;
      line[i] = (start * x) + (end * (1 - x)) + 4 * x * (x -1) * Mathf.PerlinNoise(scale * i /num_points, 0f);
    };

    return line;
  }


}
