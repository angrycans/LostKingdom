
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using Newtonsoft.Json;
using UniRx;
using Acans.Tools;
using System.Threading.Tasks;
using Acans.Manager;
namespace Acans.Animation
{
  public class FrameAnimator : MonoBehaviour
  {

    public string name;
    public string filename;
    //public Sprite[] sprites;
    public List<AnimationClip> animationClips = new List<AnimationClip>();
    public SpriteRenderer _spriteRenderer;
    // [HideInInspector]
    public AnimationClip currentAnimation = null, previousAnimation = null;

    async private void Awake()
    {
      //We don't want this stuff to execute at runtime
      await Load();
      if (gameObject.GetComponent<SpriteRenderer>() != null)
      {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
      }
      else
      {
        _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
      }
    }

    async Task Load()
    {

      Log.info("filename=>", filename);

      //FileUtils.LoadFromSteamAssets
      if (String.IsNullOrEmpty(filename))
      {
        Log.error("FrameAnimation filename is null");
        return;
      }

      var ret = await FileUtils.LoadFromSteamAssets(filename);
      Log.info("ret=>", ret.text);

      var frameAnimationJson = JsonConvert.DeserializeObject<FrameAnimatorJson>(ret.text);


      Objecter.Dump(frameAnimationJson);

      name = frameAnimationJson.name;

      var ab = await FileUtils.LoadFromSteamAssets(frameAnimationJson.file);
      ab.assetBundle.Unload(false);

      Sprite[] spriteSheet = ab.assetBundle.LoadAssetWithSubAssets<Sprite>("enemies_desert-hd");
      if (spriteSheet.Length == 0)
      {
        Log.info("assetBundle.LoadAssetWithSubAssets load sprite[] is null");
        return;
      }

      foreach (Sprite subSprite in spriteSheet)
      {
        Log.info("sprite=>", subSprite.name);
        ResourceManager.Instance.addSprite(subSprite.name, subSprite);
      }
      Resources.UnloadUnusedAssets();


      frameAnimationJson.clips.ForEach((item) =>
      {
        List<Sprite> _frames = new List<Sprite>();
        Sprite last_sprite = null;
        for (var i = item.start; i <= item.end; i++)
        {
          //Log.info(string.Format(frameAnimationJson.framename, i));
          Sprite _sprite = ResourceManager.Instance.getSprite(string.Format(frameAnimationJson.framename, i));
          if (_sprite == null)
          {
            if (i == item.start)
            {
              Log.error("frameAnimation Load error,frames.firstframe is null");
              break;
            }
            if (last_sprite)
            {
              _sprite = last_sprite;
            }
            else
            {
              Log.error("frameAnimation Load error,frames.last_sprite is null");
            }
          }
          else
          {
            last_sprite = _sprite;
          }
          _frames.Add(_sprite);
        }
        AddAnimation(item.name, _frames, frameAnimationJson.framerate, item.flip != null ? item.flip : "");
      });

      if (animationClips.Count > 0)
      {
        currentAnimation = animationClips[0];
        currentAnimation.stop = true;
      }

      // PlayAnimation("runleft");
    }
    public void PlayAnimation(string Name)
    {
      //code to actually switch your animation state to the animation name specified that's stored in animList
      currentAnimation = animationClips.Find(item => item.name.Contains(Name));
      currentAnimation.stop = false;

    }

    public void StopAnimation()
    {
      currentAnimation.stop = true;
    }


    public List<string> GetAnimation()
    {
      var names = new List<string>();
      animationClips.ForEach((clip) =>
      {
        names.Add(clip.name);
      });

      return names;
    }


    public void AddAnimation(string Name, List<Sprite> Frames, int FrameRate = 30, string Flip = "", bool Loop = true)
    {
      AnimationClip newAnimation = new AnimationClip();
      newAnimation.name = Name;
      newAnimation.frames = Frames;
      newAnimation.fps = 1 / (float)FrameRate;
      newAnimation.loop = Loop;
      newAnimation.flip = Flip.ToLower();
      animationClips.Add(newAnimation);

    }
    public void FixedUpdate()
    {
      if (currentAnimation != null)
      {
        //Log.info("FixedUpdate", currentAnimation.name);
        if (previousAnimation == null) previousAnimation = currentAnimation;
        if (currentAnimation != previousAnimation)
        {
          currentAnimation.ResetIndex();
          previousAnimation = currentAnimation;
        }
        _spriteRenderer.sprite = currentAnimation.Animate(Time.deltaTime);

        switch (currentAnimation.flip)
        {
          case "":
            _spriteRenderer.flipX = false;
            _spriteRenderer.flipY = false;
            break;
          case "none":
            _spriteRenderer.flipX = false;
            _spriteRenderer.flipY = false;
            break;

          case "x":
            _spriteRenderer.flipX = true;
            break;
          case "y":
            _spriteRenderer.flipY = true;
            break;
          case "inherit":
            break;
        }
      }

    }

  }
}