using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationButton : MonoBehaviour
{
    public Text provinceText;
    public Text locationText;

    private LocationProvince locationProvince;
    
    public void SetLocation(LocationProvince newLocationProvince)
    {
        locationProvince = newLocationProvince;
        provinceText.text = "จังหวัด" + newLocationProvince.provinceNameThai;
        locationText.text = newLocationProvince.location.placeNameThai;
    }

    public void SelectLocation()
    {
        TourismManager tourismManager = FindObjectOfType<TourismManager>();
        tourismManager.ShowLocationDetail(locationProvince.location);
    }
}
