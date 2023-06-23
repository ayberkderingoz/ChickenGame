using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoldierPositionManager : MonoBehaviour
{

    private static SoldierPositionManager _instance;
    public static SoldierPositionManager Instance => _instance;

    private int _currentIndex = -1;
    private List<GameObject> _areas;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        _areas = GameObject.FindGameObjectsWithTag("SoldierPosition").ToList();
        
    }
    public Vector3 GetPosition()
    {
        _currentIndex += 1;
        return _areas[_currentIndex].transform.position;
    }
    
}
