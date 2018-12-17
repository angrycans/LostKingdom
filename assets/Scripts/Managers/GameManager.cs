using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Acans.Tools;

namespace Acans.Engine
{
  [AddComponentMenu("AcansEngine/Managers/Game Manager")]
  public class GameManager : PersistentSingleton<GameManager>
  {

    // Use this for initialization
    void Start()
    {
      Debug.Log("GameManager start");

      //var jsontest = new jsontest();
      //jsontest.test();
    }

    // Update is called once per frame
    void Update()
    {

    }
  }

}