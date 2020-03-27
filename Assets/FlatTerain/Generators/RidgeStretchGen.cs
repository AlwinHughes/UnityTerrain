using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgeStretchGen : TerrainGenerator {

  RidgeStretchOpt opt;

  public RidgeStretchGen(RidgeStretchOpt opt) {
    this.opt = opt;
    this.draw_editor = true;
    this.gen_type = GeneratorType.RidgeStretch;
  }

  public RidgeStretchGen(TerrainGenerator tg) {
    this.opt = (RidgeStretchOpt) tg.gen_opts;
    this.draw_editor = true;
    this.gen_type = GeneratorType.Ridge;
    this.noise_store = tg.noise_store;
  }

  public override void generateTerrain(NoiseOptions no) {
    base.generateTerrain(no);

    if(opt.enabled) {

      noise_store.copyFrom2D(NoiseGrid.genNoise(no, opt.scale_x, opt.scale_y));
      //noise_grid = NoiseGrid.genNoise(o);

      //make ridges
      for(int i = 0; i < no.res; i++) {
        for(int j = 0; j < no.res; j++) {
          noise_store.set(i,j, Mathf.Pow(0.5f - Mathf.Abs(0.5f - noise_store.get(i,j)), opt.power));
          //noise_grid[i,j] = Mathf.Pow(0.5f - Mathf.Abs(0.5f - noise_grid[i,j]), getGenOpts().power);
        }
      }
    }
  }



  public override void applyTerrain(ref float[] existing_noise) {

    if(opt.enabled) {
      if(opt.multiply) {
        for(int i = 0; i < noise_store.getWidth(); i++) {
          for(int j = 0; j < noise_store.getHeight(); j++) {
            //existing_noise[i + noise_grid.GetLength(0) * j] += noise_grid[i,j];
            //existing_noise[i + noise_grid.GetLength(0) * j] = (noise_grid[i,j] +1)* existing_noise[i + noise_grid.GetLength(0) * j];
        //    existing_noise[i + noise_store.getWidth() * j] = (noise_store.get(i,j) +1) * existing_noise[i + noise_store.getWidth() * j];
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
