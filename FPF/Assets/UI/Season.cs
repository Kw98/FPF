using UnityEngine;
using UnityEditor;
public class Season
{
    public int averageDegree;
    public int averageHumidity;
    public int rainChance;
    public int degreeNext;

    public Season(int avgd, int avgh, int rainChance, int degreeNext)
    {
        this.averageDegree = avgd;
        this.averageHumidity = avgh;
        this.rainChance = rainChance;
        this.degreeNext = degreeNext;
    }
}
