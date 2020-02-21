using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothGeometric : TerrainGenerator {

  public SmoothGeometric(float amplitude_ratio, float scale_ratio, int num_octaves){
    this.gen_opts = new SmoothGeomTerrainGenOpt(amplitude_ratio, scale_ratio, num_octaves);
    this.draw_editor=true;
  }

  private SmoothGeomTerrainGenOpt getGenOpts() {
    return (SmoothGeomTerrainGenOpt) this.gen_opts;
  }

  public SmoothGeometric(SmoothGeomTerrainGenOpt go){
    this.gen_opts = go;
  }

  override public void generateTerrain(NoiseOptions options) {

    noise_grid = new float[options.res, options.res];

    NoiseOptions o = new NoiseOptions(options);

    for(int i = 0; i < getGenOpts().num_octaves; i++) {
      noise_grid = add2DArr(noise_grid, NoiseGrid.genNoise(o));
      o.scale *= getGenOpts().scale_ratio;
      o.amplitude *= getGenOpts().amplitude_ratio;
    }
  }

  public override void applyTerrain(ref float[] existing_noise) {
    for(int i = 0; i < noise_grid.GetLength(0); i++) {
      for(int j = 0; j < noise_grid.GetLength(1); j++) {
        existing_noise[i + noise_grid.GetLength(0) * j] += noise_grid[i,j];


      }
    }
  }

}
