using System.Collections.Generic;

/// <summary>
/// Data we want to be saved
/// </summary>
public class GameSaveData
{
    public GameStatesType GameState;
    public RaceableObjectSaveData[] ListOfRaceableObjectSaveData;

    public void UpdateSaveData(GameStatesType gameState, RaceableObjectManager raceableObjectManager)
    {
        GameState = gameState;
        List<RaceableObject> listOfRaceableObject = raceableObjectManager.GetListOfRaceableObject();
        if (ListOfRaceableObjectSaveData == null || ListOfRaceableObjectSaveData.Length != listOfRaceableObject.Count)
        {
            ListOfRaceableObjectSaveData = new RaceableObjectSaveData[listOfRaceableObject.Count];
        }

        for (int i = 0; i < listOfRaceableObject.Count; i++)
        {
            if (ListOfRaceableObjectSaveData[i] == null)
            {
                ListOfRaceableObjectSaveData[i] = new RaceableObjectSaveData();
            }
            ListOfRaceableObjectSaveData[i].UpdateSaveData(listOfRaceableObject[i]);
        }
    }

    public void CopyRaceableObjectDataTo(List<RaceableObject> listOfRaceableObject)
    {
        for (int i = 0; i < listOfRaceableObject.Count; i++)
        {
            ListOfRaceableObjectSaveData[i].CopyDataTo(listOfRaceableObject[i]);
        }
    }

}
