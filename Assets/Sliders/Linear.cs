using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Linear {

  public static float getPoint(float x, float max) {
    return 1 - x/max;
  }

}
