using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Chunk : MonoBehaviour {

  private int x;
  private int y;
  [Range(0f,10f)]

  [SerializeField]
  private float[] noise_grid;
  [SerializeField]
  private float[] original_noise_grid;
  private MeshFilter mesh_filter;

  public NoiseOptions noise_options;

  [SerializeField]
  public GameObject mesh_obj;

  [SerializeField]
  public bool noise_option_foldout;
  public bool generator_foldout;

  private Vector3[] verts;
  private int[] triangles;

  public TGopt tgopt;


  //[SerializeField]
  public TerrainGenerator[] generators;

  public Chunk(int x, int y, int res, Transform t) {
    Debug.Log("constructor");
    this.x = x;
    this.y = y;
    initMesh(t);
    noise_options = new NoiseOptions(0.7f, 50, 1f);

    original_noise_grid = new float[noise_options.res*noise_options.res];

    resetNoiseGrid();
    generators = new TerrainGenerator[] {new SmoothGeometric(2f, 0.5f, 3) };
    noise_options = new NoiseOptions(0.5f, res, 1f);

    generateTerrainIfNotReady();
    applyTerrain();
    constructMesh();
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

  public void init(Vector3 pos, NoiseOptions noise_options, TerrainGenerator[] tgs) {
    Debug.Log("chunk init");
    this.transform.position = pos;

    this.noise_options = noise_options;

    if(this.generators == null) {
      this.generators = tgs;
    }

    Debug.Log("chunk init2");
    original_noise_grid = new float[noise_options.res*noise_options.res];
    Debug.Log("chunk init3");
    generateTerrain();
    applyTerrain();
    constructMesh();
  }

  void OnValidate() {
    Debug.Log("on validate");

    if(mesh_obj == null) {
      Debug.Log("call init");
      initMesh(transform);
    }

    if(noise_options == null) {
      noise_options = new NoiseOptions(0.7f, 50, 1f);
    }

    if(generators == null || generators.Length == 0) {

      Debug.Log("making generators from tgopts");

      generators = new TerrainGenerator[tgopt.options.Length];

      for(int i = 0; i < tgopt.options.Length; i++) {
        generators[i] = GeneratorCaster.makeTG(tgopt.options[i],tgopt.types[i]);
      }


    } else {
      //todo later: make this better so it doesn't always re create the generators

      generators = new TerrainGenerator[tgopt.options.Length];
      for(int i = 0; i < tgopt.options.Length; i++) {
        generators[i] = GeneratorCaster.makeTG(tgopt.options[i],tgopt.types[i]);
      }

      /*
      for(int i = 0; i < generators.Length; i++) {
        generators[i] = GeneratorCaster.castTG(generators[i]);
      }
      */
    }


    //Debug.Log("vert length : " + mesh_filter.sharedMesh.triangles.Length);

    if(mesh_filter.sharedMesh.vertices.Length == 0) {
      Debug.Log("mesh null");
    }


    Debug.Log("doing something");
    original_noise_grid = new float[noise_options.res*noise_options.res];
    generateTerrainIfNotReady();
    applyTerrain();
    constructMesh();
  }

  public void createGenerators() {


    for(int i = 0; i < generators.Length; i++) {
      generators[i] = GeneratorCaster.castTG(generators[i]);
    }
  }

  public void onNoiseOptionsChanged() {
    if(noise_options.res > 1) {
      original_noise_grid = new float[noise_options.res*noise_options.res];
      generateTerrain();
      applyTerrain();
      constructMesh();
    } else {
      Debug.Log("ignoring as res is 0 or 1");
    }
  }

  public void onTerrainOptionsChange() {
    Debug.Log("on terraain options changed");
    if(noise_options.res > 1) {
      original_noise_grid = new float[noise_options.res*noise_options.res];
      generateTerrain();
      applyTerrain();
      constructMesh();
    } else {
      Debug.Log("ignoring as res is 0 or 1");
    }
  }


  //calculates the terrain for each generator
  public void generateTerrain() {
    Debug.Log("generate terrain");
    for(int i = 0; i < generators.Length; i++) {
      generators[i].generateTerrain(noise_options);
    }
  }

  public void generateTerrainIfNotReady() {
    Debug.Log("generate terrain if not ready");
    for(int i = 0; i < generators.Length; i++) {
      if(generators[i].noise_store == null){
        Debug.Log("generator " + i + " was not ready");
        generators[i].generateTerrain(noise_options);
      }
    }
  }


  //applies the terrain for each generator
  public void applyTerrain() {
    Debug.Log("apply terrain");
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
}
