using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GradientFinder {

  public static float[] findGradient(float[] arr, int width, int height) {
    float[] ret = new float[width * height];

    ret[0] = Mathf.Max(arr[0] - arr[1], arr[0] - arr[width]);
    //ret[width - 1] = Mathf.Max(arr[width -1] - arr[width -
    
    for(int i = 0; i < width; i++) {
      for(int j = 0; j < height; j++){
        if(i > 0 && i < width -1 && j > 0 && j < height -1) {
        /*ret[i + width * j] = Mathf.Max(
            Mathf.Max(
              ret[getAbove(i + width * j, width, height)] -  ret[i + width * j],
              ret[getBelow(i + width * j)] -  ret[i + width * j]
              ),
            Mathf.Max(
              ret[getLeft(i + width * j)] -  ret[i + width * j],
              ret[getRight(i + width * j)] -  ret[i + width * j]
              ));
              */
        }
      }
    }
    return null;
  }

  private static int getAbove(int cur, int width, int height) {
    return cur + height;
  }

  private static int getBelow(int cur, int width, int height) {
    return cur - height;
  }

  private static int getLeft(int cur, int width, int height) {
    return cur -1;
  }

  private static int getRight(int cur, int width, int height) {
    return cur +1;
  }





  /*
   *       (0, height -1)         (width -1, height -1)
   *
   *
   *
   *
   *
   *       (0,0)                  (width -1,0)
   *
   *
   *
   */
}
