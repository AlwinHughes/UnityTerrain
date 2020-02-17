using System.Collections;
using System;
using UnityEngine;

public class ChunkGroup : MonoBehaviour {


 // [SerializeField]
//MFFlatArray mesh_filters;

  [SerializeField]
  Chunk[] chunks;

  Chunk getChunk(int i, int j) {
    return chunks[height * i + j];
  }

  void setChunk(int i, int j, Chunk c) {
    chunks[height * i + j] = c;
  }

  /*
  [SerializeField]
  MeshFilter[] mesh_filters;

  MeshFilter getMF(int i, int j) {
    //Debug.Log("get "+  i + " " +j + " index: " + (width * i + j));
    return mesh_filters[height * i + j];
  }

  void setMF(int i, int j, MeshFilter mf) {
    mesh_filters[height * i + j] = mf;
  }
  

  [SerializeField]
  GameObject[] mesh_obj;

  GameObject getMO(int i, int j) {
    //Debug.Log("get "+  i + " " +j + " index: " + (width * i + j));
    return mesh_obj[height * i + j];
  }

  void setMO(int i, int j, GameObject mf) {
    mesh_obj[height * i + j] = mf;
  }
  */

  [SerializeField]
  private int current_width;
  [SerializeField]
  private int current_height;

  //number of chunks
  [Range(1,9)]
  public int width = 2;
  [Range(1,9)]
  public int height = 2;

  //resolution of chunks
  [Range(2,100)]
  public int res = 10;

  void OnValidate() {
    init();

    for(int i = 0; i < width; i++) {
      for(int j = 0; j < height; j++) {
        generateChunk(i,j);
      }
    }
  }

  void init() {

    if(res == 0) {
      return;
    }

    Debug.Log("current width " + current_width + " current height " + current_height);
    Debug.Log("width " + width + " height " + height);


    if(chunks == null || chunks.Length == 0 ) {
      Debug.Log("re creating chunks");
      chunks = new Chunk[width * height];
    } else if ( chunks != null && ( current_width != width || current_height != height)) {
        Debug.Log("resizing mesh");
        chunks = new Chunk[width * height];
      }

    


    /*
    if(false && current_width != width || current_height != height) {
      Debug.Log("resizing mesh");

      MeshFilter[] old_data = mesh_filters.Clone() as MeshFilter[];
      mesh_filters = new MeshFilter[width*height];

      int min_width = Math.Min(current_width, width);
      int min_height = Math.Min(current_height, height);
      for(int i = 0; i < min_width; i++) {
        for(int j = 0; j < min_height; j++) {
          mesh_filters[min_width * i + j] = old_data[min_width* i + j];
        }
      }
      current_width = width;
      current_height = height;
    }
    */

    //chunks = new Chunk[width, height];
    for(int i = 0; i < width; i++) {
      for(int j = 0; j < height; j++) {

        //if(getMeshFilter(i,j) == null) {
        //if(getMF(i,j) == null){

          Vector3 pos = new Vector3(transform.position.x + i, transform.position.y, transform.position.z + j);

          chunks[height * i + j] = new Chunk(i,j, res, transform);

          /*
          GameObject meshobj = new GameObject("mesh");
          meshobj.transform.parent = transform;


          meshobj.transform.position = pos;

          meshobj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));

          setMF(i,j, meshobj.AddComponent<MeshFilter>());
         //setMeshFilter(i,j, meshobj.AddComponent<MeshFilter>());
          
          MeshFilter mf = getMF(i,j);
          mf.mesh = new Mesh();
          

          setMF(i,j, mf);
          //getMeshFilter(i,j).mesh = new Mesh();
          */
        //} 
        //chunks[i,j] = new Chunk(getMeshFilter(i,j).sharedMesh,i,j,res);
        //chunks[i,j] = new Chunk(getMF(i,j).sharedMesh,i,j,res);
      }
    }

  }


  private void generateChunk(int x, int y) {
    chunks[x * height + y].constructMesh();
  }

  /*public ChunkGroup(int width, int height, int res) {
    this.width = width;
    this.height = height;
    this.res = res;

  }
  */

}
