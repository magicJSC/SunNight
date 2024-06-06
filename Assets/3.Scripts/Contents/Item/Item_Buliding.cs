using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Item_Buliding : Item
{
    [HideInInspector]
    public Vector2 pos;

    private void Start()
    {
        itemType = Define.ItemType.Building;
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
