using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerrainFace {

  Mesh mesh;
  int res;
  float radius = 1f;

  Vector3 localUp;
  Vector3 axisA;
  Vector3 axisB;

  PlanetOptions planet_options;

  float[,] noise_grid;

  public TerrainFace(Mesh mesh, int res, Vector3 localUp, float radius) {
    this.mesh = mesh;
    this.res = res;
    this.localUp = localUp;

    this.axisA = new Vector3(this.localUp.y, this.localUp.z, this.localUp.x);
    this.axisB = Vector3.Cross(localUp, axisA);

    this.planet_options = new PlanetOptions(3f, 2f, 4f);
    this.noise_grid = new float[res,res];
    this.noise_grid = NoiseGrid.genNoise(res,res, this.planet_options.max, planet_options.min, planet_options.scale);
    this.radius = radius;
  }

  public TerrainFace(Mesh mesh, int res, Vector3 localUp, PlanetOptions planet_options) {

    this.mesh = mesh;
    this.res = res;
    this.localUp = localUp;

    this.axisA = new Vector3(this.localUp.y, this.localUp.z, this.localUp.x);
    this.axisB = Vector3.Cross(localUp, axisA);

    this.planet_options = planet_options;
    this.noise_grid = new float[res,res];
    this.noise_grid = NoiseGrid.genNoise(res,res, this.planet_options.max, planet_options.min, planet_options.scale);

  }

  public void constructMesh() {

    Vector3[] verts = new Vector3[res*res];
    int[] triangles = new int[(res -1)*(res-1) *6];
    int tri_index = 0;
    int vert_index = 0;


    for(int i = 0; i < res; i++) { 
      for(int j = 0; j < res; j++) {
        vert_index = i + j * res;
        Vector2 percent = new Vector2(i,j) / (res -1);

        Vector3 pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f)* 2 * axisB;

        Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
        verts[vert_index] = pointOnUnitSphere * noise_grid[i,j];


        //verts[vert_index] = pointOnUnitCube * noise_grid[i,j];

        if(i != res -1 && j != res -1) {

          triangles[tri_index] = vert_index;
          triangles[tri_index + 1] = vert_index + res + 1;
          triangles[tri_index + 2] = vert_index + res;

          triangles[tri_index + 3] = vert_index ;
          triangles[tri_index + 4] = vert_index + 1;
          triangles[tri_index + 5] = vert_index + res + 1;

          tri_index += 6;
        }

      }
    }

    this.mesh.Clear();
    this.mesh.vertices = verts;
    this.mesh.triangles = triangles;
    this.mesh.RecalculateNormals();
  }


}
