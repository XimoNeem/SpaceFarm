using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Water", menuName = "Tiles/Create TileBehaviour/Swamp")]
[System.Serializable]
public class TileBehaviour_Swamp : TileBehaviour
{
    public override void Build(Building building)
    {
        throw new System.NotImplementedException();
    }

    public override void Click()
    {

    }

    public override bool TryBuild(Building building)
    {
        Debug.Log("SWAMP");

        if (building.Type == BuildingType.WaterGenerator) { return true; }
        return false;
    }
}
