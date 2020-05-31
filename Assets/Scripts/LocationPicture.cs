public class LocationPicture
{
    public int id;
    public int refer_location_id;
    public string refer_location_table;
    public string type;
    public string quality;
    public string picture;
    public string createDate;
    public string updateDate;

    public LocationPicture(int id, int refer_location_id, string refer_location_table, string type, string quality, string picture, string createDate, string updateDate)
    {
        this.id = id;
        this.refer_location_id = refer_location_id;
        this.refer_location_table = refer_location_table;
        this.type = type;
        this.quality = quality;
        this.picture = picture;
        this.createDate = createDate;
        this.updateDate = updateDate;
    }
}