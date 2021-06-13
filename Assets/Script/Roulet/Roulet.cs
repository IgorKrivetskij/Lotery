using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Roulet : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private Path _path;
    [SerializeField] private TMP_Text _startGameText;
    [SerializeField] private TMP_Text _welcomeGameText;

    private float _timeBeforeGameStart;
    public static bool IsRouletRotate;
    private RouletUI _rouletUI;
    private Ithem _prizeIthem;

    private void Awake()
    {
        _rouletUI = GetComponent<RouletUI>();
    }

    private void Start()
    {
        _objectPool.InitPool();
        _timeBeforeGameStart = 5f;
        IsRouletRotate = false;
    }

    private void TakeSomething()
    {
        if (!IsRouletRotate)
        {
            RotateRoulet();
        }
        else
        {
            _prizeIthem.SetPrize();
            Debug.Log("Game Already started, please wait!");            
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _welcomeGameText.gameObject.SetActive(false);
            TakeSomething();            
        }
    }

    private void RotateRoulet()
    {
        IsRouletRotate = true;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        while (--_timeBeforeGameStart > 0)
        {
            _startGameText.text = "Game start into  " + _timeBeforeGameStart + " seconds";
            yield return new WaitForSeconds(1f);
        }
        _startGameText.gameObject.SetActive(false);
        CalculatePrize();
        for (int i = 0; i < _path.MoovePointsCount; i++)
        {
            IthemMoovement it = _objectPool.GetIthemMoovement();
            it.SetParametrsForMoove(i);
            it.EndIthemMoove -= OnEndIthemMoove;
            it.EndIthemMoove += OnEndIthemMoove;
        }
    }

    private void OnEndIthemMoove()
    {
        IthemMoovement it = _objectPool.GetIthemMoovement();
        it.SetParametrsForMoove(0);
        it.EndIthemMoove -= OnEndIthemMoove;
        it.EndIthemMoove += OnEndIthemMoove;
    }

    private void CalculatePrize()
    {
        Ithem[] ithemPrizes = {
            _objectPool.GetObjectFromPool(GetIndex()).GetComponent<Ithem>(),
             _objectPool.GetObjectFromPool(GetIndex()).GetComponent<Ithem>(),
              _objectPool.GetObjectFromPool(GetIndex()).GetComponent<Ithem>()
        };
        ithemPrizes = FindMinChance(ithemPrizes);
        ConvertProbabilityFromNumToPercent(ithemPrizes);
        _rouletUI.Init(ithemPrizes);
        _prizeIthem = ithemPrizes[Random.Range(0, 3)];        
    }

    private int GetRandomInt()
    {
        return Random.Range(_objectPool.GetIthemCount(), 5 * _objectPool.GetIthemCount());
    }

    private int GetIndex()
    {
        return GetRandomInt() % _objectPool.GetIthemCount();
    }

    private Ithem[] FindMinChance(Ithem[] ithems)
    {
        int countSub;
        for (int i = 0; i < ithems.Length - 1; i++)
        {
            countSub = 0;
            for (int j = 0; j < ithems.Length - i - 1; j++)
            {
                if (ithems[j].GetChanceForDrop() > ithems[j + 1].GetChanceForDrop())
                {
                    Ithem tmpIthem = ithems[j];
                    ithems[j] = ithems[j + 1];
                    ithems[j + 1] = tmpIthem;
                    countSub++;
                }
            }
            if (countSub == 0)
            {
                break;
            }
        }
        return ithems;
    }

    private void ConvertProbabilityFromNumToPercent(Ithem[] ithems)
    {
        float tmpSumChances = 0;
        for (int i = 0; i < ithems.Length; i++)
        {
            tmpSumChances += ithems[i].GetChanceForDrop();
        }
        for (int i = 0; i < ithems.Length; i++)
        {
            ithems[i].SetPercent((int)(ithems[i].GetChanceForDrop() * 100 / tmpSumChances));
        }
    }

}
