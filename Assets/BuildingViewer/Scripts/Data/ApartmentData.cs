using UnityEngine;

[System.Serializable]
public class ApartmentData
{
    public int id;
    public int apartmentNumber;
    public Vector3 position;
    public int bed;
    public int bath;
    public int parking;

    public override string ToString()
    {
        string classString = $"{id}: Apartment {apartmentNumber}, {position}, Bed:{bed}, Bath:{bath}, Parking:{parking}";
        return classString;
    }
}