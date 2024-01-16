using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DodgeAnimationParams
{
    public int currentDodgeAnimIndex = 0;
    public List<AnimatorOverrideController> animatorOverrideControllers = null;
    public List<DodgeAnimData> dodgeAnimsData = null;

    public RuntimeAnimatorController GetAnimationController()
    {
        return animatorOverrideControllers[currentDodgeAnimIndex];
    }

    public DodgeAnimData GetAnimationData()
    {
        return dodgeAnimsData[currentDodgeAnimIndex];
    }
}
