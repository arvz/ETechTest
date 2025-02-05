using System.Collections.Generic;
using EnvizTest.Core.Messaging;
using UnityEngine;

public class ApartmentManager : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Canvas _canvas;

    [Header("Asset References")]
    [SerializeField] private GameObject _worldTransformPrefab;
    [SerializeField] private ApartmentPanelController _apartmentPanelPrefab;
    
    private void Awake()
    {
        Messaging.AddListener<List<ApartmentData>>(MessageType.ApartmentDataLoaded, OnApartmentDataLoaded);
    }

    private void OnApartmentDataLoaded(List<ApartmentData> allApartmentData)
    {
        foreach (var apartmentData in allApartmentData)
        {
            var worldTransformObject = Instantiate(_worldTransformPrefab, apartmentData.position, Quaternion.identity);
            worldTransformObject.transform.SetParent(transform);
            worldTransformObject.transform.name = $"{apartmentData.apartmentNumber} World Object";
            
            var apartmentPanel = Instantiate(_apartmentPanelPrefab, _canvas.transform);
            apartmentPanel.Initialize(worldTransformObject.transform);
        }
        
    }
}
