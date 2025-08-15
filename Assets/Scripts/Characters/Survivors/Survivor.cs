using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Survivor : Character, IDamageable
{
    [SerializeField] SliderBar _healthSliderBar;


    protected override void Awake()
    {
        base.Awake();
        Camera.main.transform.SetParent(transform);
        _stateManager.AddState(new CharacterIdleState(this));
        _fsm = new FiniteStateMachine(GetState<CharacterIdleState>());
        _canMove = true;
    }

    protected override void Update()
    {
        base.Update();
    }

    public void SwitchState(SurvivorState switchedSurvivorState)
    {
        _fsm.SwitchState(switchedSurvivorState);
    }
    public Vector2 GetMovement()
    {
        if (_movementCalculator != null && _canMove)
        {
            return _movementCalculator.normalizedMovement;
        }
        else return Vector2.zero;
    }

    
    public void Damage(float damage)
    {
        Debug.Log("Damage Value: " + damage);
        Stat healthKey;
        float currentHealthValue;
        if (TryGetStatKey(Stat.Health, out healthKey))
        {
            if (TryGetStatValue(Stat.Health, out currentHealthValue))
            {
                float _changedHealthValue = currentHealthValue - damage;
                if (_changedHealthValue > 0)
                {
                    _statManager.SetStatValue(healthKey, _changedHealthValue);
                }
                else
                {
                    Die();
                }
                SetCurrentHealthSliderValue(_changedHealthValue);

            }
        }

    }

    public void SetCurrentHealthSliderValue(float currentHealth)
    {
        _healthSliderBar.currentValue = currentHealth;
    }

    [ContextMenu("Test Damage")]
    public void TestDamage()
    {
        Damage(40);
    }

    private void Die()
    {
        PlayAnimation(CharacterAnimationHashManager.disappear);
        _canMove = false;
    }

}
