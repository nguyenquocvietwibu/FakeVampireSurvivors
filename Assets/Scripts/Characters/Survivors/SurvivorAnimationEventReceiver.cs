using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SurvivorAnimationEventReceiver : MonoBehaviour
{
    public event UnityAction AppearCompleted;
    private void OnCompleteAppear()
    {
        AppearCompleted?.Invoke();
        Debug.Log("Survivor Appear Completed");
    }
}
