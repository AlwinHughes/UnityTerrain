using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGroup : MonoBehaviour {

  [SerializeField]
  private MeshFilter[] mesh_filters; 
  Chunk[,] chunks;

  private MeshFilter getMeshFilter(int i, int j) {
    return mesh_filters[ (width + 1) * i + j];
  }

  private void setMeshFilter(int i, int j, MeshFilter fm) {
    Debug.Log("setting " + i + " " + j + " | index: " + ((width+1) * i + j));
    mesh_filters[ (width + 1) * i + j] = fm;
  }

  //number of chunks
  [Range(0,9)]
  public int width = 1;
  [Range(0,9)]
  public int height = 1;

  //resolution of chunks
  [Range(2,100)]
  public int res = 10;

  void OnValidate() {
    init();

    for(int i = 0; i <= width; i++) {
      for(int j = 0; j <= height; j++) {
        generateChunk(i,j);
      }
    }
  }

  void init() {

    if(res == 0) {
      return;
    }

    if(mesh_filters == null || mesh_filters.Length == 0) {
      Debug.Log("new mesh 1");
      Debug.Log(mesh_filters == null);
      mesh_filters = new MeshFilter[(width + 1) * (height+1)];
    }


    
    /*
    if(mesh_filters.Length != width * height) {
      Debug.Log("new mesh 2");
      MeshFilter[,] old_ms = mesh_filters.Clone() as MeshFilter[,]; 
      mesh_filters = new MeshFilter[width + 1, height+1];

      //coppies old mesh filters
      for(int i = 0; i <= width; i++) {
          if(i < old_ms.GetLength(0)) {
            for(int j = 0; j <= height; j++) {
              if(j < old_ms.GetLength(1)) {
                mesh_filters[i,j] = old_ms[i,j];
              }
            }
          }
      }
      old_ms = null;
    }
    */




    chunks = new Chunk[width +1, height + 1];
    for(int i = 0; i <= width; i++) {
      for(int j = 0; j <= height; j++) {

        if(getMeshFilter(i,j) == null) {
          GameObject meshobj = new GameObject("mesh");
          meshobj.transform.parent = transform;

          Vector3 pos = new Vector3(meshobj.transform.position.x + i, meshobj.transform.position.y, meshobj.transform.position.z + j);

          meshobj.transform.position = pos;

          meshobj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
          setMeshFilter(i,j, meshobj.AddComponent<MeshFilter>());
          getMeshFilter(i,j).mesh = new Mesh();
        } else {
          Debug.Log("mesh already exists");
          Debug.Log("skiped " + i + " " + j + " | index: " + ((width +1) * i + j));
        }

        chunks[i,j] = new Chunk(getMeshFilter(i,j).sharedMesh,i,j,res);
      }
    }

  }


  private void generateChunk(int x, int y) {
    chunks[x,y].constructMesh();
  }

  /*public ChunkGroup(int width, int height, int res) {
    this.width = width;
    this.height = height;
    this.res = res;

  }
  */

}
