using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Water", menuName = "Tiles/Create TileBehaviour/Water")]
[System.Serializable]
public class TileBehaviour_Water : TileBehaviour
{
    public override TileBehaviour GetBehaviour()
    {
        return new TileBehaviour_Water();
    }
    public override void Build(Building building)
    {
        throw new System.NotImplementedException();
    }

    public override void Click()
    {

    }

    public override bool TryBuild(Building building)
    {
        return false;
    }
}
