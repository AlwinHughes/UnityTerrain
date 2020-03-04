using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyConform : TerrainGenerator {

  private int width;
  private int height;

  public int s_test = 2;


  public MultiplyConform(MultiplyConformOpt lco) {
    this.gen_opts = lco;
    this.draw_editor = true;
    this.gen_type = GeneratorType.MultiplyConform;
  }

  public MultiplyConform(float[] fixed_points, Edge e, Slope s) {
    this.gen_opts = new MultiplyConformOpt(fixed_points, e, s);
    this.gen_type = GeneratorType.MultiplyConform;
    this.draw_editor = true;
    Debug.Log("is mul conf opt: " + this.gen_opts.GetType());
  }

  public MultiplyConform(TerrainGenerator tg) {
    Debug.Log("is mul conf opt: " + tg.gen_opts.GetType());
    this.gen_opts = (MultiplyConformOpt) tg.gen_opts;
    this.noise_store = tg.noise_store;
    this.gen_type = GeneratorType.MultiplyConform;
    this.draw_editor = true;
  }

  private MultiplyConformOpt getGenOpts() {
    return (MultiplyConformOpt) this.gen_opts;
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
            existing_noise[i + width * j] = (1 - slope(((float) j ) / height)) * existing_noise[i + width * j] + slope(((float) j) / height) * getGenOpts().fixed_points[i];
          }
        }
      }

      if(getGenOpts().edge == Edge.Bottom){
        for(int i = 0; i < width; i++) {
          for(int j = 0; j < height; j++) {
            existing_noise[i + width * j] = slope(((float) j) / height) * existing_noise[i + width * j] + (1 - slope(((float) j ) / height)) * getGenOpts().fixed_points[i];
          }
        }
      }

      if(getGenOpts().edge == Edge.Left){
        for(int i = 0; i < width; i++) {
          for(int j = 0; j < height; j++) {
            existing_noise[i + width * j] = (slope(((float) i) /height)) * existing_noise[i + width * j] + (1 - slope(((float) i) / height)) * getGenOpts().fixed_points[j];
          }
        }
      }

      if(getGenOpts().edge == Edge.Right){
        for(int i = 0; i < width; i++) {
          for(int j = 0; j < height; j++) {
            existing_noise[i + width * j] = (1 - slope(((float) i) / height)) * existing_noise[i + width * j] + slope(((float) i) / height)  * getGenOpts().fixed_points[j];
          }
        }
      }
    }
  }

  private float slope(float x) {
    switch(getGenOpts().slope) {
      case Slope.Linear:
        return x;
      case Slope.Ease1:
        return x*x*x*(4 - 3*x);
      case Slope.Ease2:
        return Mathf.Pow(x*x*x*(4 - 3*x),3);
      default:
        return 0;
    }
  }

}
