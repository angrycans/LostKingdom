using LitJson;
using System;
using UnityEngine;
using Acans.Tools;
using Acans.Tmx;

class jsontest
{

  public void test()
  {
    // TextAsset ass = Resources.Load("TestResources/t1") as TextAsset;
    // JsonData data = JsonMapper.ToObject(ass.text);

    // Debug.Log("data" + data["height"]);
    // DumpObjecter.Dump(data);
    // var map = new Map();
    //map.LoadJosnFromResources("TestResources/t1");


  }

  public void LoadJosnFromResources(String path)
  {
    var map = new Map();
    TextAsset ass = Resources.Load(path) as TextAsset;
    //JsonData data = JsonMapper.ToObject(ass.text);
    map = JsonMapper.ToObject<Map>(ass.text);

    Objecter.Dump(map);
    // Debug.Log("map " + map.height);
  }

}