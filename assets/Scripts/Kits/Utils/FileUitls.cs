using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UniRx;
using Newtonsoft.Json;

using Pathfinding;

namespace Acans.Tools
{
  static public class FileUtils
  {


    static public string GetFileName(string path)
    {
      var filename = "";
      Log.info(path.LastIndexOf("/"), path.LastIndexOf("."));
      if ((path.LastIndexOf("/") > 0) && (path.LastIndexOf(".") > 0))
      {
        filename = path.Substring(path.LastIndexOf("/") + 1, path.Length - path.LastIndexOf("/") - 1);
      }
      else
      {
        throw new Exception("Path error " + path);
      }

      return filename;
    }

    async static public Task<WWW> LoadFromSteamAssets(string url)
    {
      string path = "";
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_IPHONE
      path = "file://" + Application.streamingAssetsPath;
#else
    path =Application.streamingAssetsPath;
#endif
      Log.info("LoadFromSteamAssets", path + url);
      var www = await new WWW(path + url);

      return www;
    }

    async static public Task<WWW> LoadABFromSteamAssets(string url)
    {
      string path = "";
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_IPHONE
      path = "file://" + Application.streamingAssetsPath;
#else
          path =Application.streamingAssetsPath;
#endif
      Log.info("LoadFromSteamAssets", path + url);
      var www = await new WWW(path + url);

      return www;
    }


  }
}

