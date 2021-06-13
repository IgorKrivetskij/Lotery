using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _ithemPrefab;
    [SerializeField] private int _ithemCount;

    private List<GameObject> _ithemsInPool = new List<GameObject>();
    private List<GameObject> _usedIthems = new List<GameObject>();

    public void InitPool()
    {
        for (int i = 0; i < _ithemCount-1; i++)
        {
            GameObject ithem = Instantiate(_ithemPrefab, transform.position, Quaternion.identity);
            _ithemsInPool.Add(ithem);            
            ithem.SetActive(false);
        }
    }

    public IthemMoovement GetIthemMoovement()
    {
        if(_ithemsInPool.Count == 0)
        {
            _ithemsInPool = _usedIthems;
            return Checking();
        }
        else
        {
            return Checking();
        }        
    }

    private IthemMoovement Checking()
    {
        GameObject ithem = _ithemsInPool[0];
        if(_usedIthems.Count != _ithemCount)
        {
            _usedIthems.Add(ithem);
        }        
        _ithemsInPool.Remove(ithem);
        return ithem.GetComponent< IthemMoovement>();
    }

    public int GetIthemCount()
    {
        return _ithemCount;
    }

    public GameObject GetObjectFromPool(int index)
    {
        return _ithemsInPool[index];
    }
}
