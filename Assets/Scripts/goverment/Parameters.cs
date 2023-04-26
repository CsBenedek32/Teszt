using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    public int[] maxResidentsForWork = {10,30,50};
    public int[] maxResidentsForLiving = { 5, 20, 40 };
    
    public int upKeepCostForRoad = 2;
    public int upKeepCostForStadion = 100;
    public int upKeepCostForSchool = 150;

    public int[] upKeepCostForWorkPlace = { 10, 30, 50 };
    public int[] upKeepCostForLiving = { 0, 10, 20 };

}
