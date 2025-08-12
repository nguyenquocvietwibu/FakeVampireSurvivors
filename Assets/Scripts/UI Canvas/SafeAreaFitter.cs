using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaFitter : MonoBehaviour
{
    [SerializeField] private RectTransform _rt;
    [SerializeField] private Rect _safeArea;
    [SerializeField] private Rect _currentScreen;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        ApplyFitter();
    }

    private void OnRectTransformDimensionsChange()
    {
        ApplyFitter();
    }

    private void ApplyFitter()
    {
        _safeArea = Screen.safeArea;
        _currentScreen = new Rect(0, 0, Screen.width, Screen.height);
        _rt.anchorMin = new Vector2((_safeArea.x != _currentScreen.x) ? (_safeArea.x / _safeArea.width) : 0, (_safeArea.y != _currentScreen.y) ? (_safeArea.y / _safeArea.height) : 0);
        _rt.anchorMax = new Vector2((_safeArea.width + _safeArea.x) / _currentScreen.width, (_safeArea.height + _safeArea.y) / _currentScreen.height);
    }
}
