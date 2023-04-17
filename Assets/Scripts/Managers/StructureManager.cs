using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class StructureManager : MonoBehaviour
{
    public StructurePrefabWeighted[] housesPrefabs, specialPrefabs, bigStructurePrefabs;
    public GameObject forest;
    public PlacementManager placementManager;

    private float[] houseWeights, specialWeights, bigStructureWeights;

    private void Start()
    {
        houseWeights = housesPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        specialWeights = specialPrefabs.Select(prefabStats => prefabStats.weight).ToArray() ;
        bigStructureWeights = bigStructurePrefabs.Select(prefabStats => prefabStats.weight).ToArray();
    }

    internal void PlaceWoods()
    {
        int x, y, z;
        Vector3Int position;
        System.Random r = new System.Random();
        y = 0;
        do
        {
            x = r.Next(0, 30);
            z = r.Next(0, 30);
            position = new Vector3Int(x, y, z);
        } while (!CheckPositionBeforePlacementForWoods(position));
        int randomIndex = 1;
        //placementManager.PlaceObjectOnTheMap(position, housesPrefabs[randomIndex].prefab, CellType.Structure);
        placementManager.PlaceObjectOnTheMap(position, forest, CellType.Structure);
    }

    public void PlaceHouse(Vector3Int p)
    {
        if (CheckPositionBeforePlacement(p))
        {
            int randomIndex = GetRandomWeightIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(p, housesPrefabs[randomIndex].prefab, CellType.Structure);
        }
    }

    public void PlaceSpecial(Vector3Int p)
    {
        if (CheckPositionBeforePlacement(p))
        {
            int randomIndex = GetRandomWeightIndex(specialWeights);
            placementManager.PlaceObjectOnTheMap(p, specialPrefabs[randomIndex].prefab, CellType.Structure);
        }
    }

    private int GetRandomWeightIndex(float[] weights)
    {

        //honestly idk mi töréténik itt ???????????????????
        //inkabb hogy miért törénik itt ez a kérdés     -Benedek

        float sum = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i];

        }
        float random = UnityEngine.Random.Range(0, sum);
        float tempsum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            if(random>= tempsum && random < tempsum + weights[i])
            {
                return i;
            }
            tempsum+= weights[i];

        }
        return 0;
    }


    private bool CheckPositionBeforePlacementForWoods(Vector3Int position)
    {
        if (defaultCheck(position) == false)
        {
            return false;
        };
        
        return true;
    }

    private bool CheckPositionBeforePlacement(Vector3Int position)
    {
        if (defaultCheck(position) == false) {
            return false;
        };
        if (roadCheck(position) == false)
        {
            return false;
        };
        

       
        return true;
    }

   


    private bool roadCheck(Vector3Int position)
    {
        if (placementManager.GetNeighboursOfTypeFor(position, CellType.Road).Count <= 0)
        {
            Debug.Log("Must be placed near a road");
            return false;
        }
        return true;
    }

    private bool defaultCheck(Vector3Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
        {
            Debug.Log("This position is out of bounds");
            return false;
        }
        if (placementManager.CheckIfPositionIsFree(position) == false)
        {
            Debug.Log("This position is not EMPTY");
            return false;
        }
        return true;
    }

    internal void PlaceBigStructure(Vector3Int position)
    {
        int widht = 2;
        int height = 2;
        if (checkBigStructure(position, widht, height)) {

          
            int randomIndex = GetRandomWeightIndex(bigStructureWeights);
            placementManager.PlaceObjectOnTheMap(position, bigStructurePrefabs[randomIndex].prefab, CellType.SpecialStructure, widht, height);
            
        }
    }

    private bool checkBigStructure(Vector3Int position, int widht, int height)
    {
        bool nearRoad = false;
        for (int x = 0; x < widht; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);

                if (defaultCheck(newPosition) == false)
                {
                    return false;
                }
                if (nearRoad == false)
                {
                    nearRoad = roadCheck(newPosition);
                }
            }
        }
        return nearRoad;
    }

    internal void removeStructure(Vector3Int p)
    {
        if (!defaultCheck(p))
        {     
            placementManager.RemoveHouse(p);   
        }
    }
}

[Serializable]
public struct StructurePrefabWeighted
{
    public GameObject prefab;
    
    [Range(0,1)]
    public float weight;

}
