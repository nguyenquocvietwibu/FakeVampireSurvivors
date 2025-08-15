using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterAnimationHashManager
{
    public static int idle = Animator.StringToHash("Idle");
    public static int walk = Animator.StringToHash("Walk");
    //public static int appear = Animator.StringToHash("Appear");
    public static int disappear = Animator.StringToHash("Disappear");
}
