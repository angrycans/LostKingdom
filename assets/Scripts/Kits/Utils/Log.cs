using System;
using System.Text;
using UnityEngine;

namespace Acans.Tools
{
  static public class Log
  {

    static public bool EnableLog = true;



    static public void error(params object[] args)
    {
      if (EnableLog)
      {
        var sb = new StringBuilder();
        foreach (var arg in args)
          sb.Append(arg).Append(" ");
        Debug.LogError(sb.ToString());
      }
    }
    static public void warning(params object[] args)
    {
      if (EnableLog)
      {
        var sb = new StringBuilder();
        foreach (var arg in args)
          sb.Append(arg).Append(" ");
        Debug.LogWarning(sb.ToString());
      }
    }


    static public void info(params object[] args)
    {
      if (EnableLog)
      {
        var sb = new StringBuilder();
        foreach (var arg in args)
          sb.Append(arg).Append(" ");
        Debug.Log(sb.ToString());
      }
    }
  }
}

