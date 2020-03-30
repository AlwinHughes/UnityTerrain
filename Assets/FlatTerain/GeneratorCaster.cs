using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneratorCaster {

  public static TerrainGenerator castTG(TerrainGenerator tg) {
    Debug.Log("castTG");
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
    } else if (tg.gen_type == GeneratorType.MultiplyConform) {
      return new MultiplyConform(tg);
    } else if (tg.gen_type == GeneratorType.AddConform) {
      return new AddConform(tg);
    } else if(tg.gen_type == GeneratorType.SmoothStretch) {
      return new SmoothStretchGen(tg);
    } else if(tg.gen_type == GeneratorType.RidgeStretch) {
      return new SmoothStretchGen(tg);
    } else {
      return tg;
    }
  }

  public static TerrainGenerator makeTG(ScriptableObject opt, GeneratorType type) {
    if (type == GeneratorType.Bounds) {
      return new BoundsGen(opt as BoundsGenOpt);
    } else if (type == GeneratorType.GeomRidge) {
      return new GeomRidgeGen(opt as GeomRidgeGenOpt);
    } else if (type == GeneratorType.Normalise) {
      return new Normalise();
    } else if (type == GeneratorType.Ridge) {
      return new RidgeGenerator(opt as RidgeGenOpt);
    } else if (type == GeneratorType.SmoothGeometric) {
      return new SmoothGeometric(opt as SmoothGeomOpt);
    } else if (type == GeneratorType.YTransform) {
      return new YTransformGen(opt as YTransformOpt);
    } else if (type == GeneratorType.MultiplyConform) {
      return new MultiplyConform(opt as MultiplyConformOpt);
    } else if (type == GeneratorType.AddConform) {
      return new AddConform(opt as AddConformOpt);
    } else if (type == GeneratorType.SmoothStretch) {
      return new SmoothStretchGen(opt as SmoothStrechOpt);
    } else if (type == GeneratorType.RidgeStretch) {
      return new RidgeStretchGen(opt as RidgeStretchOpt);
    } 


    Debug.Log("something has gone very wrong");
    throw new System.Exception("unknown generator type");
    //return null;
  }
}

/*

    AddConformOpt.cs*
    AddConformOpt.cs.meta*
    BoundsGenOpt.cs*
    BoundsGenOpt.cs.meta*
    Edges.cs*
    Edges.cs.meta*
    GeomRidgeGenOpt.cs*
    GeomRidgeGenOpt.cs.meta*
    MultiplyConformOpt.cs*
    MultiplyConformOpt.cs.meta*
    RidgeGenOpt.cs*
    RidgeGenOpt.cs.meta*
    SmoothGeomOpt.cs*
    SmoothGeomTerrainGenOpt.cs.meta*
    */
