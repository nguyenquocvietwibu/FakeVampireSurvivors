using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderBar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Vector2 _spriteSize;
    [SerializeField] private Transform _fillTF;

    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;
    [SerializeField] private float _currentValue;
    public float minValue { get { return _minValue; } set { _minValue = value; } }
    public float maxValue { get { return _maxValue; } set { _maxValue = value; } }
    public float currentValue
    {
        get
        {
            return _currentValue;
        }
        set 
        {
            _currentValue = Mathf.Clamp(value, _minValue, _maxValue);
            Fill();
        }
    }
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteSize = _spriteRenderer.size;
    }

    private void OnValidate()
    {
        Fill();
    }
    public void Fill()
    {
        _fillTF.localPosition = new Vector3(-_spriteSize.x * (1 - _currentValue / maxValue), 0, 0);
    }
}
