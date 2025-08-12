using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;



public class MapScrollAroundSurvivorController : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Transform _currentMapTFSurvivorStand;
    [SerializeField] private Transform _clonedMapTF2rd;
    [SerializeField] private Transform _clonedMapTF3nd;
    [SerializeField] private Transform _clonedMapTF4th;
    [SerializeField] Vector3 _tilemapSize;
    [SerializeField] MapSection _currentMapSectionSurvivorStand;
    private enum MapSection
    {
        UpperLeft,
        UpperRight,
        LowerLeft,
        LowerRight,
    }
    private void Awake()
    {
        if (_tilemap != null)
        {
            _tilemapSize = _tilemap.size;
            _currentMapTFSurvivorStand = _tilemap.transform.parent;
            _clonedMapTF2rd = Instantiate(_currentMapTFSurvivorStand.gameObject, transform).transform;
            _clonedMapTF3nd = Instantiate(_currentMapTFSurvivorStand.gameObject, transform).transform;
            _clonedMapTF4th = Instantiate(_currentMapTFSurvivorStand.gameObject, transform).transform;
        }

    }

    private Transform GetCloneMapTF()
    {
        return Instantiate(_tilemap.transform.parent.gameObject, transform).transform;
    }

    private void OnEnable()
    {
        SurvivorEventManager.SurvivorPositionChanged += OnChangeSurvivorPosition;
    }
    private void OnDisable()
    {
        SurvivorEventManager.SurvivorPositionChanged -= OnChangeSurvivorPosition;
    }

    private void OnChangeSurvivorPosition(Vector2 changedSurvivorPosition)
    {
        Vector2 currentMapPosition = _currentMapTFSurvivorStand.position;
        if (changedSurvivorPosition.y < currentMapPosition.y)
        {
            if (changedSurvivorPosition.x < currentMapPosition.x) SwitchedMapSection(MapSection.LowerLeft);
            else SwitchedMapSection(MapSection.LowerRight);
        }
        else
        {
            if (changedSurvivorPosition.x < currentMapPosition.x) SwitchedMapSection(MapSection.UpperLeft);
            else SwitchedMapSection(MapSection.UpperRight);
        }
        if (changedSurvivorPosition.x < currentMapPosition.x - _tilemapSize.x)
        {
            SetCurrentMapTFSurvivorStandPosition(_currentMapTFSurvivorStand.position + new Vector3(-_tilemapSize.x, 0, 0));
        }
        else if (changedSurvivorPosition.x >= currentMapPosition.x + _tilemapSize.x)
        {
            SetCurrentMapTFSurvivorStandPosition(_currentMapTFSurvivorStand.position + new Vector3(_tilemapSize.x, 0, 0));
        }
        if (changedSurvivorPosition.y < currentMapPosition.y - _tilemapSize.y)
        {
            SetCurrentMapTFSurvivorStandPosition(_currentMapTFSurvivorStand.position += new Vector3(0, -_tilemapSize.y, 0));
        }
        else if (changedSurvivorPosition.y > currentMapPosition.y + _tilemapSize.y)
        {
            SetCurrentMapTFSurvivorStandPosition(_currentMapTFSurvivorStand.position += new Vector3(0, _tilemapSize.y, 0));
        }
    }

    private void SwitchedMapSection(MapSection switchedMapsection)
    {
        if (_currentMapSectionSurvivorStand != switchedMapsection)
        {
            _currentMapSectionSurvivorStand = switchedMapsection;
            ScrollMap();
        }
        
    }

    private void SetCurrentMapTFSurvivorStandPosition(Vector3 position)
    {
        _currentMapTFSurvivorStand.position = position;
        ScrollMap();
    }

    private void ScrollMap()
    {
        switch (_currentMapSectionSurvivorStand)
        {
            case MapSection.UpperLeft:
                ScrollUpperLeftSBracketShaped();
                break;
            case MapSection.UpperRight:
                ScrollUpperRightSBracketShaped();
                break;
            case MapSection.LowerLeft:
                ScrollLowerLeftSBracketShaped();
                break;
            case MapSection.LowerRight:
                ScrollLowerRightSBracketShaped();
                break;
        }
    }

    /// <summary>
    /// Scroll map theo dấu ngoặc vuông trái được vẽ từ dưới lên trên
    /// </summary>
    private void ScrollUpperLeftSBracketShaped()
    {
        _clonedMapTF2rd.position = new Vector3(_currentMapTFSurvivorStand.position.x - _tilemapSize.x, _currentMapTFSurvivorStand.position.y, 0);
        _clonedMapTF3nd.position = new Vector3(_clonedMapTF2rd.position.x, _clonedMapTF2rd.position.y + _tilemapSize.y, 0);
        _clonedMapTF4th.position = new Vector3(_currentMapTFSurvivorStand.position.x, _currentMapTFSurvivorStand.position.y + _tilemapSize.y, 0);
    }

    /// <summary>
    /// Scroll map theo dấu ngoặc vuông trái được vẽ từ trên xuống dưới
    /// </summary>
    private void ScrollLowerLeftSBracketShaped()
    {
        _clonedMapTF2rd.position = new Vector3(_currentMapTFSurvivorStand.position.x - _tilemapSize.x, _currentMapTFSurvivorStand.position.y, 0);
        _clonedMapTF3nd.position = new Vector3(_clonedMapTF2rd.position.x, _clonedMapTF2rd.position.y - _tilemapSize.y, 0);
        _clonedMapTF4th.position = new Vector3(_currentMapTFSurvivorStand.position.x, _currentMapTFSurvivorStand.position.y - _tilemapSize.y, 0);
    }
    /// <summary>
    /// Scroll map theo dấu ngoặc vuông phải được vẽ từ dưới lên trên
    /// </summary>
    private void ScrollUpperRightSBracketShaped()
    {
        _clonedMapTF2rd.position = new Vector3(_currentMapTFSurvivorStand.position.x + _tilemapSize.x, _currentMapTFSurvivorStand.position.y, 0);
        _clonedMapTF3nd.position = new Vector3(_clonedMapTF2rd.position.x, _clonedMapTF2rd.position.y + _tilemapSize.y, 0);
        _clonedMapTF4th.position = new Vector3(_currentMapTFSurvivorStand.position.x, _currentMapTFSurvivorStand.position.y + _tilemapSize.y, 0);
    }
    /// <summary>
    /// Scroll map theo dấu ngoặc vuông phải được vẽ từ trên xuống dưới
    /// </summary>
    private void ScrollLowerRightSBracketShaped()
    {
        _clonedMapTF2rd.position = new Vector3(_currentMapTFSurvivorStand.position.x + _tilemapSize.x, _currentMapTFSurvivorStand.position.y, 0);
        _clonedMapTF3nd.position = new Vector3(_clonedMapTF2rd.position.x, _clonedMapTF2rd.position.y - _tilemapSize.y, 0);
        _clonedMapTF4th.position = new Vector3(_currentMapTFSurvivorStand.position.x, _currentMapTFSurvivorStand.position.y - _tilemapSize.y, 0);
    }
}
