using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

using Newtonsoft.Json;

namespace Acans.Tools
{

  public static class Objecter
  {

    public static void Dump(object element)
    {

      // // if (element.GetType()){

      // }

      var toJson = JsonConvert.SerializeObject(element);

      //var toJson = Json.Serialize(element);
      Log.info("dump=>", element.GetType().Name, toJson);
      //Debug.Log(Dump(element, 2));
    }
  }


}