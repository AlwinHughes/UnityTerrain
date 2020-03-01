using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgeGenerator : TerrainGenerator {

  public RidgeGenerator(RidgeGenOpt rgo) {
    this.gen_opts = rgo;
    this.draw_editor = true;
    this.gen_type = GeneratorType.Ridge;
  }

  public RidgeGenerator(TerrainGenerator tg) {
    this.gen_opts = (RidgeGenOpt) tg.gen_opts;
    this.draw_editor = true;
    this.gen_type = GeneratorType.Ridge;
    this.noise_store = tg.noise_store;
  }

  public RidgeGenerator(float power, float scale) {
    this.gen_opts = new RidgeGenOpt(power, scale);
    this.draw_editor = true;
    this.gen_type = GeneratorType.Ridge;
  }

  private RidgeGenOpt getGenOpts() {
    return (RidgeGenOpt) this.gen_opts;
  }

  override public void generateTerrain(NoiseOptions options) {
    base.generateTerrain(options);

    if(getGenOpts().enabled) {
      NoiseOptions o = new NoiseOptions(options);
      o.amplitude = 1f;
      o.scale = getGenOpts().scale;

      noise_store.copyFrom2D(NoiseGrid.genNoise(o));
      //noise_grid = NoiseGrid.genNoise(o);

      //make ridges
      for(int i = 0; i < o.res; i++) {
        for(int j = 0; j < o.res; j++) {
          noise_store.set(i,j, Mathf.Pow(0.5f - Mathf.Abs(0.5f - noise_store.get(i,j)), getGenOpts().power));
          //noise_grid[i,j] = Mathf.Pow(0.5f - Mathf.Abs(0.5f - noise_grid[i,j]), getGenOpts().power);
        }
      }
    }
  }

  public override void applyTerrain(ref float[] existing_noise) {

    if(getGenOpts().enabled) {
      for(int i = 0; i < noise_store.getWidth(); i++) {
        for(int j = 0; j < noise_store.getHeight(); j++) {
          //existing_noise[i + noise_grid.GetLength(0) * j] += noise_grid[i,j];
          //existing_noise[i + noise_grid.GetLength(0) * j] = (noise_grid[i,j] +1)* existing_noise[i + noise_grid.GetLength(0) * j];
          existing_noise[i + noise_store.getWidth() * j] = (noise_store.get(i,j) +1) * existing_noise[i + noise_store.getWidth() * j];
        }
      }

    }

  }

  private float getMax(float[,] arr) {

    float max = arr[0,0];
    for(int i = 0; i < arr.GetLength(0); i++) {
      for(int j = 0; j < arr.GetLength(1); j++) {
        if(arr[i,j] > max) {
          max = arr[i,j];
        }
      }
    }

    return max;
  }

  private float getMin(float[,] arr) {

    float min = arr[0,0];
    for(int i = 0; i < arr.GetLength(0); i++) {
      for(int j = 0; j < arr.GetLength(1); j++) {
        if(arr[i,j] <  min) {
          min = arr[i,j];
        }
      }
    }

    return min;
  }

}
