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
        Debug.Log($"CLICK REGULAR ON {ParentTile.name}");
    }

    public override bool TryBuild(Building building)
    {
        return true;
    }
}
