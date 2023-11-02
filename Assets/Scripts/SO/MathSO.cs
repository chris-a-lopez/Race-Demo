using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MathSO", menuName = "Math/Generate Math SO")]
public class MathSO : ScriptableObject
{
    public int MinWeight { get { return minWeight; } }
    public int MaxWeight { get { return maxWeight; } }
    public int MinDuration { get { return minDuration; } }
    public int MaxDuration { get { return maxDuration; } }

    [SerializeField]protected int minWeight;
    [SerializeField] protected int maxWeight;
    [SerializeField] protected int minDuration;
    [SerializeField] protected int maxDuration;


}
