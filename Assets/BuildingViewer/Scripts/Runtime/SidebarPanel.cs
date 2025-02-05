using System;
using Cysharp.Threading.Tasks;
using PrimeTween;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

public class SidebarPanel : MonoBehaviour
{
    [SerializeField] private Button _openPanelButton;
    [SerializeField] private Button _closePanelButton;
    
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = (RectTransform)transform;
    }

    private void Start()
    {
        _openPanelButton.onClick.AddListener(OpenPanel);
        _closePanelButton.onClick.AddListener(ClosePanel);
    }

    [Button]
    public void OpenPanel()
    {
        _openPanelButton.gameObject.SetActive(false);
        Tween.PositionX(_rectTransform, 0, 0.33f);
    }
    
    [Button]
    public void ClosePanel()
    {
        var panelWidth = _rectTransform.sizeDelta.x;
        var targetX = _rectTransform.position.x - panelWidth - 50;
        Tween.UIAnchoredPositionX(_rectTransform, targetX, 0.33f).OnComplete(
            () =>
            {
                _openPanelButton.gameObject.SetActive(true);
            });
    }
}
