﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeomRidgeGen : TerrainGenerator {

  public GeomRidgeGen(GeomRidgeGenOpt opt) {
    this.gen_opts = opt;
    this.draw_editor = true;
    this.gen_type = GeneratorType.GeomRidge;
  }

  private GeomRidgeGenOpt getGenOpts() {
    return (GeomRidgeGenOpt ) this.gen_opts; }

  public GeomRidgeGen(float power, float scale_ratio, float scale, float amplitude_ratio, int num_octaves, bool multiply) {
    this.gen_opts = new GeomRidgeGenOpt(power, scale_ratio, scale, amplitude_ratio, num_octaves, multiply);
    this.draw_editor = true;
    this.gen_type = GeneratorType.GeomRidge;
  }

  public GeomRidgeGen(TerrainGenerator tg) {
    this.gen_opts = (GeomRidgeGenOpt) tg.gen_opts;
    this.draw_editor = true;
    this.gen_type = GeneratorType.GeomRidge;
    this.noise_store = tg.noise_store;
  }

  override public void generateTerrain(NoiseOptions options) {
    base.generateTerrain(options);

    NoiseOptions o = new NoiseOptions(options);
    o.amplitude = 1f;
    o.scale = getGenOpts().scale;

    float[,] new_noise = new float[o.res, o.res];

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
          
          noise_store.setAdd(i,j, Mathf.Pow(o.amplitude - Mathf.Abs(new_noise[i,j] - 0.5f * o.amplitude), getGenOpts().power));
          //noise_grid[i,j] += Mathf.Pow(o.amplitude - Mathf.Abs(new_noise[i,j] - 0.5f * o.amplitude), getGenOpts().power);
          
        }
      }
      o.scale += getGenOpts().scale_ratio; o.amplitude *= getGenOpts().amplitude_ratio;
    }


    float max = noise_store.getMax();
    float min = noise_store.getMin();

    for(int i = 0; i < o.res; i++) {
      for(int j = 0; j < o.res; j++) {
        noise_store.set(i,j, (noise_store.get(i,j) - min) / max + 1f);
        //noise_grid[i,j] = (noise_grid[i,j] - min) / max + 1f;
      }
    }

    /*
    Debug.Log("Max: " + noise_store.getMax());
    Debug.Log("Min: " + noise_store.getMin());
    */
  }


  public override void applyTerrain(ref float[] existing_noise) {

    if(getGenOpts().enabled) {
      if(getGenOpts().multiply) {
        for(int i = 0; i < noise_store.getWidth(); i++) {
          for(int j = 0; j < noise_store.getHeight(); j++) {
            existing_noise[i + noise_store.getHeight() * j] *= noise_store.get(i,j);
          }
        }
      } else {
        for(int i = 0; i < noise_store.getWidth(); i++) {
          for(int j = 0; j < noise_store.getHeight(); j++) {
            existing_noise[i + noise_store.getHeight() * j] += noise_store.get(i,j);
          }
        }
      }
    }

  }

}
