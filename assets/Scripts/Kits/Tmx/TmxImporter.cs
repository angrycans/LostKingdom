using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Acans.Tools;
using Acans.Tmx;
public class TmxImporter : MonoBehaviour
{
#if UNITY_EDITOR
  public UnityEngine.Object file;
#endif

  public string filename;

  public Acans.Tmx.Map map;
  private float width;
  private float height;

  void Start()
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
    loadMap();

  }

  void loadMap()
  {
#if UNITY_EDITOR
    filename = AssetDatabase.GetAssetPath(file);
#endif


    if (file == null || filename == null)
    {

    }
    // Cria uma intancia do XmlSerializer especificando o tipo e namespace.
    XmlSerializer serializer = new XmlSerializer(typeof(Map));

    // Um FileStream é necessário para ler um documento XML.
    FileStream fs = new FileStream(filename, FileMode.Open);
    XmlReader reader = XmlReader.Create(fs);

    // Declaração do objeto raiz que irá receber a deserialização.
    map = (Map)serializer.Deserialize(reader);

    fs.Close();
    DumpObjecter.Dump(map);

    if (map != null)
    {
      foreach (Layer layer in map.layers)
      {
        string[] split = layer.data.text.Split(',');
        layer.data.ids = new int[split.Length];
        for (int i = 0; i < split.Length; i++)
        {
          layer.data.ids[i] = int.Parse(split[i]);
        }
        layer.data.text = null;
      }
    }

    // Log.info("map=", Dumper.Dump(map));

    CreateTileMap();
    CreateTile();
  }

  void CreateTileMap()
  {

  }

  void renderMap()
  {
    if (map.layers.Length > 0)
    {
      for (int i = 0; i < map.layers[0].width; i++)
      {
        for (int j = 0; j < map.layers[0].height; j++)
        {
        }
      }
    }
  }

  // void createGrid(){
  //   GameObject grid = GameObject.Find("TmxGrid");

  //   if (!grid)
  //   {
  //     Log.error("createtile error,grid is exists");
  //     return;
  //   }
  // }

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
    grid_com.cellSize = new Vector3(64f, 64f, 0);
    //
    //tile的中心点为四个顶点的其中一个点，默认左下角，我们偏移一下保证和其他游戏对象的中心点一致
    //grid.transform.position = new Vector3(-0.5f, -0.5f, 0);
    //grid.transform.position = new Vector3(0, 1, 0);

    GameObject tilemap_obj = GameObject.Find("TmxTilemap");
    if (!tilemap_obj)
    {
      tilemap_obj = new GameObject("TmxTilemap");
      tilemap_obj.transform.parent = grid.transform;
      //tilemap_obj.transform.position = new Vector3(-367, -667, 0);
      //tilemap_obj.transform.position = new Vector3(-Config.ReferenceResolution.x / 2, -Config.ReferenceResolution.y / 2, 0);
      tilemap_obj.transform.position = new Vector3(-width / 2, -height / 2, 0);
      var tilemap = tilemap_obj.AddComponent<Tilemap>();
      TilemapRenderer render = tilemap_obj.AddComponent<TilemapRenderer>();

      Texture2D tex = Resources.Load<Texture2D>("testResources/assets/rpgTile001");
      //Sprite sprite = Resources.Load<Sprite>("testResources/assets/rpgTile175");
      Sprite sprite = Sprite.Create(tex,
                    new Rect(0, 0, tex.width, tex.height),
                    new Vector2(0.5f, 0.5f), 1);
      //Tile tile = new Tile();
      var tile = ScriptableObject.CreateInstance<Tile>();
      tile.sprite = sprite;


      if (map.layers.Length > 0)
      {
        for (int i = 0; i < map.layers[0].width; i++)
        {
          for (int j = 0; j < map.layers[0].height; j++)
          {
            //Log.info("data=", i, j, ObjectDumper.Dump(map.layers[0].data.ids));
            //Log.info("gid=", i, j, map.layers[0].getId(i, j));
            if (map.layers[0].getId(i, j) != 0)
            {
              tilemap.SetTile(new Vector3Int(i, j, 0), tile);
            }

          }
        }
      }

      // BoundsInt bounds = tilemap.cellBounds;
      // TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
      //DumpObjecter.Dump(grid);
      foreach (var pos in tilemap.cellBounds.allPositionsWithin)
      {
        Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
        Vector3 place = tilemap.CellToWorld(localPlace);
        if (tilemap.HasTile(localPlace))
        {
          //tileWorldLocations.Add(place);
          //DrawRectangle.draw(grid, new Vector2(place.x, place.y), new Vector2(place.x + grid_com.cellSize.x, place.y + grid_com.cellSize.y));

          //Log.info("tile=>", place);
        }
        else
        {
          //Log.info("tile=>", place);
        }
      }

      // Log.info(bounds.size.x, bounds.size.y);

    }




  }
  // public void OnGUI()
  // {
  //   GuiDrawing.DrawRectangle(new Vector2(0, 0), new Vector2(100, 0), new Vector2(100, 100), new Vector2(0, 100), Color.red, 2f);


  // }
}