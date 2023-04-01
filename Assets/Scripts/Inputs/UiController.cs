using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Action OnRoadPlacement, OnHousePlacement, OnSpecialPlacement, OnBigStructurePlacement, onRemoveStructure;
    public Button placeRoadButton, placeHouseButton, placeSpecialButton, placeBigStructureButton, removeStructureButton;

    public Color outlineColor;
    List<Button> buttons;

    private void Start()
    {
        buttons = new List<Button> { placeHouseButton, placeRoadButton, placeSpecialButton, placeBigStructureButton, removeStructureButton };

        placeRoadButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(placeRoadButton);
            OnRoadPlacement?.Invoke();
        
        });
        placeHouseButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(placeHouseButton);
            OnHousePlacement?.Invoke();

        });
        placeSpecialButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(placeSpecialButton);
            OnSpecialPlacement?.Invoke();

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
