using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Constructing_WindowControler : WindowController
{
    [Space(10)]
    public Button ApplyButton;

    public void SetApplyButtonActive(bool state)
    {
        ApplyButton.interactable = state;
    }

    public void ApplyBuild()
    {
        FindObjectOfType<BuildMenager>().CreateBuilding();
        this.ShowWindow(false);
        FindObjectOfType<Main_WindowController>().ShowWindow(true);
    }
   
    public void CancelBuild()
    {
        FindObjectOfType<BuildMenager>().CancelBuilding();
        this.ShowWindow(false);
        FindObjectOfType<Building_WindowControler>().ShowWindow(true);
    }
}
