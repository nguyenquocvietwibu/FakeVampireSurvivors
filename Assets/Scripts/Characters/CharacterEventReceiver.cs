using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEventReceiver : MonoBehaviour
{
    public event UnityAction DisappearCompleted;
    protected virtual void OnCompleteDisAppear()
    {
        DisappearCompleted?.Invoke();
    }

}
