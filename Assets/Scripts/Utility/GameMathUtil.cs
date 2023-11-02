using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameMathUtil {

    /// <summary>
    /// Apply weight and order to a list of IWeightOrderData object
    /// </summary>
    /// <param name="listOfWeightOrderData">List of IWeightOrderData to apply the win order and there weight</param>
    /// <param name="minWeight">Min selection weight for the IWeightOrderData object</param>
    /// <param name="maxWeight">Max Selection Weight for the IWeightOrderData object</param>
    public static void ApplyWeightWinOrder(List<IWeightOrderData> listOfWeightOrderData, int minWeight ,int maxWeight)
    {
        // get a temp copy of the list
        List<IWeightOrderData> tempList = new List<IWeightOrderData>(listOfWeightOrderData);
        int totalWeight = 0;
        // give the list of items random weights
        for (int i = 0; i < tempList.Count; i++)
        {
            // get a random weight
            int weight = Random.Range(minWeight, maxWeight);
            // keep track of all the weights
            totalWeight += weight;
            // give the item the random weight
            tempList[i].Weight = weight;
        }

        // keep track of the order for each item
        int order = 0;
        while (tempList.Count != 0)
        {
            // get a random weight from the total weight left over
            int weight = Random.Range(minWeight, totalWeight);

            // track the weight with what ever items are left
            int trackOfWeights = 0;
            for(int i = 0;i < tempList.Count;i++)
            {
                // add the weight
                trackOfWeights += tempList[i].Weight;

                // check  to see if this is the item
                if(weight <= trackOfWeights)
                {
                    // this is the item so give it the order
                    tempList[i].Order = order;
                    // remove the item weight from the total weight for next item
                    totalWeight -= tempList[i].Weight;
                    // since we gave this item an order remove it from the list
                    tempList.RemoveAt(i);
                    break;
                }
            }
            
            // go to the next order
            order++;
        }
    }
}
