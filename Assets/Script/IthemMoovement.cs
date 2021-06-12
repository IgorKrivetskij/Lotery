﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IthemMoovement : MonoBehaviour
{
    
    private float _speed;
    private bool _isIthemMooving;
    private int _currentPointIndex;
    private Path _path;

    public event UnityAction EndIthemMoove;

    private void Awake()
    {
        _isIthemMooving = false;
        _speed = 0.1f;       
        GameObject tmp = GameObject.FindWithTag("Path");
        _path = tmp.GetComponent<Path>();
    }
    

    public void SetParametrsForMoove(int startPos)
    {
        gameObject.SetActive(true);
        _currentPointIndex = startPos;
        transform.position = GetPointPosition(startPos);
        if (startPos != _path.MoovePointsCount)
        {
            StartCoroutine(Moove(++_currentPointIndex));
        }
        else
        {
            EndPath();
        }
    }

    private Vector3 GetPointPosition(int pointIndex)
    {
        return _path.GetPathPoint(pointIndex).transform.position;
    }

    private void EndPath()
    {
        EndIthemMoove?.Invoke();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_isIthemMooving && _currentPointIndex < _path.MoovePointsCount)
        {
            StartCoroutine(Moove(++_currentPointIndex));
        }
        if (_currentPointIndex == _path.MoovePointsCount)
        {
            EndPath();
        }
    }

    IEnumerator Moove(int nextPointIndex)
    {
        _isIthemMooving = true;
        yield return null;
        bool isMooveEnd = false;
        float minDistanceToPoint = 0.1f;
        while (!isMooveEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, GetPointPosition(nextPointIndex),
               _speed * Time.deltaTime);
            float currentDistanceToPoint =
                (transform.position - GetPointPosition(nextPointIndex)).sqrMagnitude;
            if (minDistanceToPoint * minDistanceToPoint >= currentDistanceToPoint)
            {
                isMooveEnd = true;
                _currentPointIndex = nextPointIndex;
                _isIthemMooving = false;
            }
        }
    }
}
