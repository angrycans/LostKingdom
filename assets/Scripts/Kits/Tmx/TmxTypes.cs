/*
 * @Author: Acans angrycans@gmail.com 
 * @Date: 2018-12-08 22:49:22 
 * @Last Modified by:   Acans 
 * @Last Modified time: 2018-12-08 22:49:22 
 https://github.com/diogorbg/TMX-MapLoader-Unity5/blob/master/TMX-MapLoader/Assets/Scripts/TMX_Data.cs
 */

using UnityEngine;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Acans.Tmx
{

  [Serializable, XmlRoot("map")]
  public class Map
  {
    [XmlAttribute] public string version;
    [XmlAttribute] public string orientation;
    [XmlAttribute] public string renderorder;
    [XmlAttribute] public int width;
    [XmlAttribute] public int height;
    [XmlAttribute] public int tilewidth;
    [XmlAttribute] public int tileheight;
    [XmlAttribute] public int nextobjectid;

    [XmlElement("tileset")]
    public Tileset[] tilesets;

    [XmlElement("layer")]
    public Layer[] layers;

    [XmlElement("objectgroup")]
    public ObjectGroup[] objectGroups;

    public List<Sprite> sprites;
  }

  [Serializable]
  public class Tileset
  {
    [XmlAttribute] public string name;
    [XmlAttribute] public int firstgid;
    [XmlAttribute] public int tilewidth;
    [XmlAttribute] public int tileheight;
    [XmlAttribute] public int tilecount;
    [XmlAttribute] public int columns;

    [XmlElement("tile")]
    public Tile[] tiles;

    //- Tileset para arquivos .tsx
    [XmlAttribute] public string source;



    [XmlElement("image")]
    public Image image;

    //public object obj = null;

    public string getSource()
    {
      if (image != null && image.source != null)
        return image.source;
      else if (source != null)
        return source;
      return "";
    }
  }

  [Serializable]
  public class Image
  {
    [XmlAttribute] public string source;
    [XmlAttribute] public int width;
    [XmlAttribute] public int height;
  }

  [Serializable]
  public class Tile
  {
    [XmlAttribute] public string id;
    [XmlElement("image")]
    public Image image;
  }

  [Serializable]
  public class Layer
  {
    [XmlAttribute] public string name;
    [XmlAttribute] public int width;
    [XmlAttribute] public int height;
    [XmlAttribute] public int offsetx;
    [XmlAttribute] public int offsety;

    [XmlElement("data")]
    public Data data;

    public int getId(int x, int y)
    {
      if (x < 0 || y < 0 || x >= width || y >= height)
        return 0;
      return data.ids[x + y * width];
    }
  }

  [Serializable]
  public class Data
  {
    [XmlAttribute] public string encoding;
    [XmlText, HideInInspector] public string text;
    public int[] ids;
  }

  [Serializable]
  public class ObjectGroup
  {
    [XmlAttribute] public string name;

    [XmlElement("object")]
    public TmxObject[] objects;
  }

  [Serializable]
  public class TmxObject
  {
    [XmlAttribute] public string name;
    [XmlAttribute] public string type;
    [XmlAttribute] public int id;
    [XmlAttribute] public int gid;
    [XmlAttribute] public int x;
    [XmlAttribute] public int y;
    [XmlAttribute] public int width;
    [XmlAttribute] public int height;
  }

}