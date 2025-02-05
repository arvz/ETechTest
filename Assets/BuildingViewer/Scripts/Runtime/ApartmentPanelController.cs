using System;
using TriInspector;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class ApartmentPanelController : MonoBehaviour
{
    private RectTransform RectTransform
    {
        get => (RectTransform)transform;
    }
    
    [SerializeField] private Transform _worldTransform;
    [SerializeField] private Vector2 _offset;
    
    private Camera _camera;
    private RectTransform _rectTransform;
    
    private void Awake()
    {
        _camera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_worldTransform == null)
        {
            return;
        }
        
        var screenPoint = RectTransformUtility.WorldToScreenPoint(_camera, _worldTransform.position);
        RectTransform.position = screenPoint + _offset;
    }

    public void Initialize(Transform worldTransformObject)
    {
        _worldTransform = worldTransformObject;
    }
}