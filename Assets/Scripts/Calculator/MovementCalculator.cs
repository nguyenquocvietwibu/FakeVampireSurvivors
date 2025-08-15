using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementCalculator : MonoBehaviour
{
    [SerializeField] protected Vector2 _normalizedMovement;

    public Vector2 normalizedMovement => _normalizedMovement;

}
