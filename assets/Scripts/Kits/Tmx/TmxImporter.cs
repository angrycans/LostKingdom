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
using UnityEngine.Tilemaps;
using Pathfinding;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Acans.Tools;
using Acans.Tmx;
using Acans.Tmx.Json;
public class TmxImporter : MonoBehaviour
{

  public string filename;

  public TmxMap map;
  private float width;
  private float height;

  async void Start()
  {

    float leftBorder;
    float rightBorder;
    float topBorder;
    float downBorder;
    //the up right corner
    Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Mathf.Abs(Camera.main.transform.position.z)));

    leftBorder = Camera.main.transform.position.x - (cornerPos.x - Camera.main.transform.position.x);
    rightBorder = cornerPos.x;
    topBorder = cornerPos.y;
    downBorder = Camera.main.transform.position.y - (cornerPos.y - Camera.main.transform.position.y);

    width = rightBorder - leftBorder;
    height = topBorder - downBorder;

    Log.info("width", width, "height", height);
    await loadMap();

  }

  void Awake()
  {
    //TextAsset t = Resources.Load<TextAsset>("TestResources/t4");

    //Log.info(t.text);
  }


  async Task loadMap()
  {
    // TextAsset t = Resources.Load<TextAsset>("TestResources/t2.tmx");
    // Log.info("t=>", t.text);
    var ret = await FileUtils.LoadFromSteamAssets(filename);
    Log.info("ret=>", ret.text);


    map = JsonConvert.DeserializeObject<TmxMap>(ret.text);


    Objecter.Dump(map);
    await LoadTileset(map);
    CreateTile();

  }



  async Task LoadTileset(TmxMap _map)
  {
    Log.info("LoadTileset start");
    if (_map.sprites == null)
    {
      _map.sprites = new List<Sprite>();
    }
    foreach (var tileset in _map.tilesets)
    {
      var tiles = tileset.tiles;
      if (tileset.image != null)
      {
        Log.info("image.source", tileset.image, Utils.getTmxImageSourceAssetPath(filename, tileset.image));
        var ret = await FileUtils.LoadFromSteamAssets(Utils.getTmxImageSourceAssetPath(filename, tileset.image));
        var tex = ret.texture;
        tex.filterMode = FilterMode.Point;
        for (int y = 0; y < tileset.imageheight / tileset.tileheight; y++)
        {
          for (int x = 0; x < tileset.imagewidth / tileset.tilewidth; x++)
          {

            //Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(Utils.getAssetPath(filename, image.source));

            var sprite = Sprite.Create(tex,
                  new Rect(
                    x * tileset.tilewidth,
                   tileset.imageheight - (y + 1) * tileset.tileheight,
                   tileset.tilewidth,
                   tileset.tileheight),
                  new Vector2(0f, 0f), 1);
            Log.info("Sprite.Create", x, y, x * tileset.tilewidth,
                   tileset.imageheight - (y + 1) * tileset.tileheight,
                   tileset.tilewidth,
                   tileset.tileheight);
            _map.sprites.Add(sprite);

          }
        }
      }
      else if (tiles != null && tiles.Length > 0)
      {
        foreach (var tile in tiles)
        {
          if (tile.image != null)
          {
            //Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(Utils.getTxmImageSourceAssetPath(filename, tile.image.source));
            //DumpObjecter.Dump(tile.image);
            Log.info("image.source", tile.image, Utils.getTmxImageSourceAssetPath(filename, tile.image));
            var ret = await FileUtils.LoadFromSteamAssets(Utils.getTmxImageSourceAssetPath(filename, tile.image));
            var tex = ret.texture;
            tex.filterMode = FilterMode.Point;
            _map.sprites.Add(Sprite.Create(tex,
                  new Rect(
                    0,
                   0,
                   tileset.tilewidth,
                   tileset.tileheight),
                  new Vector2(0f, 0f), 1));
          }
          else
          {
            Log.info("load tileset error: tileset's tile not exist image");
          }
        }

      }
      else
      {
        Log.info("load tileset error: tileset not exist image or tile");
      }

    }
  }

  void CreateTile()
  {
    GameObject grid = GameObject.Find("TmxGrid");

    if (grid)
    {
      Log.error("createtile error,grid is exists");
      return;
    }
    grid = new GameObject("TmxGrid");
    //grid.transform.position = new Vector3(-Config.ReferenceResolution.x / 2, -Config.ReferenceResolution.y / 2, 0);

    var grid_com = grid.AddComponent<Grid>();
    grid_com.cellSize = new Vector3(map.tilewidth, map.tileheight, 0);
    //
    //tile的中心点为四个顶点的其中一个点，默认左下角，我们偏移一下保证和其他游戏对象的中心点一致
    //grid.transform.position = new Vector3(-0.5f, -0.5f, 0);
    //grid.transform.position = new Vector3(0, 1, 0);
    foreach (var layer in map.layers)
    {
      // GameObject tilemap_obj = GameObject.Find("TmxTilemap");
      // if (!tilemap_obj)
      // {
      var tilemap_obj = new GameObject("TmxTilemap_" + layer.name);
      tilemap_obj.transform.parent = grid.transform;
      //tilemap_obj.transform.position = new Vector3(-367, -667, 0);
      //tilemap_obj.transform.position = new Vector3(-Config.ReferenceResolution.x / 2, -Config.ReferenceResolution.y / 2, 0);
      tilemap_obj.transform.position = new Vector3(-width / 2, -height / 2, 0);


      // Texture2D tex = Resources.Load<Texture2D>("testResources/assets/rpgTile024");
      // //Sprite sprite = Resources.Load<Sprite>("testResources/assets/rpgTile175");
      // Sprite sprite = Sprite.Create(tex,
      //               new Rect(0, 0, tex.width, tex.height),
      //               new Vector2(0.5f, 0.5f), 1);
      // //Tile tile = new Tile();
      // var tile = ScriptableObject.CreateInstance<UnityEngine.Tilemaps.Tile>();
      // tile.sprite = sprite;






      var tilemap = tilemap_obj.AddComponent<Tilemap>();
      TilemapRenderer render = tilemap_obj.AddComponent<TilemapRenderer>();

      TilemapCollider2D tilemapCollider2D = tilemap_obj.AddComponent<TilemapCollider2D>();
      // tilemapCollider2D.usedByComposite = true;
      CompositeCollider2D compositeCollider2D = tilemap_obj.AddComponent<CompositeCollider2D>();
      Rigidbody2D rigidbody2D = tilemap_obj.GetComponent<Rigidbody2D>();
      rigidbody2D.bodyType = RigidbodyType2D.Static;
      for (int i = 0; i < layer.width; i++)
      {
        for (int j = 0; j < layer.height; j++)
        {

          if (layer.getId(i, j) != 0)
          {
            // Log.info(map.layers[0].getId(i, map.layers[0].height - j), map.sprites.Count);
            var tile = ScriptableObject.CreateInstance<UnityEngine.Tilemaps.Tile>();
            tile.sprite = map.sprites[layer.getId(i, j) - 1];
            tilemap.SetTile(new Vector3Int(i, layer.height - j, 0), tile);

          }

        }

      }



      //var compositeCollider2D=tilemap_obj.GetComponent<CompositeCollider2D>();


      // BoundsInt bounds = tilemap.cellBounds;
      // TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
      //DumpObjecter.Dump(grid);
      // foreach (var pos in tilemap.cellBounds.allPositionsWithin)
      // {
      //   Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
      //   Vector3 place = tilemap.CellToWorld(localPlace);
      //   if (tilemap.HasTile(localPlace))
      //   {
      //     //tileWorldLocations.Add(place);
      //     //DrawRectangle.draw(grid, new Vector2(place.x, place.y), new Vector2(place.x + grid_com.cellSize.x, place.y + grid_com.cellSize.y));

      //     //Log.info("tile=>", place);
      //   }
      //   else
      //   {
      //     //Log.info("tile=>", place);
      //   }
      // }

      // Log.info(bounds.size.x, bounds.size.y);

    }


    //   }

  }
  // public void OnGUI()
  // {
  //   GuiDrawing.DrawRectangle(new Vector2(0, 0), new Vector2(100, 0), new Vector2(100, 100), new Vector2(0, 100), Color.red, 2f);


  // }
}