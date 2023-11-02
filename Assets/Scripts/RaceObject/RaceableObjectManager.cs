using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaceableObjectManager
{

    List<RaceableObject> listOfRaceableObject;

    /// <summary>
    /// Event that is sent when all the raceableobjects finish there race
    /// </summary>
    public event Action OnAllRaceableObjectFinishRace;

    /// <summary>
    /// Track when race object finish the race
    /// </summary>
    private int _raceableObjectFinishTracker = 0;

    public RaceableObjectManager() 
    {
        this.listOfRaceableObject = new List<RaceableObject>();
    }

    public RaceableObjectManager(List<RaceableObject> listOfRaceableObject)
    {
        this.listOfRaceableObject = new List<RaceableObject>(listOfRaceableObject);
    }

    /// <summary>
    /// Add a new raceable object
    /// </summary>
    /// <param name="raceableObject"></param>
    public void AddRaceableObject(RaceableObject raceableObject)
    {
        listOfRaceableObject.Add(raceableObject);
    }

    /// <summary>
    /// Remove all the raceable objects in the collection
    /// </summary>
    public void ClearAllRaceableObject()
    {
        listOfRaceableObject.Clear();
    }

    public List<RaceableObject> GetListOfRaceableObject()
    {
        return new List<RaceableObject>(listOfRaceableObject);
    }

    /// <summary>
    /// Initalize all the RaceableObjects
    /// </summary>
    /// <param name="minWeight"></param>
    /// <param name="maxWeight"></param>
    /// <param name="minDuration"></param>
    /// <param name="maxDuration"></param>
    public void Initalize(int minWeight, int maxWeight, int minDuration, int maxDuration)
    {
        // Set up the weight data for the raceable object
        List<IWeightOrderData> listOfWeightOrder = new List<IWeightOrderData>(this.listOfRaceableObject);
        GameMathUtil.ApplyWeightWinOrder(listOfWeightOrder, minWeight, maxWeight);

        // Set the duration for the raceable objects
        SetDuration(minDuration, maxDuration);
    }

    /// <summary>
    /// Will Set the raceable objects duration
    /// </summary>
    /// <param name="minDuration">This is the min amount a raceable object can finish in milliseconds</param>
    /// <param name="maxDuration">This is the max amount a raceable object can finish in milliseconds</param>
    private void SetDuration(int minDuration, int maxDuration)
    {
        int distance = maxDuration - minDuration;
        int sliceDuration = distance / this.listOfRaceableObject.Count;
        List<RaceableObject> tempList = new List<RaceableObject>(this.listOfRaceableObject);
        // sort the raceable objects
        tempList.Sort(delegate (RaceableObject a, RaceableObject b)
        {
            return a.Order > b.Order ? 1 : -1;
        });

        for (int i = 0; i < tempList.Count; i++)
        {
            int duration = UnityEngine.Random.Range(1, sliceDuration);
            minDuration += duration;
            // Convert the duration to seconds
            tempList[i].Duration = (float)minDuration / 1000f;
        }
    }

    /// <summary>
    /// Force all the raceable objects to the start
    /// </summary>
    public void ForceToStart()
    {
        _raceableObjectFinishTracker = 0;
        for (int i = 0; i < this.listOfRaceableObject.Count; i++)
        {
            this.listOfRaceableObject[i].SnapToStart();
        }
    }

    /// <summary>
    /// Force all the raceable objects to the end
    /// </summary>
    public void ForceToEnd()
    {
        for (int i = 0; i < this.listOfRaceableObject.Count; i++)
        {
            this.listOfRaceableObject[i].SnapToEnd();
        }
    }

    /// <summary>
    /// Start the Race for the raceable object
    /// </summary>
    public void StartRace()
    {
        _raceableObjectFinishTracker = 0;
        for (int i = 0; i < this.listOfRaceableObject.Count; i++)
        {
            this.listOfRaceableObject[i].StartRace();
            // Just remove it just in case this is called again
            this.listOfRaceableObject[i].OnFinishRace -= SingleRaceObjectFinishRace;
            this.listOfRaceableObject[i].OnFinishRace += SingleRaceObjectFinishRace;
        }
    }

    /// <summary>
    /// When a raceable object finsh a race this event will be sent
    /// </summary>
    private void SingleRaceObjectFinishRace()
    {
        _raceableObjectFinishTracker++;
        if(this.listOfRaceableObject.Count == _raceableObjectFinishTracker)
        {
            AllRaceObjectFinishRace();
        }
    }

    /// <summary>
    /// When all the raceable objects finish the race this event will be sent
    /// </summary>
    private void AllRaceObjectFinishRace()
    {
        OnAllRaceableObjectFinishRace?.Invoke();
    }
}
