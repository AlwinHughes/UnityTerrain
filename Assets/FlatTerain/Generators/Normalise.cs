using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normalise : TerrainGenerator {

  public Normalise() {
    this.gen_type = GeneratorType.Normalise;
  }

  public Normalise(TerrainGenerator tg) {
    this.gen_type = GeneratorType.Normalise;
    this.noise_store = tg.noise_store;
  }

  public override void applyTerrain(ref float[] existing_terain) {
    Debug.Log("normalise apply terrain");

    float min = existing_terain[0];
    float max = existing_terain[0];
    //get min and max
    for(int i = 1; i < existing_terain.Length; i++) {
      if(existing_terain[i] < min) {
        min = existing_terain[i];
      }
      if(existing_terain[i] > min) {
        max= existing_terain[i];
      }
    }

    float inv_range = 1f / (max - min);

    for(int i = 0; i < existing_terain.Length; i++) {
existing_terain[i] = (existing_terain[i] - min) * inv_range;
    }
  }

}
