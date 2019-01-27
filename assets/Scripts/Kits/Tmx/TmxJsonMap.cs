using UnityEngine;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Acans.Tmx.Json
{
  [System.Serializable]
  public class TmxMap
  {
    public string backgroundcolor;
    public int width;
    public int height;
    public Boolean infinite;
    public int hexsidelength;
    public Layer[] layers;

    public int nextlayerid;

    public int nextobjectid;
    //orthogonal, isometric, staggered or hexagonal
    public string orientation;
    //Rendering direction (orthogonal maps only)
    public string renderorder;

    public string tiledversion;
    public int tilewidth;
    public int tileheight;

    public Tileset[] tilesets;
    //x or y (staggered / hexagonal maps only)
    public string staggeraxis;
    //odd or even(staggered / hexagonal maps only)
    public string staggerindex;
    public string type;

    public double version;

    public List<Sprite> sprites;


  }
  [Serializable]

  public class Layer
  {
    public int id;

    public string name;

    public int opacity;
    //tilelayer, objectgroup, imagelayer or group
    public string type;

    public Boolean visible;

    public int width;
    public int height;
    public int x;
    public int y;
    public int offsetx;
    public int offsety;
    public int[] data;
    public string image;

    public Propertie[] properties;

    public int getId(int x, int y)
    {

      if (x < 0 || y < 0 || x >= width || y >= height)
        return 0;
      return data[x + y * width];
    }


  }
  [Serializable]
  public class Propertie
  {
    public string name;
    public string value;
    public string type;
  }

  [Serializable]
  public class Tile
  {

    public int id;

    public string image;

    public int imageheight;

    public int imagewidth;
    public Propertie[] properties;
  }

  [Serializable]
  public class Tileset
  {

    public int columns;

    public int firstgid;

    public int margin;

    public string name;

    public int spacing;

    public string type;

    public string image;
    public int imagewidth;
    public int imageheight;

    public Propertie[] properties;

    public Tile[] tiles;
    public int tilecount;
    public int tilewidth;
    public int tileheight;

  }



}