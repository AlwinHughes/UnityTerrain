using UnityEngine;
using UnityEditor;
using System;

public class YTransformGen : TerrainGenerator {


  public ScriptableObject options;

  YTransformOpt opt;

  public YTransformGen(YTransformOpt opt) {
    this.opt = opt;
    this.gen_type = GeneratorType.YTransform;
  }

  public YTransformGen(TerrainGenerator tg) {
    this.gen_type = GeneratorType.YTransform;
    this.noise_store = tg.noise_store;
  }

  public override void generateTerrain(NoiseOptions o) {
    //do nothing to avoid creating a noise store
  }

  public override void applyTerrain(ref float[] existing_noise) {
    float subtract = 0;

    if(!opt.enabled) {
      return;
    }

    if(opt.take_average) {
      subtract = getMin(existing_noise);
    } else {
      subtract = getSum(existing_noise) / existing_noise.Length;
    }

    for(int i = 0; i < existing_noise.Length; i++) {
        existing_noise[i] -= subtract;
    }
  }

}
