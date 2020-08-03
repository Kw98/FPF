[System.Serializable]
public class SaveFormat
{
    public string worldName;
    public string difficulty;
    public SaveFormat_Time time;
    public SaveFormat_Season season;
}

[System.Serializable]
public class SaveFormat_Time
{
    public float timer = 480; // total timeplayed no use in game
    public float lightIntensity = 0.7f;
    public int  dayInSeason = 1;
    public int  season = 0; // 4 saisons
}

[System.Serializable]
public class SaveFormat_Season
{
    public bool raining = false;
    public bool snowing = false;
    public int degree = 20;
}

[System.Serializable]
public class SaveFormat_Player
{
    public int playerSkin; // 7 skin different
    public float posX;
    public float posY;
    public float posZ;
}