
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using Newtonsoft.Json;
using UniRx;
using Acans.Tools;
using System.Threading.Tasks;

namespace Acans.Animation
{
  public class FrameAnimator : MonoBehaviour
  {

    public string name;
    public string filename;
    public Sprite[] sprites;
    private List<AnimationClip> animationClips;
    private SpriteRenderer _spriteRenderer;
    // [HideInInspector]
    private AnimationClip currentAnimation, previousAnimation;

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

      //animationClips.Add()

    }
    public void PlayAnimation(string Name)
    {
      //code to actually switch your animation state to the animation name specified that's stored in animList
      currentAnimation = animationClips.Find(item => item.name.Contains(Name));


    }

    public void AddAnimation(string Name, Sprite[] Frames, int FrameRate = 30, bool Loop = true)
    {
      AnimationClip newAnimation = new AnimationClip();
      newAnimation.name = Name;
      newAnimation.frames = Frames;
      newAnimation.frameInterval = 1 / FrameRate;
      newAnimation.loop = Loop;
      animationClips.Add(newAnimation);

    }
    public void FixedUpdate()
    {
      if (currentAnimation != null)
      {
        Log.info("FixedUpdate");
        if (previousAnimation == null) previousAnimation = currentAnimation;
        if (currentAnimation != previousAnimation)
        {
          currentAnimation.ResetIndex();
          previousAnimation = currentAnimation;
        }
        _spriteRenderer.sprite = currentAnimation.Animate(Time.deltaTime);
      }

    }

  }
}