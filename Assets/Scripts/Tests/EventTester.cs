using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}

public class StartedEventArgs : EventArgs
{
    public int CurrentHealth { get; }
    public int MaxHealth { get; }

    public StartedEventArgs(int current, int max)
    {
        CurrentHealth = current;
        MaxHealth = max;
    }
}
