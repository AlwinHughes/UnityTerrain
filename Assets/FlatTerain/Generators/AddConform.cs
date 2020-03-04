using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddConform : TerrainGenerator {

  public int s_test = 2;

  private int width;
  private int height;

  public AddConform(AddConformOpt lco) {
    this.gen_opts = lco;
    this.draw_editor = true;
    this.gen_type = GeneratorType.AddConform;
  }

  public AddConform(float[] fixed_points, Edge e) {
    this.gen_opts = new AddConformOpt(fixed_points, e);
    this.gen_type = GeneratorType.AddConform;
    this.draw_editor = true;
  }

  public AddConform (TerrainGenerator tg) {
    Debug.Log("is add conf opt type: " + tg.gen_opts.GetType());
    this.gen_opts = (AddConformOpt) tg.gen_opts;
    this.noise_store = tg.noise_store;
    this.gen_type = GeneratorType.AddConform;
    this.draw_editor = true;
  }

  private AddConformOpt getGenOpts() {
    return (AddConformOpt) this.gen_opts;
  }

  override public void generateTerrain(NoiseOptions options) {
    //do nothing to avoid creating a noise_store
    this.height = options.res;
    this.width = options.res;
  }

  public override void applyTerrain(ref float[] existing_noise) {

    if(getGenOpts().enabled) {

      if(getGenOpts().edge == Edge.Top){
        for(int i = 0; i < width; i++) {
          for(int j = 0; j < height; j++) {
            //existing_noise[i + width * j] = existing_noise[i + width * j] - (existing_noise[i + width * j] - getGenOpts().fixed_points[i]) * ( (float) j / height);
            //
            
            existing_noise[i + width * j] = existing_noise[i + width * j] - (existing_noise[i + width * j] - getGenOpts().fixed_points[i]) * ( (float) j / height);
          }
        }
      }
    }
  }

}
