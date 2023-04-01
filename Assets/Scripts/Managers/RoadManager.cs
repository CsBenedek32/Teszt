using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public PlacementManager placementManager;

    public List<Vector3Int> temporaryPlacementPositions = new List<Vector3Int>();
    public List<Vector3Int> roadPositiontoRecheck = new List<Vector3Int>();
   

    private Vector3Int startPosition;
    private bool placementMode = false;

    public RoadFixer roadFixer;

    private void Start()
    {
        roadFixer = GetComponent<RoadFixer>();
    }

    public void PlaceRoad(Vector3Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
            return;
        if (placementManager.CheckIfPositionIsFree(position) == false)
            return;
        if(placementMode == false)
        {
            temporaryPlacementPositions.Clear();
            roadPositiontoRecheck.Clear();

            placementMode = true;
            startPosition = position;

            temporaryPlacementPositions.Add(position);
            placementManager.PlaceTemporaryStructure(position, roadFixer.deadEnd, CellType.Road);

        }
        else
        {
            placementManager.RemoveAllTemporaryStructures();
            temporaryPlacementPositions.Clear();
            foreach (var pfix in roadPositiontoRecheck)
            {
                roadFixer.FixRoadAtPosition(placementManager, pfix);
            }
            
            roadPositiontoRecheck.Clear();

            temporaryPlacementPositions = placementManager.GetPathBetween(startPosition, position);
            foreach (var temporaryPosition in temporaryPlacementPositions)
            {
                if (placementManager.CheckIfPositionIsFree(temporaryPosition) == false)
                    continue;
                placementManager.PlaceTemporaryStructure(temporaryPosition, roadFixer.deadEnd, CellType.Road);
            }
        }
        


        FixRoadPrefabs();

    }

    private void FixRoadPrefabs()
    {
        foreach (var temp in temporaryPlacementPositions)
        {
            roadFixer.FixRoadAtPosition(placementManager, temp);
            var neighbours = placementManager.GetNeighboursOfTypeFor(temp,CellType.Road);
            foreach (var roadPosition in neighbours)
            {
               if(roadPositiontoRecheck.Contains(roadPosition) == false) { 
                    roadPositiontoRecheck.Add(roadPosition);
                }
            }
        }

        foreach (var positionToFix in roadPositiontoRecheck)
        {
            roadFixer.FixRoadAtPosition(placementManager, positionToFix);
        }
    }
  



    public void FinishPlacingRoad()
    {
        placementMode = false;
        placementManager.AddtemporaryStructuresToStructureDictionary();
       
       
        temporaryPlacementPositions.Clear();
        startPosition = Vector3Int.zero;
    }

    internal void removeRoad(Vector3Int pos)
    {
        placementManager.removeRoad(pos);
        roadFixer.updateRoadAround(placementManager,pos);
    }
}