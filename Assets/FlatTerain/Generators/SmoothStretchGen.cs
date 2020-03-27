using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothStretchGen : TerrainGenerator {

  SmoothStrechOpt opt;

  public SmoothStretchGen(float scale_x, float scale_y, float amplitude) {
    opt = new SmoothStrechOpt(scale_x, scale_y);
    this.draw_editor = true;
    this.gen_type = GeneratorType.SmoothStretch;
  }

  public SmoothStretchGen(SmoothStrechOpt opt) {
    this.opt = opt;
  }

  public SmoothStretchGen(TerrainGenerator tg) {
    opt = (SmoothStrechOpt) tg.gen_opts;
    this.draw_editor = true;
    this.gen_type = GeneratorType.SmoothStretch;
  }

  public override void generateTerrain(NoiseOptions no) {
    base.generateTerrain(no);

    if(opt.enabled) {
      noise_store.setAdd2D(NoiseGrid.genNoise(no, opt.scale_x, opt.scale_y));
    }
  }

  public override void applyTerrain(ref float[] existing_noise) {

    if(opt.enabled) {
      for(int i = 0; i < noise_store.getWidth(); i++) {
        for(int j = 0; j < noise_store.getHeight(); j++) {
          existing_noise[i + noise_store.getWidth() * j] += noise_store.get(i,j);
        }
      }
    }

  }


}
