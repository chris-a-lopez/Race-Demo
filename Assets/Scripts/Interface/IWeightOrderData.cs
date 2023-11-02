using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An interface that will identify the weight of the item that is used for selction 
/// and the order in which it got selected from a list
/// </summary>
public interface IWeightOrderData
{
    /// <summary>
    /// Weight given to this data
    /// </summary>
    public int Weight {  get; set; }

    /// <summary>
    /// Order of this selection
    /// </summary>
    public int Order { get; set; }

}
