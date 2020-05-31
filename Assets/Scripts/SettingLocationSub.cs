using System.Collections.Generic;

public class SettingLocationSub
{
    public int id { get; set; }
    public int parent_location_id { get; set; }
    public string placeNameThai { get; set; }
    public string placeNameEng { get; set; }
    public string latitude { get; set; }
    public string longtitude { get; set; }
    public string sorting { get; set; }
    public string gameDescription { get; set; }
    public IList<object> setting_location_picture { get; set; }
    public string createDate { get; set; }
    public string updateDate { get; set; }
}
