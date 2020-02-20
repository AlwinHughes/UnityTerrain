using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITerrainGenOpt
{
  ScriptableObject getGenOpts();

  bool getFoldOutRef();
}
