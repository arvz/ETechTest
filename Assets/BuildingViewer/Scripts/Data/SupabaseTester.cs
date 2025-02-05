using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using JetBrains.Annotations;
using TriInspector;
using UnityEngine.Serialization;

public class SupabaseTester : MonoBehaviour
{
    [SerializeField, ReadOnly] private List<ApartmentData> _allApartmentData;

    [Button, UsedImplicitly]
    public async UniTask Fetch()
    {
        await FetchApartmentData();
    }
    
    private async UniTask FetchApartmentData()
    {
        SupabaseProvider supabaseProvider = new SupabaseProvider();
        _allApartmentData = await supabaseProvider.GetApartmentsAsync();

        foreach (ApartmentData apartmentData in _allApartmentData)
        {
            Debug.Log($"#Supabase# {apartmentData}");
            
        }
    }
}

