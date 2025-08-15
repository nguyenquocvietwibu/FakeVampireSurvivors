using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SurvivorAnimmation
{
    Disappear,
    Appear,
    Walk,
    Idle,
}

public class SurvivorAnimationHash : MonoBehaviour
{
    [SerializeField] AnimationClip _walk;
    [SerializeField] AnimationClip _idle;
    [SerializeField] AnimationClip _appear;
    [SerializeField] AnimationClip _disappear;


    private readonly Dictionary<SurvivorAnimmation, int> _animationNameDict = new();

    private void Awake()
    {
        _animationNameDict.Add(SurvivorAnimmation.Walk, GetHashFromString(_walk.name));
        _animationNameDict.Add(SurvivorAnimmation.Idle, GetHashFromString(_idle.name));
        _animationNameDict.Add(SurvivorAnimmation.Disappear, GetHashFromString(_disappear.name));
        _animationNameDict.Add(SurvivorAnimmation.Appear, GetHashFromString(_appear.name));
    }

    public int GetAnimationHash(SurvivorAnimmation animmation)
    {
        if (_animationNameDict.ContainsKey(animmation))
        {
            return _animationNameDict[animmation];
        }
        else
        {
            throw new System.Exception($"No key: {animmation}");
        }
    }

    private int GetHashFromString(string convertedContent)
    {
        return Animator.StringToHash(convertedContent);
    }
}
