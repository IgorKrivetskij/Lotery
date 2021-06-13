using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RouletUI : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _prizes;
    [SerializeField] private TMP_Text[] _gameStarted;
    [SerializeField] private GameObject _kursor;

    public void Init(Ithem[] ithemPrizes)
    {
        for (int i = 0; i < 3; i++)
        {
            _prizes[i].material.color = ithemPrizes[i].gameObject.GetComponent<MeshRenderer>().material.color;
            TMP_Text[] texts = _prizes[i].GetComponentsInChildren<TMP_Text>();
            texts[0].text = "Chance get this  :";
            texts[1].text = ithemPrizes[i].GetPercent().ToString() + " %";
            _prizes[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < _gameStarted.Length; i++)
        {
            _gameStarted[i].gameObject.SetActive(true);
        }
        _kursor.SetActive(true);
    }
}
