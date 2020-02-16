using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

  [SerializeField]
  MeshFilter[] mesh_filters;
  TerrainFace[] terrain_faces;

  [Range(2,256)]
  public int res = 20;

  [Range(0.1f,5f)]
  public float radius = 3f;
  
  [Range(1f,10f)]
  public float scale = 1f;


  PlanetOptions planet_options = new PlanetOptions(3f, 2f, 4f);

  void OnValidate() {
    init();
    generateMesh();
  }



  void init(){

    Debug.Log("init");

    if(mesh_filters == null || mesh_filters.Length == 0) {
      Debug.Log("new mesh filters");
      mesh_filters = new MeshFilter[6];
    }
    this.terrain_faces = new TerrainFace[6];

    Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

    //create radius of each vertex
    for(int i = 0; i < vertex_radiuses.Length; i++) {
       vertex_radiuses[i] = planet_options.min + (planet_options.max - planet_options.min) * Random.Range(0f, 1f);
    }


    for(int i = 0; i < 6; i++) {

      if(mesh_filters[i] == null) {
        GameObject meshobj = new GameObject("mesh");
        meshobj.transform.parent = transform;

        meshobj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
        mesh_filters[i] = meshobj.AddComponent<MeshFilter>();
        mesh_filters[i].mesh = new Mesh();

      }
        //terrain_faces[i] = new TerrainFace(mesh_filters[i].sharedMesh, res, directions[i], radius);
        terrain_faces[i] = new TerrainFace(mesh_filters[i].sharedMesh, res, directions[i], planet_options);
    }
  }


  void generateMesh() {
    Debug.Log("gen mesh");
    foreach( TerrainFace tf in terrain_faces) {
      tf.constructMesh();
    }
  }

  /*
   * Verticies:
   *
   * top:
   *          0         1
   *            ---------
   *           |         |
   *           |         |
   *           |         |
   *            ---------
   *          2         3
   *
   * bottom ( also vied from the top):
   *
   *          4          5
   *            ---------
   *           |         |
   *           |         |
   *           |         |
   *            ---------
   *          6          7
   *
   */

  //containes the radius of the 8 verticies of a cube
  //order starts at the top at the back left clockweise then does the same on the bottom
  public float[] vertex_radiuses = {0f,0f,0f,0f,0f,0f,0f,0f};
  public int[,] edges_index = {
    /* top edges*/{0,1}, {1,2}, {2,3}, {3,0},
    /* vertical edges on side*/ {0,4}, {1,5}, {2,6}, {3,7}, 
    /* bottom edges*/ {4,5}, {5,6}, {6,7}, {7,4}};
  public int[,] faces_vertex_index = { 
    /*top*/{0,1,2,3},
    /*left*/{0,3,6,4},
    /*front*/{3,2,6,7}, 
    /*right*/{2,1,7,6},
    /*back*/{0,1,4,5},
    /*bottom*/{4,5,7,6}};


}
