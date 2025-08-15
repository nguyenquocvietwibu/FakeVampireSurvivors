using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementCalculator : MonoBehaviour
{
    [SerializeField] private Vector2 _movement;
    public Vector2 movement => _movement;

    private void Update()
    {
        _movement = Vector2.ClampMagnitude(SurvivorPositionCalculator.GetCurrentPosition() - (Vector2)transform.position, 1);
    }

}
