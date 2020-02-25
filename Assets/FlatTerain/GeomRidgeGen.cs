using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeomRidgeGen : TerrainGenerator {

  public GeomRidgeGen(GeomRidgeGenOpt opt) {
    this.gen_opts = opt;
    this.draw_editor = true;
  }

  private GeomRidgeGenOpt getGenOpts() {
    return (GeomRidgeGenOpt ) this.gen_opts;
  }

  public GeomRidgeGen(float power, float scale_ratio, float scale, float amplitude_ratio, int num_octaves) {
    this.gen_opts = new GeomRidgeGenOpt(power, scale_ratio, scale, amplitude_ratio, num_octaves);
    this.draw_editor = true;
  }

  override public void generateTerrain(NoiseOptions options) {

    NoiseOptions o = new NoiseOptions(options);
    o.amplitude = 1f;
    o.scale = getGenOpts().scale;

    float[,] new_noise = new float[o.res, o.res];

    
    noise_grid = new float[o.res, o.res];
    /*
    for(int i = 0; i < o.res; i++) {
      for(int j = 0; j < o.res; j++) {
        noise_grid[i,j] = 0;
      }
    }
    */


    for(int k = 0; k < getGenOpts().num_octaves; k++) {
      new_noise = NoiseGrid.genNoise(o);
      
      //make ridges
      for(int i = 0; i < o.res; i++) {
        for(int j = 0; j < o.res; j++) {

          //noise_grid[i,j] = 0.5f + 0.5f*Mathf.Pow(1f - 2f *o.amplitude* Mathf.Abs(0.5f*o.amplitude - noise_grid[i,j]), getGenOpts().power);
          
          //noise_grid[i,j] +=  Mathf.Abs(noise_grid[i,j] - 0.5f * o.amplitude);
          
          noise_grid[i,j] += Mathf.Pow(o.amplitude - Mathf.Abs(new_noise[i,j] - 0.5f * o.amplitude), getGenOpts().power);
          
        }
      }
      o.scale += getGenOpts().scale_ratio; o.amplitude *= getGenOpts().amplitude_ratio;
    }


    float max = getMax(noise_grid);
    float min = getMin(noise_grid);

    for(int i = 0; i < o.res; i++) {
      for(int j = 0; j < o.res; j++) {
        noise_grid[i,j] = (noise_grid[i,j] - min) / max + 1f;
      }
    }

    Debug.Log("Max: " + getMax(noise_grid));
    Debug.Log("Min: " + getMin(noise_grid));
  }


  public override void applyTerrain(ref float[] existing_noise) {

    if(getGenOpts().enabled) {
      for(int i = 0; i < noise_grid.GetLength(0); i++) {
        for(int j = 0; j < noise_grid.GetLength(1); j++) {
          existing_noise[i + noise_grid.GetLength(0) * j] *= noise_grid[i,j];
          //existing_noise[i + noise_grid.GetLength(0) * j] = (noise_grid[i,j] +1)* existing_noise[i + noise_grid.GetLength(0) * j];
        }
      }
    }

  }

}
