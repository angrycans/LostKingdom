using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEditor;

namespace Acans
{
  public class CreateAnimatorController : Editor
  {

    [MenuItem("AcansTools/Animator/Create Animator Controller")]
    static void CreateController()
    {
      // Creates the controller
      AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath("Assets/Animations/Motion.controller");

      // Add Animation Clips
      AnimationClip clip = new AnimationClip();
      clip.frameRate = 24;
      AssetDatabase.CreateAsset(clip, "Assets/Animations/Motion.anim");
      AssetDatabase.SaveAssets();

      // Add parameters
      controller.AddParameter("TransitionNow", AnimatorControllerParameterType.Trigger);
      controller.AddParameter("Reset", AnimatorControllerParameterType.Trigger);
      controller.AddParameter("GotoB1", AnimatorControllerParameterType.Trigger);
      controller.AddParameter("GotoC", AnimatorControllerParameterType.Trigger);

      // Add StateMachines
      AnimatorStateMachine rootStateMachine = controller.layers[0].stateMachine;
      AnimatorStateMachine stateMachineA = rootStateMachine.AddStateMachine("smA");
      AnimatorStateMachine stateMachineB = rootStateMachine.AddStateMachine("smB");
      AnimatorStateMachine stateMachineC = stateMachineB.AddStateMachine("smC");

      // Add Clip


      // Add States
      AnimatorState stateA1 = stateMachineA.AddState("stateA1");
      AnimatorState stateB1 = stateMachineB.AddState("stateB1");
      AnimatorState stateB2 = stateMachineB.AddState("stateB2");
      stateMachineC.AddState("stateC1");
      AnimatorState stateC2 = stateMachineC.AddState("stateC2"); // don’t add an entry transition, should entry to state by default

      // Add clip
      stateA1.motion = clip;

      // Add Transitions
      AnimatorStateTransition exitTransition = stateA1.AddExitTransition();
      exitTransition.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "TransitionNow");
      exitTransition.duration = 0;

      AnimatorStateTransition resetTransition = rootStateMachine.AddAnyStateTransition(stateA1);
      resetTransition.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "Reset");
      resetTransition.duration = 0;

      AnimatorTransition transitionB1 = stateMachineB.AddEntryTransition(stateB1);
      transitionB1.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "GotoB1");
      stateMachineB.AddEntryTransition(stateB2);
      stateMachineC.defaultState = stateC2;
      AnimatorStateTransition exitTransitionC2 = stateC2.AddExitTransition();
      exitTransitionC2.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "TransitionNow");
      exitTransitionC2.duration = 0;

      AnimatorTransition stateMachineTransition = rootStateMachine.AddStateMachineTransition(stateMachineA, stateMachineC);
      stateMachineTransition.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "GotoC");
      rootStateMachine.AddStateMachineTransition(stateMachineA, stateMachineB);
    }


  }
}