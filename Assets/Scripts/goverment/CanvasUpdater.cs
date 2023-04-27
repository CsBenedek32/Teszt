using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUpdater : MonoBehaviour
{
    public CityManagment cityManagment;
    [SerializeField] TextMeshProUGUI money;
    [SerializeField] TextMeshProUGUI population;

    // Update is called once per frame
    void Update()
    {
        money.text = "$$: "+cityManagment.spendableMoney.ToString();
        population.text = cityManagment.citizens.Count.ToString();
    }
}
