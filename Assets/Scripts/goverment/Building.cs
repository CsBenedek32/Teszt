using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    //public Parameters parameters;

    int type;
    int maxResidents;
    int upKeepCost;
    int level;
    bool canUpgrade;
    List<Person> currentResidents;

    public Building(int type)
    {
        this.type = type;
        this.level = 0;
        canUpgrade = false;
        currentResidents = new List<Person>();

        if (type == 1 || type == 2) {
            maxResidents = 0;
            upKeepCost = 0;
        }
        else {
            maxResidents = 0;
            upKeepCost = 0;
        }
    }
}
