using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;


public enum BuildingType
{
   ResidentialZone,
   CommercailZone,
   IndustrialZone,
   RoadType,
   StadiumType
   /*...*/
}


public class CityManagment : MonoBehaviour
{
    public MoneyManager moneyManager;
    public List<Person> citizens;
    Dictionary<Vector3Int, Building> buildings;
    public int spendableMoney;

    public int remainingPlacesInCity;
    public int remainingWorkPlacesInCity;
    private int insideTick;


    //type = 0 -> residential
    //type = 1 -> commercial
    //type = 2 -> industrial
    //p => egyedi position kulcsként hasznalhato törlésnél;
    public bool placeBuildingInCity(Vector3Int p, BuildingType type)
    {
        int cost = calculateCost(type);
        if(cost <= spendableMoney)
        {
            spendableMoney = spendableMoney - cost;
        }
        else
        {
            Debug.Log("not enough money for " + type + ": $$ " + cost);
            return false;
        }

        Building newBuilding = new Building(type);
        buildings.Add(p,newBuilding);
        return true;
        //DebugLogBuildings();
    }

    private int calculateCost(BuildingType type)
    {
        switch (type)
        {
            case BuildingType.ResidentialZone: return 100;
            case BuildingType.CommercailZone: return 150;
            case BuildingType.IndustrialZone: return 200;
            case BuildingType.StadiumType: return 300;
            default: return 0;
               
        }
    }

    internal void DebugLogBuildings()
    {
        foreach (var item in buildings)
        {
            Debug.Log(item.Key + " " + item.Value.type + " " + item.Value.maxResidents);
        }
        
    }



    // Start is called before the first frame update
    void Start()
    {
        citizens = new List<Person>();
        buildings = new Dictionary<Vector3Int, Building>();
        spendableMoney = moneyManager.starterFunds;
        remainingPlacesInCity = 0;
        remainingWorkPlacesInCity = 0;
        insideTick = 0;
        TimeTickSystem.OnTick += TimeTickSystem_OnClick;
    }

    private void Update()
    {
    }

    private void TimeTickSystem_OnClick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        insideTick += 1;
        if(insideTick % 5 == 0)
        {
            remainingPlacesInCity = 0;
            remainingWorkPlacesInCity = 0;

            foreach (var item in buildings)
            {
                if (item.Value.type == BuildingType.ResidentialZone)
                {
                    remainingPlacesInCity += item.Value.maxResidents - item.Value.currentResidents.Count;
                }
                else if (item.Value.type == BuildingType.IndustrialZone || item.Value.type == BuildingType.CommercailZone)
                {
                    remainingWorkPlacesInCity += item.Value.maxResidents - item.Value.currentResidents.Count;
                }
            }

            if(remainingPlacesInCity>0)
                CitizensMoveInToCity();
            if(remainingWorkPlacesInCity>0)
                CitizensFindWork();
        }
        if(insideTick % 10 == 0)
        {
            spendableMoney = spendableMoney + moneyManager.collectMoney(citizens.Count);
        }
        calculateHappiness();
    }

    private void calculateHappiness()
    {
        //throw new NotImplementedException();
    }

    private void CitizensMoveInToCity()
    {
        int numberOfMovingCitizens = 0;
        if (remainingPlacesInCity > 20)
        {
            System.Random rand = new System.Random();
            numberOfMovingCitizens = rand.Next(1, 20);
        }
        else numberOfMovingCitizens = remainingPlacesInCity;


        for(int i = 0; i < numberOfMovingCitizens; i++)
        {
            Person p = new Person();
            foreach (var item in buildings)
            {
                if (item.Value.currentResidents.Count != item.Value.maxResidents && item.Value.type == BuildingType.ResidentialZone) {
                    item.Value.currentResidents.Add(p);
                    p.Home = item.Value;
                    break;
                }
            }
            citizens.Add(p);
            Debug.Log("moved in " + p);
        }
       

    }

    private void CitizensFindWork()
    {
        //throw new NotImplementedException();
    }

    internal void RemoveBuilding(Vector3Int p)
    {
        buildings.Remove(p);
    }
}
