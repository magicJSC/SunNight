using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Buliding : Item
{
    public Vector2 pos;

    private void Start()
    {
        pos = transform.position - Managers.Game.tower.transform.position;
        Managers.Game.buildData.Add(pos, id);
        Managers.Game.grid.banBuild.SetTile(new Vector3Int((int)pos.x,(int)pos.y,0), Managers.Game.grid.banTile);
    }

    public void DeleteBuilding()
    {
        Managers.Game.tower.build.SetTile(new Vector3Int((int)pos.x,(int)pos.y,0),null);
        Managers.Game.buildData.Remove(pos);
    }
}
