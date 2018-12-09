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

  void Start()
  {
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


    CreateTileMap();

    fs.Close();
    DumpObjecter.Dump(map);

  }

  void CreateTileMap()
  {
    GameObject grid = GameObject.Find("TmxGrid");
    if (!grid)
    {
      grid = new GameObject("TmxGrid");
      var grid_com = grid.AddComponent<Grid>();
      grid_com.cellSize = new Vector3(1f, 1f, 0);
    }
    //tile的中心点为四个顶点的其中一个点，默认左下角，我们偏移一下保证和其他游戏对象的中心点一致
    //grid.transform.position = new Vector3(-0.5f, -0.5f, 0);
    grid.transform.position = new Vector3(0, 0, 0);

    GameObject tilemap = GameObject.Find("TmxTilemap");
    if (!tilemap)
    {
      tilemap = new GameObject("TmxTilemap");
      tilemap.transform.parent = grid.transform;
      var tilemap_com = tilemap.AddComponent<Tilemap>();
      TilemapRenderer render = tilemap.AddComponent<TilemapRenderer>();

      Sprite sprite = Resources.Load<Sprite>("testResources/assets/rpgTile175");
      //Tile tile = new Tile();
      var tile = ScriptableObject.CreateInstance<Tile>();
      tile.sprite = sprite;
      tilemap_com.SetTile(new Vector3Int(1, 1, 0), tile);
    }
  }




}