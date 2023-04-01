using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public RoadManager roadManager;
    public InputManager inputManager;

    public UiController uiController;
    public StructureManager structureManager;


    private void Start()
    {
        uiController.OnRoadPlacement += roadPlacementHandler;
        uiController.OnHousePlacement += housePlacementHandler;
        uiController.OnSpecialPlacement += specialPlacementHandler;
        uiController.OnBigStructurePlacement += bigStructurePlacementHandler;
        uiController.onRemoveStructure += removeStructureHandler;
    }

    private void removeStructureHandler()
    {
        ClearInputAction();
        
        inputManager.OnMouseClick += structureManager.removeStructure;
        inputManager.OnMouseClick += roadManager.removeRoad;
    }

    private void bigStructurePlacementHandler()
    {
        ClearInputAction();
        inputManager.OnMouseClick += structureManager.PlaceBigStructure;
    }

    private void housePlacementHandler()
    {
        ClearInputAction();
        inputManager.OnMouseClick += structureManager.PlaceHouse;
    }

    private void specialPlacementHandler()
    {
        ClearInputAction();
        inputManager.OnMouseClick += structureManager.PlaceSpecial;
    }

    private void roadPlacementHandler()
    {
        ClearInputAction();
        inputManager.OnMouseClick += roadManager.PlaceRoad;
        inputManager.OnMouseHold += roadManager.PlaceRoad;
        inputManager.OnMouseUp += roadManager.FinishPlacingRoad;

    }

    private void ClearInputAction()
    {
        inputManager.OnMouseClick = null;
        inputManager.OnMouseHold = null;
        inputManager.OnMouseUp = null;

    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
       
    }
}