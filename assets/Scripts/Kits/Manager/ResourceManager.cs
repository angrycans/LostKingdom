using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Acans.Tools;
namespace Acans.Manager
{

  public class ResourceManager : Singleton<ResourceManager>
  {

    //private Dictionary<string, List<Sprite>> _spriteList = new Dictionary<string, List<Sprite>>();
    private Dictionary<string, Sprite> _spriteMap = new Dictionary<string, Sprite>();

    void Start()
    {
      Log.info("ResourceManager start");
    }

    public void addSprite(string name, Sprite _sprite)
    {
      _spriteMap.Add(name, _sprite);
    }

    public Sprite getSprite(string name)
    {
      if (_spriteMap.ContainsKey(name))
      {
        return _spriteMap[name];
      }
      else
      {
        return null;
      }
    }

    void Update()
    {

    }
  }

}