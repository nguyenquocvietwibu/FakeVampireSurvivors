using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour
{
    [SerializeField] private StatManager _statManager;

    private void Awake()
    {
        _statManager.GetComponent<StatManager>();
    }
}
