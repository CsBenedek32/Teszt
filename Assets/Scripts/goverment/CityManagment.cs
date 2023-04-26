using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManagment : MonoBehaviour
{
    public MoneyManager moneyManager;
    public List<Person> citizens;
    public Dictionary<int, Building> buildings;
    public int spendableMoney;
    int asd =0;


    //type = 0 -> residential
    //type = 1 -> commercial
    //type = 2 -> industrial
    //p => egyedi position kulcsként hasznalhato törlésnél;
    internal void placeZoneType(int p, int type)
    {
      Building newBuilding = new Building(type);
      
    }




    // Start is called before the first frame update
    void Start()
    {
        citizens = new List<Person>();
        buildings = new Dictionary<int, Building>();
        spendableMoney = moneyManager.starterFunds;
    }



  
}
