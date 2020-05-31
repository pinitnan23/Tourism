using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TourismManager : MonoBehaviour
{
    public Transform resultParent;
    public Transform contentParent;

    public GameObject locationButton;
    public GameObject emptyLocation;
    public GameObject searchUI;

    public LocationUI locationUI;

    public Text searchTextField;
    public float height = 110;

    private List<Location> locations;
    private List<GameObject> locationButtons;
    private LocationList locationList;
    private List<LocationProvince> locationProvinces;

    // Start is called before the first frame update
    void Start()
    {
        emptyLocation.SetActive(false);
        searchUI.SetActive(true);

        locations = new List<Location>();
        locationButtons = new List<GameObject>();
        //LoadJson();
        GetJsonData();

        float width = contentParent.GetComponent<RectTransform>().rect.width;
        Vector2 newSize = new Vector2(width, height);
        contentParent.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }

    private void GetJsonData()
    {
        locationList = new LocationList();
        /*
        var assembly = Assembly.GetExecutingAssembly();
        string[] names = assembly.GetManifestResourceNames();
        foreach (var name in names) Debug.Log(name);
        */
        // Solution1
        /*
        string jsonFileName = "JsonData.json";
        LocationList ObjContactList = new LocationList();
        var assembly = Assembly.GetExecutingAssembly();
        Debug.Log($"{assembly.GetName().Name}.Data.{jsonFileName}");
        using(Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Data.{jsonFileName}"))
        using (var reader = new StreamReader(stream))
        {
            var jsonString = reader.ReadToEnd();
            Debug.Log(jsonString);
            //Converting JSON Array Objects into generic list    
            locationList = JsonConvert.DeserializeObject<LocationList>(jsonString);
            Debug.Log(locationList.data.Count);
        }
        */
        // Solution2
        
        using (StreamReader r = new StreamReader("./Data/JsonData.json"))
        {
            string json = r.ReadToEnd();
            locationList = JsonConvert.DeserializeObject<LocationList>(json);
        }
        
        // Solution3
        /*
        TextAsset file = Resources.Load("JsonData") as TextAsset;
        string json = file.ToString();
        locationList = JsonConvert.DeserializeObject<LocationList>(json);
        */
        
        locationProvinces = new List<LocationProvince>();
        for (int i = 0; i < locationList.data.Count; i++)
        {
            LocationProvince newLocationProvince = new LocationProvince();
            newLocationProvince.location = locationList.data[i];
            
            // Set province name
            string[] province = new string[2];
            province = Page_Load(float.Parse(locationList.data[i].latitude), float.Parse(locationList.data[i].longtitude));
            newLocationProvince.provinceNameThai = province[0];
            newLocationProvince.provinceNameEng = province[1];
            
            locationProvinces.Add(newLocationProvince);
        }
        Debug.Log(locationProvinces.Count);
    }

    public void SearchLocation()
    {
        string keyword = searchTextField.text.Trim();
        List<LocationProvince> resultLocations = new List<LocationProvince>();
        if (keyword != "")
        {
            for (int i = 0; i < locationProvinces.Count; i++)
            {
                if (locationProvinces[i].location.placeNameThai.Contains(keyword) || locationProvinces[i].location.placeNameEng.Contains(keyword) || locationProvinces[i].provinceNameThai.Contains(keyword) || locationProvinces[i].provinceNameEng.Contains(keyword))
                {
                    resultLocations.Add(locationProvinces[i]);
                }
            }
        }

        for (int i = 0; i < locationButtons.Count; i++)
        {
            Destroy(locationButtons[i]);
        }

        if (resultLocations.Count > 0)
        {
            emptyLocation.SetActive(false);
            for (int i = 0; i < resultLocations.Count; i++)
            {
                GameObject newLocationButton = Instantiate(locationButton, contentParent);
                LocationButton locationButtonScript = newLocationButton.GetComponent<LocationButton>();
                locationButtonScript.SetLocation(resultLocations[i]);
                locationButtons.Add(newLocationButton);
            }
        }
        else
        {
            emptyLocation.SetActive(true);
        }
    }

    public void ShowAllLocation()
    {
        List<Location> resultLocations = new List<Location>(locations);
        emptyLocation.SetActive(false);

        for (int i = 0; i < locationButtons.Count; i++)
        {
            Destroy(locationButtons[i]);
        }
        locationButtons.Clear();

        for (int i = 0; i < locationProvinces.Count; i++)
        {
            GameObject newLocationButton = Instantiate(locationButton, contentParent);
            LocationButton locationButtonScript = newLocationButton.GetComponent<LocationButton>();
            locationButtonScript.SetLocation(locationProvinces[i]);
            locationButtons.Add(newLocationButton);
        }

    }

    public void ShowLocationDetail(Location location)
    {
        searchUI.SetActive(false);
        locationUI.ShowLocation(location);
    }

    private string HttpGetReqeust(string url, Dictionary<string, object> param)
    {
        WebRequest request = null;
        WebResponse response = null;
        StreamReader reader = null;
        Stream dataStream = null;
        string responseFromServer = null;
        try
        {
            StringBuilder sbAbsUrl = new StringBuilder();
            StringBuilder sbParam = new StringBuilder();
            string paramFormat = "{0}{1}={2}";
            string paramConnector = string.Empty;
            if (param != null && param.Count > 0)
            {
                foreach (KeyValuePair<string, object> eachParam in param)
                {
                    sbParam.AppendFormat(paramFormat, paramConnector, eachParam.Key, eachParam.Value);
                    paramConnector = "&";
                }
            }

            sbAbsUrl.Append(url);

            if (!string.IsNullOrEmpty(sbParam.ToString()))
            {
                sbAbsUrl.Append("?");
                sbAbsUrl.Append(sbParam.ToString());
            }

            request = WebRequest.Create(sbAbsUrl.ToString());
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            response = request.GetResponse();
            dataStream = response.GetResponseStream();
            reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();
        }
        catch (Exception ex)
        {
            if (reader != null)
            {
                reader.Close();
            }
            if (dataStream != null)
            {
                dataStream.Close();
            }
            if (response != null)
            {
                response.Close();
            }
            throw ex;
        }
        return responseFromServer;
    }

    protected string[] Page_Load(float lat, float lon)
    {
        string[] province = new string[2];

        string searchResult = string.Empty;
        string nostraServiceURL = "http://api.nostramap.com/NostraStandardServices/identify/identifyTH";
        if (!string.IsNullOrEmpty(nostraServiceURL))
        {
            Dictionary<string, object> dicReqParams = new Dictionary<string, object>();
            dicReqParams.Add("key", "GIwg97bQ)jmsgEUgg0XFpao2kmxRXx5v7YlH4xBJb99GqpjSSQdx8Vfp)CyceogTz7S7(QJy4Ki5jLacKjnAFZG=====2");
            dicReqParams.Add("lat", lat);
            dicReqParams.Add("lon", lon);
            searchResult = HttpGetReqeust(nostraServiceURL, dicReqParams);
            object searchojbect = JsonConvert.DeserializeObject(searchResult);

            JObject jsonObject = JObject.Parse(searchojbect.ToString());
            
            province[0] = jsonObject["Result"][0]["AdminLevel1_L"].ToString();
            province[1] = jsonObject["Result"][0]["AdminLevel1_E"].ToString();
        }
        return province;
    }
}

public class LocationList
{
    public int message { get; set; }
    public IList<Location> data { get; set; }
}

public class LocationProvince
{
    public Location location { get; set; }
    public string provinceNameThai { get; set; }
    public string provinceNameEng { get; set; }
}