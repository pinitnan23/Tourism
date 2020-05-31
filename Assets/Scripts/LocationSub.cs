using System.Collections.Generic;

public class LocationSub
{
    public int id;
    public int parent_location_id;
    public string placeNameThai;
    public string placeNameEng;
    public string latitude;
    public string longtitude;
    public string sorting;
    public string gameDescription;
    public LocationPicture[] setting_location_picture;
    public string createDate;
    public string updateDate;

    public LocationSub(int id, int parent_location_id, string placeNameThai, string placeNameEng, string latitude, string longtitude, string sorting, string gameDescription, string createDate, string updateDate)
    {
        this.id = id;
        this.parent_location_id = parent_location_id;
        this.placeNameThai = placeNameThai;
        this.placeNameEng = placeNameEng;
        this.latitude = latitude;
        this.longtitude = longtitude;
        this.sorting = sorting;
        this.gameDescription = gameDescription;
        this.createDate = createDate;
        this.updateDate = updateDate;
    }

    public void SetSetting_location_picture(List<LocationPicture> locationsPicture)
    {
        setting_location_picture = new LocationPicture[locationsPicture.Count];
        for (int i = 0; i < locationsPicture.Count; i++)
        {
            setting_location_picture[i] = locationsPicture[i];
        }
    }
}
