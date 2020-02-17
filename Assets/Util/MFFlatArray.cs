using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MFFlatArray {

  [SerializeField]
  private MeshFilter[] data;

  [SerializeField]
  private int[] dims;

  public MFFlatArray(int x, int y) {
    this.dims = new int[] {x, y};
    this.data = new MeshFilter[x*y];
  }

  public MFFlatArray(int[] dims) {
    this.dims = dims;
    this.data = new MeshFilter[calcArrLength()];
  }

  private int calcArrLength(){
    int l = 1;
    for(int i = 0; i < data.Length; i++) {
      l *= dims[i];
    }
    return l;
  }

  private int calcAnyArrLength(int[] arr) {
    int l = 1;
    for(int i = 0; i < arr.Length; i++) {
      l *= arr[i];
    }
    return l;
  }

  public int[] getDims() { return dims; }

  public int getRank() { return dims.Length; }

  public MeshFilter get(int i, int j) {
    if(dims.Length != 2) {
      throw new Exception("wrong length accessor");
    }
    return data[dims[0] * i + j];
  }

  public MeshFilter get(int[] accessor) {
    if(accessor.Length != dims.Length) {
      throw new Exception("wrong length accessor");
    }

    int index = accessor[accessor.Length-1];
    for(int i = 0; i < accessor.Length-1; i++) {
      index += dims[i] * accessor[i];
    }

    return data[index];
  }


  public void set(int i, int j, MeshFilter val) {
    if(dims.Length != 2) {
      throw new Exception("wrong length accessor");
    }


    data[dims[0] * i + j] = val;
  }

  public void set(int[] accessor, MeshFilter val) {
    if(accessor.Length != dims.Length) {
      throw new Exception("wrong length accessor");
    }

    int index = accessor[accessor.Length-1];
    for(int i = 0; i < accessor.Length-1; i++) {
      index += dims[i] * accessor[i];
    }

    data[index] = val;
  }

  private int[] getMin(int[] a1, int[] a2) {
    if(a1.Length != a2.Length) {
      throw new Exception("array lengths do not agree");
    }

    int[] ret = new int[a1.Length];
    for(int i = 0; i < a1.Length; i++) {
      ret[i] = Math.Min(a1[i], a2[i]);
    }

    return ret;
  }

  public void resize(int[] new_dims, bool copy) {

    if(copy && dims.Length == 2) {
      //copying data from old to new currently only works for 2d arrays
      MeshFilter[] old_data = data.Clone() as MeshFilter[]; data = new MeshFilter[calcAnyArrLength(new_dims)];
      int[] copy_dims = getMin(new_dims, dims);

      for(int i = 0; i < copy_dims[0]; i++) {
        for(int j = 0; j < copy_dims[1]; j++) {
          data[copy_dims[0] * i + j] = old_data[copy_dims[0] * i + j];
        }
      }

    } else {
      dims = new_dims;
      data = new MeshFilter[calcArrLength()];
    }
  }


}
