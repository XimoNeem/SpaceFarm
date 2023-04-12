using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Regular", menuName = "Tiles/Create TileBehaviour/Regular")]
[System.Serializable]
public class TileBehaviour_Regular : TileBehaviour
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
        Debug.Log("REGULAR");

        if (building.Type == BuildingType.WaterGenerator)
        {
            Debug.Log(building.Type);
            foreach (var item in ParentTile.GetNearesrTiles(TileDirection.All))
            {
                Debug.Log(item);
                if (item.TileBehaviour is TileBehaviour_Water)
                {
                    return true;
                }
            }
            return false;
        }
        return true;
    }
}
