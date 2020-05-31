using System.Collections.Generic;

public class Location
{
    public int id { get; set; }
    public string placeNameThai { get; set; }
    public string placeNameEng { get; set; }
    public string latitude { get; set; }
    public string longtitude { get; set; }
    public int point { get; set; }
    public string gameDescription { get; set; }
    public string shotDescription { get; set; }
    public string longDescription { get; set; }
    public string url { get; set; }
    public IList<SettingLocationSub> setting_location_sub { get; set; }
    public IList<SettingLocationPicture> setting_location_picture { get; set; }
    public string createDate { get; set; }
    public string updateDate { get; set; }
}