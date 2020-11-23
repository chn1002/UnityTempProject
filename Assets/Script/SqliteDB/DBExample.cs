using SQLite4Unity3d;

public class DBExample 
{
    [PrimaryKey, AutoIncrement]
    public int index { get; set; }
    public string name { get; set; }
    public string date { get; set; }
}
