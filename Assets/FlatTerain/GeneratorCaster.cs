using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneratorCaster {

  public static TerrainGenerator castTG(TerrainGenerator tg) {
    if (tg.gen_type == GeneratorType.Bounds) {
      return new BoundsGen(tg);
    } else if (tg.gen_type == GeneratorType.GeomRidge) {
      return new GeomRidgeGen(tg);
    } else if (tg.gen_type == GeneratorType.Normalise) {
      return new Normalise(tg);
    } else if (tg.gen_type == GeneratorType.Ridge) {
      return new RidgeGenerator(tg);
    } else if (tg.gen_type == GeneratorType.SmoothGeometric) {
      return new SmoothGeometric(tg);
    } else if (tg.gen_type == GeneratorType.YTransform) {
      return new YTransformGen(tg);
    } else {
      return tg;
    }
  }
}
