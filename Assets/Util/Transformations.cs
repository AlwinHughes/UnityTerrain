using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Transformations {

  static float zeroEdgesQuadratic(float x, float y, float val) {
    return 16 * x * (x-1) * y * (y-1)* val;
  }

}
