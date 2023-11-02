using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An interface applied to an object that can be used in a race
/// </summary>
public interface IRaceObject
{
    /// <summary>
    /// Start position of the object
    /// </summary>
    public Vector3 StartPosition { get; set; }

    /// <summary>
    /// End Position of the object
    /// </summary>
    public Vector3 EndPosition { get; set; }

    /// <summary>
    /// The duration of how long it will take the object to go from start to end
    /// </summary>
    public float Duration {  get; set; }

    /// <summary>
    /// Start the Race
    /// </summary>
    public void StartRace();

    /// <summary>
    /// Snap Object back to Start
    /// </summary>
    public void SnapToStart();

    /// <summary>
    /// Snap Object to the End
    /// </summary>
    public void SnapToEnd();
}
