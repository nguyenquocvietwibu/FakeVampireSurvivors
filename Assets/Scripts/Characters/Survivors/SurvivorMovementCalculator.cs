using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorMovementCalculator : MonoBehaviour
{
    [SerializeField] private Vector2 _movement;
    public Vector2 movement => _movement;

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
        _movement = movement;
    }
}
