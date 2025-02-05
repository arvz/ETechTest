using System;
using System.Collections.Generic;
using EnvizTest.Core.Messaging;
using UnityEngine;

public class ApartmentManager : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Canvas _panelsCanvas;
    [SerializeField] private RectTransform _sidebarEntryParent;
    [SerializeField] private Transform _building;

    [Header("Asset References")]
    [SerializeField] private ApartmentWorldObject _apartmentWorldPrefab;
    [SerializeField] private ApartmentListEntry _apartmentListEntryPrefab;
    [SerializeField] private ApartmentPanelController _apartmentPanelPrefab;

    private Dictionary<ApartmentData, ApartmentPanelController> _apartmentPanels = new();
    private ApartmentPanelController _currentActiveApartment;

    private void Awake()
    {
        Messaging.AddListener<List<ApartmentData>>(MessageType.ApartmentDataLoaded, OnApartmentDataLoaded);
        Messaging.AddListener<ApartmentData>(MessageType.ApartmentEntryClicked, OnApartmentEntryClicked);
    }

    private void OnDestroy()
    {
        Messaging.RemoveListener<List<ApartmentData>>(MessageType.ApartmentDataLoaded, OnApartmentDataLoaded);
        Messaging.RemoveListener<ApartmentData>(MessageType.ApartmentEntryClicked, OnApartmentEntryClicked);
    }

    private void OnApartmentEntryClicked(ApartmentData data)
    {
        bool found = _apartmentPanels.TryGetValue(data, out var apartmentPanelController);
        if (found == false)
        {
            return;
        }

        if (_currentActiveApartment != null)
        {
            _currentActiveApartment.gameObject.SetActive(false);
        }

        _currentActiveApartment = apartmentPanelController;
        apartmentPanelController.gameObject.SetActive(true);
        Messaging.SendMessage(MessageType.ApartmentFocusChanged, data);
    }

    private void OnApartmentDataLoaded(List<ApartmentData> allApartmentData)
    {
        foreach (var apartmentData in allApartmentData)
        {
            var worldTransformObject = Instantiate(_apartmentWorldPrefab, apartmentData.position, Quaternion.identity);
            worldTransformObject.transform.SetParent(transform);
            worldTransformObject.Initialize(apartmentData, _building.position);
            worldTransformObject.transform.name = $"{apartmentData.apartmentNumber} World Object";

            var apartmentPanel = Instantiate(_apartmentPanelPrefab, _panelsCanvas.transform);
            apartmentPanel.Initialize(worldTransformObject.transform, apartmentData);
            _apartmentPanels.Add(apartmentData, apartmentPanel);

            var apartmentListEntry = Instantiate(_apartmentListEntryPrefab, _sidebarEntryParent);
            apartmentListEntry.Initialize(apartmentData);
        }
    }
}