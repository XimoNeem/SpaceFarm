using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileBehaviour : ScriptableObject
{
    public Tile ParentTile;
    public abstract void Click();

    public abstract TileBehaviour GetBehaviour();

    public abstract bool TryBuild(Building building);

    public abstract void Build(Building building);
}
