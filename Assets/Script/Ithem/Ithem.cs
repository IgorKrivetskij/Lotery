using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(IthemMoovement))]
public class Ithem : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private float _chanceForDrop;
    private float _percentForDrop;
    [SerializeField] private bool _isPrize;
    
    private void Awake()
    {
        _isPrize = false;
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = new Color(GetRandomFloat(), GetRandomFloat(), GetRandomFloat());
        _chanceForDrop = GetRandomFloat();
    }

    private float GetRandomFloat()
    {
        return Random.Range(0f, 1f);
    }

    public float GetChanceForDrop()
    {
        return _chanceForDrop;
    }

    public void SetPercent(int percent)
    {
        if (percent > 0)
        {
            _percentForDrop = percent;
        }
    }

    public float GetPercent() => _percentForDrop;

    public void SetPrize()
    {
        _isPrize = true;
    }

    public bool IsPrize()
    {
        return _isPrize;
    }
}
