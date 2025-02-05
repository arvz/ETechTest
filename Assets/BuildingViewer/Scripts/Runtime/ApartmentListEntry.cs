using EnvizTest.Core.Messaging;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ApartmentListEntry : Selectable, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _hoveredColor;

    [Header("Self References")]
    [SerializeField] private Image _backgroundPanelImage;
    [SerializeField] private Image _selectionBorderImage;
    [SerializeField] private TextMeshProUGUI _apartmentNumberText;
    [SerializeField] private TextMeshProUGUI _bedText;
    [SerializeField] private TextMeshProUGUI _bathText;
    [SerializeField] private TextMeshProUGUI _parkingText;
    
    private ApartmentData _apartmentData;

    public void Initialize(ApartmentData apartmentData)
    {
        _apartmentData = apartmentData;
        _apartmentNumberText.text = $"Apartment {_apartmentData.apartmentNumber}";
        _bedText.text = _apartmentData.bed.ToString();
        _bathText.text = _apartmentData.bath.ToString();
        _parkingText.text = _apartmentData.parking.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tween.Color(_backgroundPanelImage, _hoveredColor, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tween.Color(_backgroundPanelImage, _normalColor, 0.2f);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        _selectionBorderImage.gameObject.SetActive(true);
        Messaging.SendMessage(MessageType.ApartmentEntryClicked, _apartmentData);
    }
    
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        _selectionBorderImage.gameObject.SetActive(false);
    }
}