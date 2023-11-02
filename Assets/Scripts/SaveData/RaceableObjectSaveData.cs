using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data we want to be for a raceableobject
/// </summary>
public class RaceableObjectSaveData
{
    public string Name;
    public float Duration;
    public int Weight;
    public int Order;


    public void UpdateSaveData(RaceableObject raceableObject)
    {
        Name = raceableObject.Name;
        Duration = raceableObject.Duration;
        Weight = raceableObject.Weight;
        Order = raceableObject.Order;
    }

    public void CopyDataTo(RaceableObject raceableObject)
    {
        raceableObject.Name = Name;
        raceableObject.Duration = Duration;
        raceableObject.Weight = Weight;
        raceableObject.Order = Order;
    }
}
