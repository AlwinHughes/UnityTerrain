using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGroupOpt : ScriptableObject {

  [Range(0,10)]
  public int height;
  [Range(0,10)]
  public int width;

  [Range(2,200)]
  public int res;

  public ChunkGroupOpt(int height, int width, int res) {
    this.height = height;
    this.width = width;
    this.res = res;
  }

}
