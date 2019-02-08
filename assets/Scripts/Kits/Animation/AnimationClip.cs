
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using Acans.Tools;
namespace Acans.Animation
{
  public class AnimationClip
  {
    public string name;

    public float fps = 0.1f;
    private float frameInterval;  // Counts time between frames
    public List<Sprite> frames = new List<Sprite>();
    public bool loop;
    public bool stop = false;
    /*
      
      "none" or ""  clear flip    default
      "x"  flipx =true
      "y"  flipy=true
      "inherit"  inherit last flip
     */
    public string flip;
    private int index = 0;
    // public UnityEvent startEvents;
    // public UnityEvent finishEvents;
    // Index of current frame
    [HideInInspector]
    public Sprite currentSprite;
    // private static readonly float MAX_FRAME_TIME = 0.04f;    // Time between frames

    private void Awake()
    {
      Log.info("AnimationClip Awake");
      currentSprite = frames[0];
      frameInterval = fps;
    }
    public void ResetIndex()
    {
      index = 0;
    }
    public Sprite Animate(float delta)
    {
      // Calls start event on frame 0
      //if (index == 0) startEvents.Invoke();

      if (frames.Count > 1)
      {
        if (stop)
        {
          return currentSprite ? currentSprite : frames[0];
        }
        frameInterval -= delta;

        if (frameInterval <= 0)
        {
          if (index < frames.Count - 1)
          {
            index++;
            currentSprite = frames[index];
            //Log.info(index, delta, frameInterval, fps);
            if (index == frames.Count - 1)
            {
              if (loop) index = 0;
              else
              {

                // Try something with finishTriggers here, if finishTriggers is even necessary in the first place
                //finishEvents.Invoke();
              }
            }
          }
          frameInterval = fps;
        }
      }
      else
      {

        Log.info("frames count", frames.Count);
        currentSprite = frames[0]; // Maybe could use List<> for frames and check hasNext() instead of a conditional for a one-frame animation?
      }

      return currentSprite;
    }

  }
}