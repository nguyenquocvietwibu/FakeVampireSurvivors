using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScreenMatchFitter : MonoBehaviour
{
    [SerializeField] Vector2 _currentScreen;
    [SerializeField] CanvasScaler _canvasScaler;

    private void Awake()
    {
        _canvasScaler = GetComponent<CanvasScaler>();
        ApplyCurrentScreenMatch();
    }

    private void ApplyWidthMatch()
    {
        _canvasScaler.matchWidthOrHeight = 0;
    }
    private void ApplyHeightMatch()
    {
        _canvasScaler.matchWidthOrHeight = 0.5f;
    }

    private void OnRectTransformDimensionsChange()
    {
        ApplyCurrentScreenMatch();
    }

    private void ApplyCurrentScreenMatch()
    {
        _currentScreen = new Vector2(Screen.width, Screen.height);
        float currentScreenRatio = _currentScreen.x / _currentScreen.y;
        float refRatio = _canvasScaler.referenceResolution.x / _canvasScaler.referenceResolution.y;
        //Debug.Log($"{currentScreenRatio} > {refRatio} is {currentScreenRatio > refRatio}");
       
        if (currentScreenRatio > refRatio)
        {
            ApplyHeightMatch();
        }
        else ApplyWidthMatch();
    }
}
