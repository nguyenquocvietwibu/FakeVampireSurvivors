using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Survivor : MonoBehaviour, IDamageable
{

    [SerializeField] Rigidbody2D _rb2D;
    [SerializeField] SurvivorMovementCalculator _movementCalculator;
    [SerializeField] Animator _animator;
    [SerializeField] SurvivorAnimationHashManager _animationhashManager;
    [SerializeField] SpriteRenderer _spriteRenderer;
    private StateManager<SurvivorState> _stateManager;
    private FiniteStateMachine _fsm;

    public static event UnityAction<Vector2> PositionChanged;

    [SerializeField] private string currentState;
    private void Awake()
    {
        Camera.main.transform.SetParent(transform);
        _movementCalculator = GetComponent<SurvivorMovementCalculator>();
        _animator = GetComponent<Animator>();
        _animationhashManager = GetComponent<SurvivorAnimationHashManager>();
        _rb2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _stateManager = new StateManager<SurvivorState>();
        _stateManager.AddState(typeof(SurvivorIdleState), new SurvivorIdleState(this));
        _stateManager.AddState(typeof(SurvivorWalkState), new SurvivorWalkState(this));
    }
    void Start()
    {
        Application.targetFrameRate = 120;
        _fsm = new FiniteStateMachine(_stateManager.GetState(typeof(SurvivorIdleState)));
    }
    private void Update()
    {
        currentState = _fsm.currentState.ToString();
        _fsm.UpdateState();
    }
    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void Move(Vector2 movement)
    {
        _rb2D.velocity = movement;
        PositionChanged?.Invoke(_rb2D.position);
    }
    public Vector2 GetVelocity()
    {
        return _rb2D.velocity;
    }

    public void SwitchState(SurvivorState switchedSurvivorState)
    {
        _fsm.SwitchState(switchedSurvivorState);
    }

    public SurvivorState GetState<T>() where T : SurvivorState
    {
        return _stateManager.GetState(typeof(T));
    }

    public Vector2 GetMovement()
    {
        if (_movementCalculator != null)
        {
            return _movementCalculator.movement;
        }
        else return Vector2.zero;
    }

    public void PlayAnimation(SurvivorAnimmation animmation)
    {
        if (_animationhashManager != null)
        {
            _animator.Play(_animationhashManager.GetAnimationHash(animmation));
        }
    }

    public void CheckFlipXSprite()
    {
        if (_spriteRenderer != null)
        {
            float currentXDirection = GetMovement().x;
            bool isFacingLeft = _spriteRenderer.flipX;
            if (currentXDirection > 0 && isFacingLeft)
            {
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
            }
            else if (currentXDirection < 0 && !isFacingLeft)
            {
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
            }
        } 
    }

    public void Damage(float damage)
    {
        Debug.Log("Player is damaged");
    }
}
