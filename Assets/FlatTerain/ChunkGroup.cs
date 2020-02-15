using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGroup : MonoBehaviour {

  //number of chunks
  [Range(0,9)]
  public int width = 1;
  [Range(0,9)]
  public int height = 1;

  //resolution of chunks
  [Range(2,100)]
  public int res = 10;

  [SerializeField]
  MeshFilter[,] mesh_filters;
  Chunk[,] chunks;


  void OnValidate() {
    init();
  }

  void init() {

    if(res == 0) {
      return;
    }


    if(mesh_filters == null || mesh_filters.GetLength(0) == 0) {
      Debug.Log("new mesh");
      mesh_filters = new MeshFilter[width + 1, height+1];
    }

    if(mesh_filters.GetLength(0) != width + 1 || mesh_filters.GetLength(1) != height + 1) {
      Debug.Log("new mesh");
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
    }

    chunks = new Chunk[width +1, height + 1];
    for(int i = 0; i <= width; i++) {
      for(int j = 0; j <= height; j++) {

        if(mesh_filters[i,j] == null) {
          GameObject meshobj = new GameObject("mesh");
          meshobj.transform.parent = transform;

          Vector3 pos = new Vector3(meshobj.transform.position.x + i, meshobj.transform.position.y, meshobj.transform.position.z + j);

          meshobj.transform.position = pos;

          meshobj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
          mesh_filters[i,j] = meshobj.AddComponent<MeshFilter>();
          mesh_filters[i,j].mesh = new Mesh();
        }

        chunks[i,j] = new Chunk(mesh_filters[i,j].sharedMesh,i,j,res);
      }
    }

    for(int i = 0; i <= width; i++) {
      for(int j = 0; j <= height; j++) {
        generateChunk(i,j);
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
