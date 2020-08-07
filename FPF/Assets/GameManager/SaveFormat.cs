[System.Serializable]
public class SaveFormat
{
    public string worldName;
    public string difficulty;
    public SaveFormat_Time time = new SaveFormat_Time();
    public SaveFormat_Weather weather = new SaveFormat_Weather();
    public SaveFormat_Player player = new SaveFormat_Player();
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
public class SaveFormat_Weather
{
    public bool raining = false;
    public bool snowing = false;
    public int humidity = 40;
    public int wind = 0;
    public int degree = 20;
}

[System.Serializable]
public class SaveFormat_Player
{
    public SaveFormat_SpeTree specialisation = new SaveFormat_SpeTree();
    public int playerSkin; // 7 skin different
    public float posX;
    public float posY;
    public float posZ;
}

[System.Serializable]
public class SaveFormat_Spe
{
    public int actualExp = 0;
    public int actualLvl = 1;
    public int totalExpNextLvl = 100;
    public int maxLvl = 100;

    public void GainExp(int amount)
    {
        actualExp += amount;
        if (actualExp >= amount && actualLvl != maxLvl)
        {
            actualLvl += 1;
            totalExpNextLvl += totalExpNextLvl;
        }

    }
}

[System.Serializable]
public class SaveFormat_SpeTree
{
    public SaveFormat_Spe general = new SaveFormat_Spe();
    public SaveFormat_Spe tomato = new SaveFormat_Spe();
    public SaveFormat_Spe carrot = new SaveFormat_Spe();
    public SaveFormat_Spe corn = new SaveFormat_Spe();
    public SaveFormat_Spe eggplant = new SaveFormat_Spe();
    public SaveFormat_Spe pumpkin = new SaveFormat_Spe();
    public SaveFormat_Spe turnip = new SaveFormat_Spe();
    public SaveFormat_Spe wood = new SaveFormat_Spe();
    public SaveFormat_Spe stone = new SaveFormat_Spe();
    public SaveFormat_Spe chicken = new SaveFormat_Spe();
    public SaveFormat_Spe pig = new SaveFormat_Spe();
    public SaveFormat_Spe cow = new SaveFormat_Spe();
}