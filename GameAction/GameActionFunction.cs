using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionFunction 
{
    /// <summary>
    /// 获取对应动画的时长
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public float GetAniamatorLength(Animator animator,string name)
    {
        float length = 0;
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip item in clips)
        {
            if (item.name.Equals(name))
            {
                length = item.length;
                break;
            }
        }
        return length;
    }
}
