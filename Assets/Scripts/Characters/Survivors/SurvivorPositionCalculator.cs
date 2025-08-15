using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SurvivorPositionCalculator : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2D;

    public static event UnityAction<Vector2> SurvivorPositionChanged;
    private Vector2 _lastSurvivorPosition;
    private static Vector2 _currentPosition;



    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _lastSurvivorPosition = _rb2D.position;
    }

    private void Update()
    {
        if (_lastSurvivorPosition != _rb2D.position)
        {
            _lastSurvivorPosition = _rb2D.position;
            SurvivorPositionChanged?.Invoke(_lastSurvivorPosition);
        }
        CheckCurrentPosition();
    }
    public static Vector2 GetCurrentPosition()
    {
        return _currentPosition;
    }

    private void CheckCurrentPosition()
    {
        if (_rb2D != null)
        {
            _currentPosition = _rb2D.position;
        }
    }
}
