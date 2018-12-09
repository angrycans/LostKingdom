using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using LitJson;
using UnityEngine;

namespace Acans.Tools
{

  public class DumpObjecter
  {

    public static void Dump(object element)
    {

      var toJson = LitJson.JsonMapper.ToJson(element);
      Log.info(element.GetType().Name, toJson);
      //Debug.Log(Dump(element, 2));
    }
  }


}