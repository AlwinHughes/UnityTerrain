using System.Collections;
using System;
using UnityEngine;

public class ChunkGroup : MonoBehaviour {

  public ChunkGroupOpt opts;

  public Chunk[] chunks;

  public void OnValidate() {
    Debug.Log("Group OnValidate");

    if(opts == null) {
      Debug.Log("making new options");
      opts = new ChunkGroupOpt(2,2, 40);
    }

    if(chunks == null) {
      Debug.Log("making chunk arr");
      chunks = new Chunk[opts.height * opts.width];

      makeAllChunks();
      setChunkPos();
    }
  }


  public void makeAllChunks() {
    Debug.Log("making all chunks");
    for(int i = 0; i < opts.height; i++) {
      for(int j = 0; j < opts.width; j++) {
        chunks[i + j * opts.height] = gameObject.AddComponent(typeof(Chunk)) as Chunk;
      }
    }
  }

  public void setChunkPos() {
    Debug.Log("set chunk pos");
    for(int i = 0; i < opts.height; i++) {
      for(int j = 0; j < opts.width; j++) {
        chunks[i + j * opts.height].setPos(i,j);
      }
    }
  }

  public void createChunk(int i, int j) {
        chunks[i + j * opts.height] = new Chunk(i, j, opts.res, transform);
  }

  public void deleteChunk(int i, int j) {
    Destroy(chunks[i + j * opts.height]);
  }

}
