using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "building", menuName = "ScriptableObjects/Building")]
public class Building : ScriptableObject
{
    public string buildingName;
    public string description;
    public Sprite sprite;
    public Costs costs;
    public Usages usages;
    public int maxWorkersCount;
    public Productions productions;
    public Size size;
    public int buildTimeInGameMinutes;

    [System.Serializable]
    public struct Costs
    {
        public int planksCost;
        public int steelCost;
        public int chipsCost;
        public int ferratiumCost;
    }

    [System.Serializable]
    public struct Usages
    {
        public int rawFoodUsage;
        public int foodUsage;
        public int woodUsage;
        public int coalUsage;
        public int ironUsage;
        public int sandUssage;
        public int ferratytUsage;
        public int electricityUsage;
    }

    [System.Serializable]
    public struct Productions
    {
        public int rawFoodProduction;
        public int foodProduction;
        public int woodProduction;
        public int planksProduction;
        public int coalProduction;
        public int ironProduction;
        public int steelProduction;
        public int sandProduction;
        public int chipsProduction;
        public int ferratytProduction;
        public int ferratiumProduction;
        public int electricityProduction;
    }

    [System.Serializable]
    public struct Size
    {
        public int width;
        public int height;
    }
}
