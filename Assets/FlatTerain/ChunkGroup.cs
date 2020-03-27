using System.Collections;
using System;
using UnityEngine;

public class ChunkGroup : MonoBehaviour {

  public ChunkGroupOpt opts;

  public NoiseOptions noise_options;


  public TerrainGenerator[] generators;

  public Chunk[] chunks;

  public void OnValidate() {
    Debug.Log("Group OnValidate");

    if(opts == null) {
      Debug.Log("making new options");
      opts = new ChunkGroupOpt(2,2, 1f, 1f);
    }

    if(noise_options == null) {
      noise_options = new NoiseOptions(0.4f, 40, 1);
    }

    if(generators == null) {
      generators = new TerrainGenerator[] {new SmoothGeometric(0.2f, 5f, 6), new GeomRidgeGen(2f, 2f, 1f, 0.5f, 3, true)};
    }

    if(chunks == null) {
      Debug.Log("making chunk arr");
      chunks = new Chunk[opts.length* opts.width];

      makeAllChunks();
    }
  }


  public void makeAllChunks() {
    Debug.Log("making all chunks");
    for(int i = 0; i < opts.length; i++) {
      for(int j = 0; j < opts.width; j++) {
        chunks[i + j * opts.length] = gameObject.AddComponent(typeof(Chunk)) as Chunk;

        chunks[i + j * opts.length].init(transform.position + new Vector3(i * opts.side_length, 0, j * opts.side_width), noise_options, generators);
      }
    }
  }


  public void createChunk(int i, int j) {
        chunks[i + j * opts.length] = new Chunk(i, j, noise_options.res, transform);
  }

  public void deleteChunk(int i, int j) {
    Destroy(chunks[i + j * opts.length]);
  }

}
