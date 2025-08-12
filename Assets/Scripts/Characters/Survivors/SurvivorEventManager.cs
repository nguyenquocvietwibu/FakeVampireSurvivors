using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SurvivorEventManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2D;

    public static event UnityAction<Vector2> SurvivorPositionChanged;
    private Vector2 _lastSurvivorPosition;

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
    }
}
