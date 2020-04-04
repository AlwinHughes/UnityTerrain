using System;
using UnityEngine;

[Serializable]
public class ColourGenerator {


  ColourSettings col_set;

  Texture2D texture;
  const int texture_res = 50;

  public ColourGenerator(ColourSettings cs) {
    this.col_set = cs;
    texture = new Texture2D(texture_res,1);
  }

  public void updateHeight(MinMax mm) {
    Debug.Log("update minmax");
    col_set.material.SetVector("_heightMinMax", new Vector4(mm.min, mm.max));
  }

  public void updateColours(){
    Debug.Log("update colours");
    Color[] colours = new Color[texture_res];

    for(int i = 0; i < texture_res; i++) {
      colours[i] = col_set.col_grad.Evaluate(i / (texture_res - 1f));
    }

    texture.SetPixels(colours);
    texture.Apply();
    col_set.material.SetTexture("_texture", texture);
  }



}
