using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorMovementCalculator : MovementCalculator
{

    private void OnEnable()
    {
        GameInputReader.GameplayMoved += OnCalculateMovement;
    }

    private void OnDisable()
    {
        GameInputReader.GameplayMoved -= OnCalculateMovement;
    }

    private void OnCalculateMovement(Vector2 movement)
    {
        _normalizedMovement = movement;
    }
}
