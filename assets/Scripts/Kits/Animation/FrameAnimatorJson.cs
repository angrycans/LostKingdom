using UnityEngine;
using System;
using System.Collections.Generic;
/*
{
	"name": "desertWorlf",
	"framename": "desertWolf_0%03d.png",
	"sprites": "enemies_desert-hd.png",
	"framerate": 25,
	"clips": [
		{
			"name": "death",
			"start": 69,
			"end": 86
		}, */
namespace Acans.Animation
{
  [System.Serializable]
  public class FrameAnimatorJson
  {
    public string name;
    public string framename;
    public string file;
    public int framerate;

    public List<AniClip> clips;



  }
  [Serializable]

  public class AniClip
  {
    public int start;
    public int end;

    public string name;


  }




}