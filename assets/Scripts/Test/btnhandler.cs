using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Acans.Tools;
using Pathfinding;
public class btnhandler : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void test()
  {
    //AstarPath.active.Scan();
    var go = GameObject.Find("d");
    //go.SetActive(!go.activeSelf);
    go.GetComponent<Collider2D>().enabled = !go.GetComponent<Collider2D>().enabled;
    // var bounds = go.GetComponent<Collider2D>().bounds;
    // var guo = new GraphUpdateObject(bounds);
    // guo.modifyWalkability = true;
    // guo.setWalkability = true;

    //AstarPath.active.UpdateGraphs(guo);
    AstarPath.active.Scan();
    Log.info("hello test");
  }
}
