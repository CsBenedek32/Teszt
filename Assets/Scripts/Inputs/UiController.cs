using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Action OnRoadPlacement, OnResidentialPlacement, OnCommercialPlacement, OnIndustrialPlacement, OnBigStructurePlacement, onRemoveStructure,onSelect;
    public Button placeRoadButton, placeResidentialButton, placeCommercialButton, placeIndustrialButton, placeBigStructureButton, removeStructureButton,selectButton;


    public Color outlineColor;
    List<Button> buttons;

    private void Start()
    {
        buttons = new List<Button> { 
            placeResidentialButton,
            placeCommercialButton,
            placeIndustrialButton,

            placeRoadButton,
            placeBigStructureButton,
            selectButton
            removeStructureButton };


        placeRoadButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(placeRoadButton);
            OnRoadPlacement?.Invoke();
        
        });
        placeResidentialButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(placeResidentialButton);
            OnResidentialPlacement?.Invoke();

        });
        placeCommercialButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(placeCommercialButton);
            OnCommercialPlacement?.Invoke();

        });
        placeIndustrialButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(placeIndustrialButton);
            OnIndustrialPlacement?.Invoke();

        });
        placeBigStructureButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(placeBigStructureButton);
            OnBigStructurePlacement?.Invoke();

        });
        removeStructureButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(removeStructureButton);
            onRemoveStructure?.Invoke();

        });

        selectButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(selectButton);
            onSelect?.Invoke();

        });

    }

    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    private void ResetButtonColor()
    {
        foreach (var button in buttons)
        {
            button.GetComponent<Outline>().enabled = false;
        }
    }
}
