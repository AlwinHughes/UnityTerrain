using System.Collections;
using System;
using System.Collections;
using UnityEngine;

public class Chunk : MonoBehaviour {

  private int x;
  private int y;
  [Range(0f,10f)]
  private float old_scale = 3f;

  [SerializeField]
  private float[] noise_grid;
  [SerializeField]
  private float[] original_noise_grid;
  private MeshFilter mesh_filter;

  public NoiseOptions noise_options;

  [SerializeField]
  public GameObject mesh_obj;

  [SerializeField]
  public ChunkterainOptions terrain_options;
  public bool noise_option_foldout;
  public bool generator_foldout;

  private Vector3[] verts;
  private int[] triangles;

  public TerrainGenerator[] generators;

  public Chunk(int x, int y, int res, Transform t) {
    Debug.Log("constructor");
    this.x = x;
    this.y = y;
    initMesh(t);
    //terrain_options = new ChunkterainOptions(new float[]{3f}, new float[] {1f}, res);
    //copyInToGrid(NoiseGrid.genNoise(terrain_options), ref original_noise_grid);
    resetNoiseGrid();
    generators = new TerrainGenerator[] {new SmoothGeometric(2f, 0.5f, 3) };
    noise_options = new NoiseOptions(3f, res, 1f);
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

    /*if(terrain_options == null) {
      terrain_options = new ChunkterainOptions(new float[]{3f}, new float[] {1f}, 10);
      }
      */

    if(mesh_obj == null) {
      initMesh(transform);
    }

    if(noise_options == null) {
      noise_options = new NoiseOptions(3f, 20, 1f);
    }

    /*
       if(original_noise_grid == null || original_noise_grid.Length != noise_options.res*noise_options.res) {
       original_noise_grid = new float[noise_options.res*noise_options.res];
       }
       */

    if(generators == null || generators.Length == 0) 
      generators = new TerrainGenerator[] {new SmoothGeometric(0.33f, 3f, 3), new RidgeGenerator(2f, 2f), new YTransformGen(false), new BoundsGen(-0.5f, 0.5f)};
    //generators = new TerrainGenerator[] { new RidgeGenerator(2f)}; }

    onTerrainOptionsChange();
  }

public void onTerrainOptionsChange() {
  original_noise_grid = new float[noise_options.res*noise_options.res];

  generateTerrain();
  applyTerrain();

  constructMesh();
}

//calculates the terrain for each generator
public void generateTerrain() {
  for(int i = 0; i < generators.Length; i++) {
    generators[i].generateTerrain(noise_options);
  }
}

//applies the terrain for each generator
public void applyTerrain() {
  for(int i = 0; i < generators.Length; i++) {
    generators[i].applyTerrain(ref original_noise_grid);
  }
  resetNoiseGrid();
}

public void constructMesh() {

  Debug.Log("construct mesh");

  verts = new Vector3[noise_options.res * noise_options.res];
  triangles = new int[(noise_options.res-1)*(noise_options.res-1) *6];

  int tri_index = 0;
  int vert_index = 0;

  float inv_res = 1f/(noise_options.res-1);
  for(int i = 0; i < noise_options.res; i++) { 
    for(int j = 0; j < noise_options.res; j++) {
      vert_index = i + noise_options.res * j;

      verts[vert_index] = new Vector3(i* inv_res, noise_grid[vert_index], j * inv_res);

      if(i != noise_options.res -1 && j != noise_options.res -1) {

        triangles[tri_index] = vert_index;
        triangles[tri_index + 1] = vert_index + noise_options.res;
        triangles[tri_index + 2] = vert_index + noise_options.res + 1;

        triangles[tri_index + 3] = vert_index;
        triangles[tri_index + 4] = vert_index + noise_options.res + 1;
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
  noise_grid = new float[noise_options.res*noise_options.res];
  for(int i = 0; i < noise_options.res; i++) { 
    for(int j = 0; j <noise_options.res; j++) {
      noise_grid[i + noise_options.res * j] = noise2d[i,j];
    }
  }
}

private void copyInToGrid(float[,] noise2d, ref float[] grid) {
  if(grid.Length != noise_options.res*noise_options.res) {
    grid = new float[noise_options.res * noise_options.res];
  }
  for(int i = 0; i < noise_options.res; i++) { 
    for(int j = 0; j < noise_options.res; j++) {
      grid[i + noise_options.res * j] = noise2d[i,j];
    }
  }
}

public void resetNoiseGrid() {
  noise_grid = original_noise_grid.Clone() as float[];
}


public void transformNoiseGrid() {
  Debug.Log("Transform");
  float inv_res = 1f/(noise_options.res-1);

  float max = getMax(noise_grid);
  float min= getMin(noise_grid);

  float mean = (max + min)/2f;

  for(int i = 0; i < noise_options.res; i++) {
    for(int j = 0; j < noise_options.res; j++) {
      float x = i*inv_res;
      float y = j*inv_res;

      noise_grid[i + noise_options.res * j] = 
        //mean * (float) Math.Pow( 1 - 16f * x * (1-x) * y * (1-y), 1) + 
        /*noise contribution */ noise_grid[i + noise_options.res * j] * ((float) (x*(1 - x)*y*(1 - y))) * 16f;

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
/*
   public void updateVerts() {
   Debug.Log("generate terrain");

   int vert_index = 0;
   float inv_res = 1f/(terrain_options.res-1);
   for(int i = 0; i < terrain_options.res; i++) { 
   for(int j = 0; j < terrain_options.res; j++) {
   vert_index = i + terrain_options.res * j;

   verts[vert_index] = new Vector3(i* inv_res, noise_grid[vert_index], j * inv_res);
   }
   }
   }

   public void updateTriangles() {

   int tri_index = 0;
   int vert_index = 0;
   for(int i = 0; i < terrain_options.res; i++) { 
   for(int j = 0; j < terrain_options.res; j++) {
   vert_index = i + terrain_options.res * j;


   if(i != terrain_options.res -1 && j != terrain_options.res -1) {

   triangles[tri_index] = vert_index;
   triangles[tri_index + 1] = vert_index + terrain_options.res;
   triangles[tri_index + 2] = vert_index + terrain_options.res + 1;

   triangles[tri_index + 3] = vert_index;
   triangles[tri_index + 4] = vert_index + terrain_options.res + 1;
   triangles[tri_index + 5] = vert_index + 1;

   tri_index += 6;
   }

   }

   }
   */
}
