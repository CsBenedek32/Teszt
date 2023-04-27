using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int starterFunds;
    public int tax;

    public int collectMoney(int count)
    {
       return count * tax;
    }

    // Start is called before the first frame update
    void Start()
    {
        tax = 12;
        starterFunds = 5000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
