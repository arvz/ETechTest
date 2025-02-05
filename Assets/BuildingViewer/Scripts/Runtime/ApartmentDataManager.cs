using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EnvizTest.Core.Messaging;
using UnityEngine;

public class ApartmentDataManager : MonoBehaviour
{
    private List<ApartmentData> _apartmentData;
    private IApartmentDataProvider _apartmentDataProvider;
    
    private void Awake()
    {
        _apartmentDataProvider = new SupabaseProvider();
    }

    private void Start()
    {
        FetchData().Forget();
    }

    private async UniTaskVoid FetchData()
    {
        _apartmentData = await _apartmentDataProvider.GetApartmentsAsync().AttachExternalCancellation(destroyCancellationToken);
        
        Messaging.SendMessage(MessageType.ApartmentDataLoaded, _apartmentData);
    }
}
