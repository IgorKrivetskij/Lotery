using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulet : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;

    private float _timeBeforeGameStart;
    private bool _isRouletRotate;

    private void Start()
    {
        _objectPool.InitPool();
        _timeBeforeGameStart = 2f;
        _isRouletRotate = false;
    }

    private void TakeSomething()
    {
        if (!_isRouletRotate)
        {
            StartCoroutine(RotateRoulet());
        }
        else
        {
            Debug.Log("Game Already started, please wait!");
        }      
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            TakeSomething();
        }
    }

    IEnumerator RotateRoulet()
    {
        _isRouletRotate = true;
        yield return new WaitForSeconds(_timeBeforeGameStart);
        for (int i = 0; i < 6; i++)
        {
            IthemMoovement it = _objectPool.GetIthemMoovement();
            it.SetParametrsForMoove( i);
            it.EndIthemMoove += OnEndIthemMoove;
        }
    }

    private void OnEndIthemMoove()
    {
        IthemMoovement it = _objectPool.GetIthemMoovement();
        it.SetParametrsForMoove( 0);
        it.EndIthemMoove -= OnEndIthemMoove;
        it.EndIthemMoove += OnEndIthemMoove;
    }
}
