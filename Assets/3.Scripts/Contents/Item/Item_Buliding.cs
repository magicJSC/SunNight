using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Buliding : Item
{
    public Vector2 pos;

    private void Start()
    {
        pos = transform.position - Managers.Game.tower.transform.position;
        Managers.Game.tileData.Add(pos, id);
    }

    public void DeleteBuilding()
    {
        Managers.Game.tower.tilemap.SetTile(new Vector3Int((int)pos.x,(int)pos.y,0),null);
        Managers.Game.tileData.Remove(pos);
    }
}
