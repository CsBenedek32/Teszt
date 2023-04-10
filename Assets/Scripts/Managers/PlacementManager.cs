using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlacementManager : MonoBehaviour
{
    public int width, height;
    Grid placementGrid;

    int numberOfStructures;
    private Dictionary<Vector3Int, StructureModel> temporaryRoadobjects = new Dictionary<Vector3Int, StructureModel>();
    private Dictionary<Vector3Int, StructureModel> structureAndRoadsDictionary = new Dictionary<Vector3Int, StructureModel>();
    private StructureModel selected;

   
  
    private void Start()
    {
        numberOfStructures = 0;
        placementGrid = new Grid(width, height);
        selected = null;
       
    }

    internal CellType[] GetNeighbourTypesFor(Vector3Int position)
    {
        return placementGrid.GetAllAdjacentCellTypes(position.x, position.z);
    }

    internal Dictionary<Vector3Int,CellType> GetNeighbourPositionAndType(Vector3Int position)
    {
        Dictionary<Vector3Int, CellType> result = new Dictionary<Vector3Int, CellType>();

        foreach (var cell in placementGrid.GetAllAdjacentCells(position.x, position.z)) {
            result.Add(new Vector3Int(cell.X, 0, cell.Y),placementGrid.getCellType(new Vector3Int(cell.X, 0, cell.Y)));
        }

        return result;
           
    }

    internal bool CheckIfPositionInBound(Vector3Int position)
    {
        if (position.x >= 0 && position.x < width && position.z >= 0 && position.z < height)
        {
            return true;
        }
        return false;
    }

    internal void PlaceObjectOnTheMap(Vector3Int position, GameObject structurePrefab, CellType type, int width = 1, int height = 1)
    {
        
        StructureModel structure = CreateANewStructureModel(position, structurePrefab, type);
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);
                placementGrid[newPosition.x, newPosition.z] = type;
                structureAndRoadsDictionary.Add(newPosition, structure);
                //Debug.Log(structureAndRoadsDictionary[newPosition]);

            }
        }
        

    }

    internal void SelectObjectOnTHeMap(Vector3Int position)
    {
        selected = structureAndRoadsDictionary[position];
        Debug.Log(selected);
    }


    //ty chatgpt very cool XDDD
    public void RemoveHouse(Vector3Int p)
    {

        if (placementGrid.getCellType(p) == CellType.SpecialStructure)
        {
            if (structureAndRoadsDictionary.ContainsKey(p))
            {
                int structureId = structureAndRoadsDictionary[p].getId();

                foreach (var structureToRemove in structureAndRoadsDictionary.Where(x => x.Value.getId() == structureId).ToList())
                {
                    Destroy(structureToRemove.Value.gameObject);
                    placementGrid[structureToRemove.Key.x, structureToRemove.Key.z] = CellType.Empty;
                    structureAndRoadsDictionary.Remove(structureToRemove.Key);
                }
            }
            
        }
        else if(placementGrid.getCellType(p) == CellType.Structure)
        {
            var positionToRemove = p;
            if (placementGrid[positionToRemove.x, positionToRemove.z] == CellType.Structure)
            {
                if (structureAndRoadsDictionary.ContainsKey(positionToRemove))
                {
                    StructureModel structureToRemove = structureAndRoadsDictionary[positionToRemove];
                    structureAndRoadsDictionary.Remove(positionToRemove);
                    placementGrid[positionToRemove.x, positionToRemove.z] = CellType.Empty;
                    Destroy(structureToRemove.gameObject);
                }
            }  
        }
       
    }

    internal bool CheckIfPositionIsFree(Vector3Int position)
    {
        return CheckIfPositionIsOfType(position, CellType.Empty);
    }

    private bool CheckIfPositionIsOfType(Vector3Int position, CellType type)
    {
        return placementGrid[position.x, position.z] == type;
    }

    internal void PlaceTemporaryStructure(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        placementGrid[position.x, position.z] = type;
        StructureModel structure = CreateANewStructureModel(position, structurePrefab, type);
        temporaryRoadobjects.Add(position, structure);
    }

    internal List<Vector3Int> GetNeighboursOfTypeFor(Vector3Int position, CellType type)
    {
        var neighbourVertices = placementGrid.GetAdjacentCellsOfType(position.x, position.z, type);
        List<Vector3Int> neighbours = new List<Vector3Int>();
        foreach (var point in neighbourVertices)
        {
            neighbours.Add(new Vector3Int(point.X, 0, point.Y));
        }
        return neighbours;
    }

    private StructureModel CreateANewStructureModel(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        numberOfStructures += 1;
        GameObject structure = new GameObject(type.ToString());
        structure.transform.SetParent(transform);
        structure.transform.localPosition = position;
        var structureModel = structure.AddComponent<StructureModel>();
        structureModel.CreateModel(structurePrefab,numberOfStructures);
        return structureModel;
    }

    

    internal List<Vector3Int> GetPathBetween(Vector3Int startPosition, Vector3Int endPosition)
    {
        var resultPath = GridSearch.AStarSearch(placementGrid, new Point(startPosition.x, startPosition.z), new Point(endPosition.x, endPosition.z));
        List<Vector3Int> path = new List<Vector3Int>();
        foreach (Point point in resultPath)
        {
            path.Add(new Vector3Int(point.X, 0, point.Y));
        }
        return path;
    }

    internal void RemoveAllTemporaryStructures()
    {
        foreach (var structure in temporaryRoadobjects.Values)
        {
            var position = Vector3Int.RoundToInt(structure.transform.position);
            placementGrid[position.x, position.z] = CellType.Empty;
            Destroy(structure.gameObject);
        }
        temporaryRoadobjects.Clear();
    }

    internal void AddtemporaryStructuresToStructureDictionary()
    {
        foreach (var structure in temporaryRoadobjects)
        {
            structureAndRoadsDictionary.Add(structure.Key, structure.Value);
          
        }
        temporaryRoadobjects.Clear();
    }

    public void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation)
    {
        if (temporaryRoadobjects.ContainsKey(position))
            temporaryRoadobjects[position].SwapModel(newModel, rotation);
        else if (structureAndRoadsDictionary.ContainsKey(position))
            structureAndRoadsDictionary[position].SwapModel(newModel, rotation);
    }

    internal void removeRoad(Vector3Int pos)
    {
       
        if (structureAndRoadsDictionary.ContainsKey(pos))
        {
            StructureModel structureToRemove = structureAndRoadsDictionary[pos];
            Destroy(structureToRemove.gameObject);
            placementGrid[pos.x, pos.z] = CellType.Empty;
            structureAndRoadsDictionary.Remove(pos);
            
        }
    }

   

    
}