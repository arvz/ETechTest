using System;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using TriInspector;

public class SupabaseTester : MonoBehaviour
{
    const string supabaseUrl = "https://lbrwitwkvngztscjzgtp.supabase.co";
    const string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxicndpdHdrdm5nenRzY2p6Z3RwIiwicm9sZSI6ImFub24iLCJpYXQiOjE3Mzg2NTc4NTksImV4cCI6MjA1NDIzMzg1OX0.cpLcpVDqkZ5OIsSDUsZ8ifCFCk8GpFav0HgFSCJwthY";

    [SerializeField] ApartmentPanelController apartmentPanelController;
    [SerializeField] private List<ApartmentRecord> apartmentRecords;

    private async void Start()
    {
        await Fetch();
    }

    [Button]
    public async UniTask Fetch()
    {
        await FetchApartmentData();
    }
    
    private async UniTask FetchApartmentData()
    {
        string endpoint = $"{supabaseUrl}/rest/v1/ApartmentData?select=*";
        using var request = UnityWebRequest.Get(endpoint);
        request.SetRequestHeader("apikey", apiKey);
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        await request.SendWebRequest().ToUniTask();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            apartmentRecords.Clear();
            var json = request.downloadHandler.text;
            var data = JsonConvert.DeserializeObject<List<ApartmentRecord>>(json);

            apartmentRecords = data;
            
            foreach (var record in data)
            {
                Debug.Log("#arvz# Apartment: " + record.apartmentNumber);
            }
        }
        else
        {
            Debug.LogError(request.error);
        }
        
        apartmentPanelController.SetPanelText(apartmentRecords[0].apartmentNumber.ToString());
    }
}

[System.Serializable]
public class ApartmentRecord
{
    public int id;
    public int apartmentNumber;
    public Vector3 position;
    public int bed;
    public int bath;
    public int parking;

}