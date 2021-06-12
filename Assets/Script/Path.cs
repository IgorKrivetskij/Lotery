using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private MoovePoint[] _pointsForMoove;

    public int MoovePointsCount => _pointsForMoove.Length;

    public MoovePoint GetPathPoint(int pointIndex)
    {
        return _pointsForMoove[pointIndex];
    }
}
