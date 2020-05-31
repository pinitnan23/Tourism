using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationUI : MonoBehaviour
{
    public GameObject locationUI;
    public Text locationNameText;
    public Text locationDetailText;

    public void ShowLocation(Location location)
    {
        locationUI.SetActive(true);

        locationNameText.text = location.placeNameThai + " (" + location.placeNameEng + ")";
        locationDetailText.text = location.longDescription;
    }
}
