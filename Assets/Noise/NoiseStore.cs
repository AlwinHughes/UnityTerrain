using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class NoiseStore {

  [SerializeField]
  private int width;
  [SerializeField]
  private int height;

  [SerializeField]
  private float[] store;

  public NoiseStore(){}

  public NoiseStore(int width, int height) {
    this.width = width;
    this.height = height;
    this.store = new float[width * height];
  }

  public float get(int i, int j) {
    return store[i + width * j];
  }

  public void set(int i, int j, float f) {
    store[i + width * j] = f;
  }

  public void setAdd(int i, int j, float f) {
    store[i + width * j] += f;
  }

  public bool isReady() {
    Debug.Log("is ready");
    return store != null;
  }

  public void setAdd2D(float[,] a2) {

    for(int i = 0; i < width; i++) {
      for(int j = 0; j < height; j++) {
        store[i + j * width] += a2[i,j];
      }
    }
  }


  public void copyFrom2D(float[,] a2) {

    if(a2 == null || a2.GetLength(0) != width || a2.GetLength(1) != height) {
      width = a2.GetLength(0);
      height = a2.GetLength(1);
      store = new float[a2.GetLength(0) * a2.GetLength(1)];
    }

    for(int i = 0; i < width; i++) {
      for(int j = 0; j < height; j++) {
        store[i + j * width] = a2[i,j];
      }
    }
  }

  public int getWidth() { return width; }
  public int getHeight() { return height; }

  public float getMax() {

    float max = store[0];
    for(int i = 0; i < store.Length; i++) {
      if(store[i] > max) {
        max = store[i];
      }
    }
    return max;
  }


  public float getMin() {

    float min = store[0];
    for(int i = 0; i < store.Length; i++) {
      if(store[i] < min) {
        min = store[i];
      }
    }
    return min;
  }

}
