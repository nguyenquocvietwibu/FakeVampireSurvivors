using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterAnimmation
{
    Disappear,
    Appear,
    Walk,
    Idle,
}

public class CharacterAnimationHash : MonoBehaviour
{
    [SerializeField] AnimationClip _walk;
    [SerializeField] AnimationClip _idle;
    [SerializeField] AnimationClip _appear;
    [SerializeField] AnimationClip _disappear;

    private readonly Dictionary<CharacterAnimmation, int> _animationHashDict = new();

    private void Awake()
    {
        _animationHashDict.Add(CharacterAnimmation.Walk, GetHashFromString(_walk.name));
        _animationHashDict.Add(CharacterAnimmation.Idle, GetHashFromString(_idle.name));
        _animationHashDict.Add(CharacterAnimmation.Disappear, GetHashFromString(_disappear.name));
        _animationHashDict.Add(CharacterAnimmation.Appear, GetHashFromString(_appear.name));
    }

    /// <summary>
    /// Lấy ra animation hash của character
    /// </summary>
    /// <param name="animmation"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public int GetAnimationHash(CharacterAnimmation animmation)
    {
        if (_animationHashDict.ContainsKey(animmation))
        {
            return _animationHashDict[animmation];
        }
        else
        {
            throw new System.Exception($"No key: {animmation}");
        }
    }

    /// <summary>
    /// Lấy ra mã băm từ chuỗi
    /// </summary>
    /// <param name="convertedContent"></param>
    /// <returns></returns>
    private int GetHashFromString(string convertedContent)
    {
        return Animator.StringToHash(convertedContent);
    }
}

