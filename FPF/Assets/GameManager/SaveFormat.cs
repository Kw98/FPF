[System.Serializable]
public class SaveFormat
{
    public string worldName;
    public string difficulty;
    public SaveFormat_Time time = new SaveFormat_Time();
    public SaveFormat_Weather weather = new SaveFormat_Weather();
    public SaveFormat_Player player = new SaveFormat_Player();
    public string farmDictionaryLocation;
    public string dpSDictionaryLocation;
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
    public SaveFormat_Disaster disaster = new SaveFormat_Disaster();
}

[System.Serializable]
public class SaveFormat_Disaster
{
    public float timerLeft = 0f;
    public int id = 0;
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
        while (actualExp >= totalExpNextLvl && actualLvl != maxLvl)
        {
            actualLvl += 1;
            totalExpNextLvl *= 2;
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

[System.Serializable]
public class SaveFormat_DirtPile
{
    public float posX = 0f;
    public float posY = 0f;
    public float posZ = 0f;
    public int vegetebalId = 0;
    public SaveFormat_Seed seed = new SaveFormat_Seed();
}

[System.Serializable]
public class SaveFormat_Seed
{
    public float timer = 0;
    public float totalTimer = 0;
    public int actualHumidity = 0;
    public int humidityLimit = 0;
    public int degreeMini = 0;
    public int degreeMax = 0;
    public int windLimit = 0;
    public int resistance = 0;
}

[System.Serializable]
public class SaveFormat_Animal
{
    public float posX = 0;
    public float posY = 0;
    public float posZ = 0;
    public float growthTime = 1200;
    public float actualGrowthTime = 0;
    public float RPR = 1200;
    public float actualRPT= 0;
    public bool adult = false;
    public bool ressource = false;
    public int food = 50;
    public int id = -1;
}