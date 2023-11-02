using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanesText : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> listOfText;

    /// <summary>
    /// Turn on The Text
    /// </summary>
    /// <param name="visible">will change visibility of lanes all the text</param>
    /// <param name="laneNumber">make a lane visible if its set to -1 it will turn all lanes on</param>
    public void SetLanesVisibility(bool visible, int laneNumber = -1)
    {

        if (laneNumber == -1)
        {
            for (int i = 0; i < listOfText.Count; i++)
            {
                listOfText[i].gameObject.SetActive(visible);
            }
        }
        else
        {
            listOfText[laneNumber].gameObject.SetActive(visible);
        }
    }


    public void UpdateLanesText(RaceableObjectManager raceableObjectManager)
    {
        List<RaceableObject> listOfRaceableObject = raceableObjectManager.GetListOfRaceableObject();

        for (int i = 0; i < listOfRaceableObject.Count; i++)
        {
            listOfText[i].text = (listOfRaceableObject[i].Order + 1).ToString();
        }

    }

}
