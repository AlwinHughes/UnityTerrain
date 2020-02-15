using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk {

  private int x;
  private int y;
  private int res;

  private float[,] noise_grid;

  private Mesh mesh;
  //private MeshFilter mesh_filter;
  

  


  private Vector3[] verts;
  private int[] triangles;

  public Chunk(Mesh mesh, int x, int y, int res) {
    this.x = x;
    this.y = y;
    this.res = res;
    this.mesh = mesh;
  }

  public void constructMesh() {

    generateNoise();

    verts = new Vector3[res*res];
    triangles = new int[(res -1)*(res-1) *6];

    int tri_index = 0;
    int vert_index = 0;

    float inv_res = 1f/(res-1);
    for(int i = 0; i < res; i++) { 
      for(int j = 0; j < res; j++) {
        vert_index = i + res * j;


        //verts[vert_index] = new Vector3(i/(res-1), j/(res-1), noise_grid[i,j]);
        
        verts[vert_index] = new Vector3(i* inv_res, noise_grid[i,j], j * inv_res);


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

    this.mesh.Clear();
    this.mesh.vertices = verts;
    this.mesh.triangles = triangles;
    this.mesh.RecalculateNormals();
  }

  private void generateNoise() {
    noise_grid = new float[res,res];
    noise_grid = NoiseGrid.genNoise(res,res, 3f);
  }




}
