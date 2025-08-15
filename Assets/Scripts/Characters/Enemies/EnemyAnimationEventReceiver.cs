using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimationEventReceiver : MonoBehaviour
{
    public event UnityAction AppearCompleted;
    private void OnCompleteAppear()
    {
        AppearCompleted?.Invoke();
    }
}
