using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class ApartmentPanelController : MonoBehaviour
{
    private RectTransform RectTransform
    {
        get
        {
            if (_rectTransform == null)
            {
                _rectTransform = (RectTransform)transform;
            }

            return _rectTransform;
        }
    }
    
    [Header("Settings")]
    [SerializeField] private Vector2 _offset;
    
    [Header("Self References")]
    [SerializeField] private TextMeshProUGUI _apartmentNumberText;
    [SerializeField] private TextMeshProUGUI _bedText;
    [SerializeField] private TextMeshProUGUI _bathText;
    [SerializeField] private TextMeshProUGUI _parkingText;
    
    [Header("Scene References")]
    [SerializeField] private Transform _worldTransform;
    
    private Camera _camera;
    private RectTransform _rectTransform;
    private ApartmentData _apartmentData;
    
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

    public void Initialize(Transform worldTransformObject, ApartmentData apartmentData)
    {
        gameObject.SetActive(false);
        _worldTransform = worldTransformObject;
        _apartmentData = apartmentData;
        
        _apartmentData = apartmentData;
        _apartmentNumberText.text = $"Apartment {_apartmentData.apartmentNumber}";
        _bedText.text = _apartmentData.bed.ToString();
        _bathText.text = _apartmentData.bath.ToString();
        _parkingText.text = _apartmentData.parking.ToString();
    }
}