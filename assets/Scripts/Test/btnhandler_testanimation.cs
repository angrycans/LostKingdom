using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Acans.Animation;
using Acans.Tools;
using Pathfinding;
public class btnhandler_testanimation : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void test(string name)
  {
    Log.info("onclick", name);

    var go = GameObject.Find("ani");
    if (name == "stop")
    {

      go.GetComponent<FrameAnimator>().StopAnimation();
    }
    else
    {
      go.GetComponent<FrameAnimator>().PlayAnimation(name);
    }

  }
}
