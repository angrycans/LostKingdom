using UnityEngine;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Acans.Tmx
{
  [Serializable]

  public class LayersItem
  {
    /// <summary>
    /// 
    /// </summary>
    public List<int> data { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int height { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int opacity { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Boolean visible { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int width { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int x { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int y { get; set; }
  }
  [Serializable]
  public class TilesItem
  {
    /// <summary>
    /// 
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string image { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int imageheight { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int imagewidth { get; set; }
  }

  [Serializable]
  public class TilesetsItem
  {
    /// <summary>
    /// 
    /// </summary>
    public int columns { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int firstgid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int margin { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int spacing { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int tilecount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int tileheight { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<TilesItem> tiles { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int tilewidth { get; set; }
  }
  [Serializable]
  public class Root
  {
    /// <summary>
    /// 
    /// </summary>
    public int height { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Boolean infinite { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public LayersItem[] layers { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int nextlayerid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int nextobjectid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string orientation { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string renderorder { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string tiledversion { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int tileheight { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<TilesetsItem> tilesets { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int tilewidth { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double version { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int width { get; set; }
  }


}