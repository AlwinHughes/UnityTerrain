using System.Collections;
using System;
using System.Collections;
using UnityEngine;

public class Chunk : MonoBehaviour {

  private int x;
  private int y;
  [Range(2,100)]
  public int res = 10;
  [Range(0f,10f)]
  public float scale = 3f;
  private float old_scale = 3f;

  [SerializeField]
  private float[] noise_grid;
  [SerializeField]
  private float[] original_noise_grid;
  private MeshFilter mesh_filter;

  [SerializeField]
  public GameObject mesh_obj;

  public ChunkterainOptions terrain_options;

  private Vector3[] verts;
  private int[] triangles;

  public Chunk(int x, int y, int res, Transform t) {
    Debug.Log("constructor");
    this.x = x;
    this.y = y;
    this.res = res;
    initMesh(t);
    copyInToNoiseGrid(NoiseGrid.genNoise(res,res, scale));
  }

  ~Chunk() {
    Debug.Log("destroying mesh");
    Destroy(mesh_obj);
  }

  void initMesh(Transform t) {
    Debug.Log("init mesh");
    mesh_obj = new GameObject("mesh");
    mesh_obj.transform.parent = t;
    Vector3 pos = new Vector3(t.position.x + x, t.position.y, t.position.z + y);
    mesh_obj.transform.position = pos;

    mesh_obj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));

    mesh_filter = mesh_obj.AddComponent<MeshFilter>();

    mesh_filter.mesh = new Mesh();
  }

  public void transformPosition(Vector3 pos) {
    this.transform.position = pos;
  }

  void OnValidate() {
    Debug.Log("on validate");
    if(res == null || res == 0) {
      res = 10;
    }
    if(mesh_obj == null) {
      initMesh(transform);
    }
    if(noise_grid == null || noise_grid.Length == 0 || old_scale != scale) {
      copyInToNoiseGrid(NoiseGrid.genNoise(res,res, scale));
      old_scale = scale;
    }

    
    //transformNoiseGrid();
    
    constructMesh();
  }

  public void constructMesh() {

    Debug.Log("construct mesh");

    if(noise_grid.Length != res * res) {
      copyInToNoiseGrid(NoiseGrid.genNoise(res,res, scale));
    }

    
    verts = new Vector3[res*res];
    triangles = new int[(res -1)*(res-1) *6];

    int tri_index = 0;
    int vert_index = 0;

    float inv_res = 1f/(res-1);
    for(int i = 0; i < res; i++) { 
      for(int j = 0; j < res; j++) {
        vert_index = i + res * j;

        verts[vert_index] = new Vector3(i* inv_res, noise_grid[vert_index], j * inv_res);

        if(i != res -1 && j != res -1) {

          triangles[tri_index] = vert_index;
          triangles[tri_index + 1] = vert_index + res;
          triangles[tri_index + 2] = vert_index + res + 1;

          triangles[tri_index + 3] = vert_index;
          triangles[tri_index + 4] = vert_index + res + 1;
          triangles[tri_index + 5] = vert_index + 1;

          tri_index += 6;
        }

      }
    }

    this.mesh_filter.sharedMesh.Clear();
    this.mesh_filter.sharedMesh.vertices = verts;
    this.mesh_filter.sharedMesh.triangles = triangles;
    this.mesh_filter.sharedMesh.RecalculateNormals();
  }

  private void copyInToNoiseGrid(float[,] noise2d) {
    noise_grid = new float[res*res];
    for(int i = 0; i < res; i++) { 
      for(int j = 0; j < res; j++) {
        noise_grid[i + res * j] = noise2d[i,j];
      }
    }
  }

  private void copyInToGrid(float[,] noise2d, ref float[] grid) {
    if(grid.Length != res*res) {
      grid = new float[res * res];
    }
    for(int i = 0; i < res; i++) { 
      for(int j = 0; j < res; j++) {
        grid[i + res * j] = noise2d[i,j];
      }
    }
  }

  public void resetNoiseGrid() {
    noise_grid = original_noise_grid.Clone() as float[];
  }


  public void transformNoiseGrid() {
    Debug.Log("Transform");
    float inv_res = 1f/(res-1);

    float max = getMax(noise_grid);
    float min= getMin(noise_grid);

    float mean = (max + min)/2f;

    for(int i = 0; i < res; i++) {
      for(int j = 0; j < res; j++) {
        float x = i*inv_res;
        float y = j*inv_res;
        noise_grid[i + res * j] = 
          mean * (float) Math.Pow( 1 - 16f * x * (1-x) * y * (1-y), 1) + 
         /*noise contribution */ noise_grid[i + res * j] * ((float) Math.Sqrt(x*(1 - x)*y*(1 - y))) * 4f;
      }
    }
  }

  float getMin(float[] arr) {
    float min = arr[0];
    for(int i = 1; i < arr.Length; i++) {
      if(arr[i] < min) {
        min = arr[i];
      }
    }
    return min;
  }

  float getMax(float[] arr) {
    float max = arr[0];
    for(int i = 1; i < arr.Length; i++) {
      if(arr[i] > max) {
        max = arr[i];
      }
    }
    return max;
  }

}
