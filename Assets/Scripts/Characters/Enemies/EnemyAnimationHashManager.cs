using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAnimmation
{
    Disappear,
    Appear,
    Walk,
    Idle,
}
public class EnemyAnimationHashManager : MonoBehaviour
{

    [SerializeField] AnimationClip _walk;
    [SerializeField] AnimationClip _idle;
    [SerializeField] AnimationClip _appear;
    [SerializeField] AnimationClip _disappear;


    private readonly Dictionary<EnemyAnimmation, int> _animationNameDict = new();

    private void Awake()
    {
        _animationNameDict.Add(EnemyAnimmation.Walk, GetHashFromString(_walk.name));
        _animationNameDict.Add(EnemyAnimmation.Idle, GetHashFromString(_idle.name));
        _animationNameDict.Add(EnemyAnimmation.Disappear, GetHashFromString(_disappear.name));
        _animationNameDict.Add(EnemyAnimmation.Appear, GetHashFromString(_appear.name));
    }

    public int GetAnimationHash(EnemyAnimmation animmation)
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
