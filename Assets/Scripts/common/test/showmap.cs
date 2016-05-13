using UnityEngine;
using System.Collections;
using System.IO;

using LitJson;
using System.Text.RegularExpressions;

using AcansUtils;



public class showmap : MonoBehaviour
{
  tk2dSprite sprite;

  // Use this for initialization
  void Start()
  {
		//GetPrefab("");
		//testJson();
		Debug.Log("show test pcg");



		//g.AddComponent(g2);



		ACANS.test map = new ACANS.test();
	//	t.


		for (int i = 0; i < map.grid.GetLength(0); i++)
		{
			for (int j = 0; j < map.grid.GetLength(1); j++)
			{
				//sb.Append( grid[i, j] );

	
				GameObject g = new GameObject();
				tk2dSpriteCollectionData currentCollectionData =
					Resources.Load("tk2dSpriteData/map_blue_sourceData/map_blue_source", typeof(tk2dSpriteCollectionData)) as
					 tk2dSpriteCollectionData;
				tk2dSprite sprite = tk2dSprite.AddComponent(g, currentCollectionData, "floor_01");
				//sprite.transform.position = new Vector3(129/2, 158/2, 1);
				sprite.transform.position = new Vector3(129/2+(120*i), 158/2+(120*j), 1);


				//show postion
				GameObject g2= new GameObject();
				tk2dTextMesh textmesh=g2.AddComponent<tk2dTextMesh>();	
				GameObject fontData = Resources.Load("FontData/font_01data") as GameObject;
				textmesh.font = fontData.GetComponent<tk2dFontData>();
				textmesh.text = i+","+j;
				textmesh.anchor = TextAnchor.MiddleCenter;
				//textmesh.Commit();
				g2.transform.parent = g.transform;
				g2.transform.localPosition=new Vector3(0, 0, -1);
		

			}
		}

			//sb.Append("\n");



  }

  // Update is called once per frame
  void Update()
  {
  }
[System.Serializable]
public class Person
{

    public int id = 0;

    public string name = "name";

    public int age = 0;

    public Movie[] movie;
}

[System.Serializable]
public class Movie
{

    public string title = null;

    public string status = null;

    public string originalstory = null;

    public string originalwriter = null;
}

  void testJson()
  {
    Debug.Log("test json");

    //string json = File.ReadAllText(Path.Combine(Application.dataPath+"/Resources/", "config/config.json"));
    string json = File.ReadAllText(Path.Combine(Application.dataPath+"/Scripts/", "config/config.json"));

    Debug.Log(" json ="+json);

    Person person = null;
    person = JsonUtility.FromJson<Person>(json);

    Debug.Log("person "+person.age);

    JsonData jsondata = JsonMapper.ToObject(json);

    Debug.Log("litjson =" + jsondata["name"]);

    Debug.Log("litjson 2=" + jsondata["movie"][0]["title"]);


  }

  public GameObject GetPrefab(string name)
  {
    GameObject prefab = null;
    //Check in the dictionary if this type of tile already exists. If not, load it from the resources.


    //Debug.Log(prefabType);
    //GameObject t=null;
    //string directory = "/resources/" +name;

    //tk2dSpriteCollectionData currentCollectionData=(tk2dSpriteCollectionData)Resources.Load("tk2dSpriteData/map_blue_source.prefab");
    GameObject t = new GameObject();
    tk2dSpriteCollectionData currentCollectionData =
      Resources.Load("tk2dSpriteData/map_blue_sourceData/map_blue_source", typeof(tk2dSpriteCollectionData)) as
        tk2dSpriteCollectionData;
    sprite = tk2dSprite.AddComponent(t, currentCollectionData, "floor_01");
    sprite.transform.position = new Vector3(129/2, 158/2, 1);
    //sprite.scale = new Vector3(10, 10, 10);
    Debug.Log(currentCollectionData);

    Debug.Log("bounds"+sprite.CurrentSprite.GetBounds().size.x+" "+sprite.CurrentSprite.GetBounds().size.y);
    Debug.Log("untrimmebounds"+sprite.CurrentSprite.GetUntrimmedBounds().size.x+" "+sprite.CurrentSprite.GetUntrimmedBounds().size.y);
    Debug.LogFormat("x={0} y={1} str={2}",0,1,"aaa");
    AcansUtils.Log.info(sprite.CurrentSprite.GetBounds().size.x,sprite.CurrentSprite.GetBounds().size.y,sprite.CurrentSprite);
    /*

	GameObject go = new GameObject();
      		tk2dSprite sprite = go.AddComponent<tk2dSprite>();

		    tk2dSpriteCollectionData spriteCollection = Resources.Load ("td2dSpriteData/images", typeof(tk2dSpriteCollectionData)) as tk2dSpriteCollectionData;

		Debug.Log("spriteCollection="+spriteCollection.ToString());

		sprite = tk2dSprite.AddComponent(gameObject,spriteCollection, "floor_01");
		sprite.transform.position = new Vector3(10,50,0);    		Object prefabdemo = EditorUtility.CreateEmptyPrefab("Assets/Temporary/"+t.gameObject.name+".prefab");
            EditorUtility.ReplacePrefab(t.gameObject, prefabdemo, ReplacePrefabOptions.ConnectToPrefab);


  			//Debug.Log(directory);
  			if (tileDictionary.ContainsKey(directory))
  				prefab = tileDictionary[directory];
  			else
  			{
  				prefab = Resources.Load(directory) as GameObject;
  				if (prefab == null)
  				{
  					//If there are no matches in the directory try for default prefab.
  					directory = theme + "/Cells/" + type + "/PRE_" + type + "_" + "Default";
  					if (tileDictionary.ContainsKey(directory))
  						prefab = tileDictionary[directory];
  					else
  					{
  						prefab = Resources.Load(directory) as GameObject;
  						//Add the tile to the tileDictionary.
  						tileDictionary.Add(directory, prefab);
  					}
  				}
  				else
  					//Add the tile to the tileDictionary.
  					tileDictionary.Add(directory, prefab);
  			}
*/
    return prefab;
  }
}
