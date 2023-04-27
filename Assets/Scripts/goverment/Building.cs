using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class Building
{
   

    public BuildingType type;
    public int maxResidents;
    public int upKeepCost;
    public int level;
    public bool canUpgrade;
    public List<Person> currentResidents;
    

    public Building(BuildingType type)
    {
       
        
        
        this.type = type;
        this.level = 0;
        canUpgrade = false;
        currentResidents = new List<Person>();

        if (type == BuildingType.CommercailZone || type == BuildingType.IndustrialZone) {
            maxResidents = Parameters.maxResidentsForWork[level];
            upKeepCost = Parameters.upKeepCostForWorkPlace[level];
        }
        else {
            maxResidents = Parameters.maxResidentsForLiving[level];
            upKeepCost = Parameters.upKeepCostForLiving[level];
        }

       
    }

}
