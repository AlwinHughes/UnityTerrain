using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SmoothGeometric : TerrainGenerator {

  public SmoothGeometric(float amplitude_ratio, float scale_ratio, int num_octaves) {
    this.gen_opts = new SmoothGeomTerrainGenOpt(amplitude_ratio, scale_ratio, num_octaves);
    this.draw_editor = true;
    this.gen_type = GeneratorType.SmoothGeometric;
  }


  private SmoothGeomTerrainGenOpt getGenOpts() {
    return (SmoothGeomTerrainGenOpt) this.gen_opts;
  }

  public SmoothGeometric(SmoothGeomTerrainGenOpt go){
    this.gen_opts = go;
    this.gen_type = GeneratorType.SmoothGeometric;
  }

  public SmoothGeometric(TerrainGenerator tg) {
    this.draw_editor = true;
    this.gen_type = GeneratorType.SmoothGeometric;
    this.gen_opts = (SmoothGeomTerrainGenOpt) tg.gen_opts;
    this.noise_store = tg.noise_store;
  }

  public override void generateTerrain(NoiseOptions options) {

    Debug.Log("override gen ter");

    base.generateTerrain(options);

    Debug.Log("making noise");
    if(getGenOpts().enabled) {

      NoiseOptions o = new NoiseOptions(options);

      for(int i = 0; i < getGenOpts().num_octaves; i++) {
        noise_store.setAdd2D(NoiseGrid.genNoise(o));
        //noise_grid = add2DArr(noise_grid, NoiseGrid.genNoise(o));
        o.scale *= getGenOpts().scale_ratio;
        o.amplitude *= getGenOpts().amplitude_ratio;
      }
    }
  }

  public override void applyTerrain(ref float[] existing_noise) {

    Debug.Log("override apply terrain");

    if(getGenOpts().enabled) {
      for(int i = 0; i < noise_store.getWidth(); i++) {
        for(int j = 0; j < noise_store.getHeight(); j++) {
          existing_noise[i + noise_store.getWidth() * j] += noise_store.get(i,j);


        }
      }
    }
  }

}
