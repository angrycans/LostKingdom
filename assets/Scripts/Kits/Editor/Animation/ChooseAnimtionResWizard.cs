using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEditor;
using System;



namespace Acans.Tools
{
  public class ChooseAnimtionResWizard : Editor
  {
    public class ChooseAnimtionRes : ScriptableWizard
    {
      public Sprite sprite;
      public UnityEngine.Object JsonFile;
      private string filePath;
      void OnWizardCreate()
      {
        filePath = AssetDatabase.GetAssetPath(JsonFile);
        Log.info("OnWizardCreate", filePath);
        Log.info(FileUtils.GetFileName(filePath));
        AnimationClip clip = new AnimationClip();
        clip.frameRate = 24;
        AssetDatabase.CreateAsset(clip, "Assets/Animations/" + FileUtils.GetFileName(filePath) + ".animclip");
        AssetDatabase.SaveAssets();
      }

      private Transform m_AnimatorPrefab;

      [MenuItem("AcansTools/Animator/CreateAnimatorFromJson")]
      public static void createAnimator()
      {
        ScriptableWizard.DisplayWizard<ChooseAnimtionRes>("ChooseRes", "Apply");
      }
    }


  }
}