using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YTransformGen : TerrainGenerator {

  public bool take_average;

  public ScriptableObject options;

  public YTransformGen(bool take_average) {
    this.take_average = take_average;
    this.gen_type = GeneratorType.YTransform;
  }

  public YTransformGen(TerrainGenerator tg) {
    this.take_average = false;
    this.gen_type = GeneratorType.YTransform;
    this.noise_store = tg.noise_store;
  }

  public override void generateTerrain(NoiseOptions o) {
    //do nothing to avoid creating a noise store
  }

  public override void applyTerrain(ref float[] existing_noise) {
    float subtract = 0;
    if(take_average) {
      subtract = getMin(existing_noise);
    } else {
      subtract = getSum(existing_noise) / existing_noise.Length;
    }

    for(int i = 0; i < existing_noise.Length; i++) {
        existing_noise[i] -= subtract;
    }
  }

}
