using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGroupOpt : ScriptableObject {

  [Range(0,10)]
  public int length;
  [Range(0,10)]
  public int width;

  [Range(0f,10f)]
  public float side_length;

  [Range(0f,10f)]
  public float side_width;


  public ChunkGroupOpt(int length, int width, float side_length, float side_width) {
    this.length = length;
    this.width = width;
    this.side_length = side_length;
    this.side_width = side_width;
  }

}
