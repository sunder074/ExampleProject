using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [SerializeField] GameObject _obj;
    GameObject _currentObject;

    private void Start()
    {
        SpawnObject();
    }

    private void Update()
    {
        if(_currentObject == null)
        {
            SpawnObject();
        }
    }

    public void SpawnObject()
    {
        _currentObject = Instantiate(_obj);
    }
}
