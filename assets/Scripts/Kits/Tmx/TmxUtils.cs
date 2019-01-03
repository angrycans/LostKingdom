using UnityEngine;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Acans.Tmx
{


  public class Utils
  {
    static public String getTmxImageSourceAssetPath(String tmxfile, String tmxsource)
    {
      var path = tmxfile.Substring(0, tmxfile.LastIndexOf("/") + 1);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_IPHONE
      return "/" + path + tmxsource;
#else
    return path + tmxsource;
#endif

    }
  }
}