using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsGen : TerrainGenerator {

  public BoundsGen(float floor, float ceiling) {
    this.gen_opts = new BoundsGenOpt(floor, ceiling);
    this.draw_editor = true;
  }

  public BoundsGen(BoundsGenOpt op) {
    this.gen_opts = op;
    this.draw_editor = true;
  }

  private BoundsGenOpt getGenOpts() {
    return (BoundsGenOpt) gen_opts;
  }


  public override void applyTerrain(ref float[] existing_noise) {

    if(getGenOpts().enabled) {
      for(int i = 0; i < existing_noise.Length; i++) {
        if(existing_noise[i] < getGenOpts().floor) {
          existing_noise[i] = getGenOpts().floor;
        } else if (existing_noise[i] > getGenOpts().ceiling) {
          existing_noise[i] = getGenOpts().ceiling;
        }
      }
    }

  }

}
