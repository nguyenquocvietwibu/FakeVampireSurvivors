using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    [SerializeField] protected Rigidbody2D _rb2D;
    [SerializeField] protected MovementCalculator _movementCalculator;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected StatManager _statManager;
    [SerializeField] protected CharacterEventReceiver _eventReceiver;
    [SerializeField] protected string _currentStateName;
    [SerializeField] protected bool _canMove;
    protected StateManager<CharacterState> _stateManager;
    protected FiniteStateMachine _fsm;

    public bool CanMove => _canMove;

    protected virtual void Awake()
    {
        _statManager = GetComponent<StatManager>();
        _movementCalculator = GetComponent<MovementCalculator>();
        _animator = GetComponent<Animator>();
        _eventReceiver = GetComponent<CharacterEventReceiver>();
        _rb2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _stateManager = new StateManager<CharacterState>();
        _stateManager.AddState(new CharacterWalkState(this));
    }

    protected virtual void OnEnable()
    {
        _eventReceiver.DisappearCompleted += OnCompleteDisappear;
    }

    protected virtual void OnDisable()
    {
        _eventReceiver.DisappearCompleted -= OnCompleteDisappear;
    }
    protected virtual void Update()
    {
        UpdateState();
    }
    public virtual void PlayAnimation(int animationHash)
    {
        _animator.Play(animationHash);
    }


    protected virtual void OnCompleteDisappear()
    {

    }

    public virtual CharacterState GetState<T>() where T : CharacterState
    {
        return _stateManager.GetState(typeof(T));
    }

    public virtual Vector2 GetNormalizedMovement()
    {
        return _movementCalculator.normalizedMovement;
    }

    public virtual void SwitchState(CharacterState characterState)
    {
        _fsm.SwitchState(characterState);
    }

    public virtual Vector2 GetVelocity()
    {
        return _rb2D.velocity;
    }

    public void CheckFlipXSprite()
    {
        float currentXDirection = GetNormalizedMovement().x;
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
    public bool TryGetStatKey(Stat stat, out Stat statKeyResult)
    {
        return _statManager.TryGetStatKey(stat, out statKeyResult);
    }

    public bool TryGetStatValue(Stat stat, out float StatValueResult)
    {
        return _statManager.TryGetStatValue(stat, out StatValueResult);
    }

    public void Move(Vector2 movement)
    {
        _rb2D.velocity = movement;
    }

    protected virtual void UpdateState()
    {
        _currentStateName = _fsm.currentState.ToString();
        _fsm.UpdateState();
    }
}
