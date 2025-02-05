using System;
using EnvizTest.Core.Messaging;
using TV.Camera;
using Unity.Cinemachine;
using UnityEngine;

public class ApartmentWorldObject : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _cmCamera;
    
    private ApartmentData _apartmentData;
    
    private void Awake()
    {
        Messaging.AddListener<ApartmentData>(MessageType.ApartmentFocusChanged, OnApartmentFocusChanged);
    }

    private void OnDestroy()
    {
        Messaging.RemoveListener<ApartmentData>(MessageType.ApartmentFocusChanged, OnApartmentFocusChanged);
    }

    public void Initialize(ApartmentData apartmentData, Vector3 buildingPosition)
    {
        _apartmentData = apartmentData;
        var buildingPositionAtOurY = new Vector3(buildingPosition.x, transform.position.y, buildingPosition.z);
        transform.forward = buildingPositionAtOurY - transform.position;
    }
    
    private void OnApartmentFocusChanged(ApartmentData data)
    {
        if (_apartmentData == data)
        {
            _cmCamera.gameObject.SetActive(true);
        }
        else
        {
            _cmCamera.gameObject.SetActive(false);
        }
    }
}
