using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(IthemMoovement))]
public class Ithem : MonoBehaviour
{
    private float _chanceForDrop;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = new Color(GetRandomFloat(), GetRandomFloat(), GetRandomFloat());
    }

    private float GetRandomFloat()
    {
        return Random.Range(0f, 1f);
    }
}
