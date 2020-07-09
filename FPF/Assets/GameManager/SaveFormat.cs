[System.Serializable]
public class SaveFormat
{
    public string   worldName;
    public string   difficulty;
    public SaveFormat_Time    time;
}

[System.Serializable]
public class SaveFormat_Time
{
    public float    timePlayed; // total timeplayed no use in game
    public float    timeInGame; // reset toutes les 24h ingame
    public int  dayInWeek; // monday, tuesday, wednesday, thursday, friday, saturday, sunday
    public int  dayInSaison;
    public int  saison; // 4 saisons
}

[System.Serializable]
public class SaveFormat_Player
{
}