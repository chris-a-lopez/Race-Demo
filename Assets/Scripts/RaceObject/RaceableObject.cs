using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

/// <summary>
/// An object used for a race
/// </summary>
public class RaceableObject : MonoBehaviour,  IRaceObject, IWeightOrderData
{
    
    [SerializeField] TextMeshPro NameText;
    [SerializeField] Transform StartPositionTransform;
    [SerializeField] Transform EndPositionTransform;
    [SerializeField] RaceObjectAnimator RaceObjectAnimator;
    public Vector3 StartPosition { get { return StartPositionTransform.localPosition; } set { StartPositionTransform.position = new Vector3(value.x, value.y, value.z); } }
    public Vector3 EndPosition { get { return EndPositionTransform.localPosition; } set { EndPositionTransform.position = new Vector3(value.x, value.y, value.z); } }
    public string Name { get { return NameText.text; } set { NameText.text = value; } }
    public float Duration { get; set; }
    public int Weight { get; set; }
    public int Order { get; set; }

    /// <summary>
    /// Event when race object finish the race
    /// </summary>
    public event Action OnFinishRace;

    /// <summary>
    /// Start the race
    /// </summary>
    public void StartRace()
    {
        SnapToStart();

        Ease[] eases = new Ease[] { Ease.Linear, Ease.InSine, Ease.InQuad, Ease.InCubic, Ease.InCirc, Ease.InQuart, Ease.InQuint, Ease.InExpo };
        // Pick random ease for the path to create different variation of the race object moving
        Ease randomEase = eases[UnityEngine.Random.Range(0, eases.Length - 1)];
        transform.DOMoveX(EndPosition.x, Duration).SetEase(randomEase).OnComplete(FinishRace);
        RaceObjectAnimator.PlayRunAnimation();
    }

    /// <summary>
    /// Will Send an event when the race is finish
    /// </summary>
    private void FinishRace()
    {
        RaceObjectAnimator.StopRunAnimation();
        OnFinishRace?.Invoke();
    }


    /// <summary>
    /// Snap the race object to the start of the race
    /// </summary>
    public void SnapToStart()
    {
        transform.DOPause();
        // Only need the x position for this starting position
        transform.position = new Vector3(StartPosition.x, this.transform.position.y, this.transform.position.z);
    }

    /// <summary>
    /// Snap the race object to the end of the race
    /// </summary>
    public void SnapToEnd()
    {
        RaceObjectAnimator.StopRunAnimation();
        transform.DOPause();
        transform.position = new Vector3(EndPosition.x, this.transform.position.y, this.transform.position.z);
    }
}
