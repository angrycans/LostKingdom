
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

using Acans.Tools;
namespace Acans.Animation
{

  [System.Serializable]
  public class AnimationClip
  {
    public string name;

    public float frameInterval = MAX_FRAME_TIME;  // Counts time between frames
    public Sprite[] frames;
    public bool loop;
    private int index = 0;
    // public UnityEvent startEvents;
    // public UnityEvent finishEvents;
    // Index of current frame
    [HideInInspector]
    public Sprite currentSprite;
    private static readonly float MAX_FRAME_TIME = 0.1f;    // Time between frames

    private void Awake()
    {
      Log.info("AnimationClip Awake");
      currentSprite = frames[0];
    }
    public void ResetIndex()
    {
      index = 0;
    }
    public Sprite Animate(float delta)
    {
      // Calls start event on frame 0
      //if (index == 0) startEvents.Invoke();

      if (frames.Length > 1)
      {
        frameInterval -= delta;

        if (frameInterval <= 0)
        {
          if (index < frames.Length - 1)
          {
            index++;
            currentSprite = frames[index];
            if (index == frames.Length - 1)
            {
              if (loop) index = 0;
              else
              {
                // Try something with finishTriggers here, if finishTriggers is even necessary in the first place
                //finishEvents.Invoke();
              }
            }
          }
          frameInterval = MAX_FRAME_TIME;
        }
      }
      else
      {
        currentSprite = frames[0]; // Maybe could use List<> for frames and check hasNext() instead of a conditional for a one-frame animation?
      }

      return currentSprite;
    }

  }
}