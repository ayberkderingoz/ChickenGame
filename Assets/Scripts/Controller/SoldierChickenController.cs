using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoldierChickenController : MonoBehaviour
{
    public List<GameObject> soldierChickens = new List<GameObject>();
    
    
    private static SoldierChickenController _instance;
    public static SoldierChickenController Instance => _instance;
    
    public Action<List<GameObject>> OnSoldierChickenChanged;
    
        
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            _instance = this;
        }
    }



    public void AddSoldier(GameObject soldier)
    {
        soldierChickens.Add(soldier);
        OnSoldierChickenChanged?.Invoke(soldierChickens);
    }

    public void RemoveSoldier(GameObject soldier)
    {
        soldierChickens.Remove(soldier);
        OnSoldierChickenChanged?.Invoke(soldierChickens);
    }
    public GameObject GetClosestSoldier(Vector3 pos)
    {
        var closestDistance = Vector3.Distance(soldierChickens[0].transform.position,pos);
        var closestSoldier = soldierChickens[0];

        foreach (var soldier in soldierChickens)
        {
            if (Vector3.Distance(soldier.transform.position, pos)<closestDistance)
            {
                closestDistance = Vector3.Distance(soldier.transform.position, pos);
                closestSoldier = soldier;

            }
        }
        return closestSoldier;
    }
    
}
