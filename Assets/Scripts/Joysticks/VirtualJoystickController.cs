using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class VirtualJoystickController : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Image _joystickBackground;
    [SerializeField] private Image _joystickHandle;
    [SerializeField] private RectTransform _joystickRT;
    [SerializeField] private Vector2 _defaultJoystickPosition;
    [SerializeField] private Vector2 _joystickpointerInput;
    [SerializeField] private RectTransform _joystickZoneRT;
    [SerializeField] private float _joystickBackgroundSize;
    [SerializeField] private float _joystickRadius;
    [SerializeField] private Vector2 _joystickPointerValue;
    [SerializeField] private float _distance;
    [SerializeField] private Vector2 _defaultJoystickAnchorMin;
    [SerializeField] private Vector2 _defaultJoystickAnchorMax;

    // Sử dụng [InputControl] để hiển thị trình chọn control path trong Inspector
    [SerializeField, InputControl()]
    private string _controlPath;

    protected override string controlPathInternal
    {
        get => _controlPath;
        set => _controlPath = value;
    }

    private void Awake()
    {
        
        RectTransform currentPanelTransform = GetComponent<RectTransform>();
        currentPanelTransform.offsetMax = Vector3.zero;
        currentPanelTransform.offsetMin = Vector3.zero;
        currentPanelTransform.localScale = Vector3.one;
        _joystickRT = _joystickBackground.rectTransform;
        _defaultJoystickPosition = _joystickBackground.rectTransform.localPosition;
        _joystickZoneRT = GetComponent<RectTransform>();
        _defaultJoystickAnchorMin = _joystickRT.anchorMin;
        _defaultJoystickAnchorMax = _joystickRT.anchorMax;
        float joyStickWidth = _joystickBackground.rectTransform.rect.width;
        float joyStickHeight = _joystickBackground.rectTransform.rect.height;
        if (joyStickWidth / joyStickHeight != 1)
        {
            throw new Exception("virtual joystick background không phải hình tròn!");
        }
        else
        {
            _joystickRadius = joyStickWidth / 2;
        }

    }

    private void Start()
    {
        SetJoystickVisible(true);
        _defaultJoystickPosition = _joystickRT.anchoredPosition;
    }

    /// <summary>
    /// Cài nhìn thấy cho joystick
    /// </summary>
    /// <param name="isVisible"> Đại diện cho có được nhìn thấy hay không </param>
    private void SetJoystickVisible(bool isVisible)
    {
        _joystickBackground.enabled = isVisible;
        _joystickHandle.enabled = isVisible;
    }

    /// <summary>
    /// Cài vị trí mặc định cho joystick
    /// </summary>
    private void SetDefaultJoystickPosition()
    {
        SetJoystickDefaultAnchor();
        _joystickBackground.rectTransform.anchoredPosition = _defaultJoystickPosition;
        _joystickHandle.rectTransform.anchoredPosition = Vector3.zero;
       
    }

    /// <summary>
    /// Cài vị trí cho joystick
    /// </summary>
    /// <param name="position"> Vị trí được cài </param>
    private void SetJoystickPosition(Vector2 position)
    {
        SetJoystickMiddleAnchor();
        _joystickBackground.rectTransform.anchoredPosition = position;
    }

    /// <summary>
    /// Cài phần nắm của joystick
    /// </summary>
    /// <param name="position"> Vị trí được cài </param>
    private void SetJoystickHandlePosition(Vector2 position)
    {
        _joystickHandle.rectTransform.anchoredPosition = position;
    }

    /// <summary>
    /// Khi nhấn pointer xuống: di chuyển joystick tới vị trí nhấn 
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickZoneRT, eventData.pressPosition, eventData.pressEventCamera, out Vector2 localJoystickZonePoint))
        {
            SetJoystickPosition(localJoystickZonePoint);
        }
    }

    /// <summary>
    /// Khi thả pointer ra: cài default position cho joystick
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        SetDefaultJoystickPosition();
        _joystickPointerValue = Vector2.zero;
        SendValueToControl(_joystickPointerValue);
    }

    /// <summary>
    /// Khi drag pointer: giới phạm vi kéo joystick trong bán kính của joystick là _joystickRadius
    /// </summary>
    /// <param name="eventData"></param>
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        // Kiểm tra xem có drag trong rect transform của joystick nếu có thì sẽ lấy ra kết quả là điểm cục bộ của rect transform đó
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickRT, eventData.position, eventData.pressEventCamera, out Vector2 localJoystickPoint))
        {
            // Nếu kéo vượt quá bán kính thì sẽ clamp vector 2 đang kéo về vector 2 tối đa được cho phép bán kính của joystick
            // Sau đó di chuyển handle của joystick theo giá trị của localpoint được xử lý và normalize lấy giá trị cho pointerValue
            if (Vector2.Distance(Vector2.zero, localJoystickPoint) >= _joystickRadius)
            {
                Vector2 clampedLocalJoystickPoint = Vector2.ClampMagnitude(localJoystickPoint, _joystickRadius);
                SetJoystickHandlePosition(clampedLocalJoystickPoint);
                _joystickPointerValue = clampedLocalJoystickPoint.normalized;
            }
            // Nếu không vượt quá thì lấy thẳng giá trị của localPoint để di chuyển handle và lấy phần trăm scale của localPoint / độ dài tối đa (bán kính joystick) cho pointerValue
            else
            {

                SetJoystickHandlePosition(localJoystickPoint);
                Vector2 scaledLocalJoystickPoint = localJoystickPoint / _joystickRadius;
                _joystickPointerValue = scaledLocalJoystickPoint;
            }
            // Gửi giá trị đầu vào tới Input System
            SendValueToControl(_joystickPointerValue);
        }
    }

    private void SetJoystickDefaultAnchor()
    {
        _joystickRT.anchorMin = _defaultJoystickAnchorMin;
        _joystickRT.anchorMax = _defaultJoystickAnchorMax;
    }

    private void SetJoystickMiddleAnchor()
    {
        _joystickRT.anchorMin = new Vector2(0.5f, 0.5f);
        _joystickRT.anchorMax = new Vector2(0.5f, 0.5f);
    }
}