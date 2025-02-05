using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class SupabaseProvider : IApartmentDataProvider
{
    private const string SupabaseUrl = "https://lbrwitwkvngztscjzgtp.supabase.co";
    private const string APIKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxicndpdHdrdm5nenRzY2p6Z3RwIiwicm9sZSI6ImFub24iLCJpYXQiOjE3Mzg2NTc4NTksImV4cCI6MjA1NDIzMzg1OX0.cpLcpVDqkZ5OIsSDUsZ8ifCFCk8GpFav0HgFSCJwthY";

    public async UniTask<List<ApartmentData>> GetApartmentsAsync()
    {
        Debug.Log($"#Supabase# Getting Apartments Data from Supabase...");
        
        string endpoint = $"{SupabaseUrl}/rest/v1/ApartmentData?select=*";
        using var request = UnityWebRequest.Get(endpoint);
        request.SetRequestHeader("apikey", APIKey);
        request.SetRequestHeader("Authorization", "Bearer " + APIKey);
        await request.SendWebRequest().ToUniTask();

        if (request.result == UnityWebRequest.Result.Success)
        {
            List<ApartmentData> apartmentData = new();
            var json = request.downloadHandler.text;
            apartmentData = JsonConvert.DeserializeObject<List<ApartmentData>>(json);

            Debug.Log($"#Supabase# Successfully Retrieved {apartmentData.Count} Apartments Data from Supabase!");
            
            return apartmentData;
        }

        Debug.LogError(request.error);
        return null;
    }
}