using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap walls;
    public Tilemap banBuild;
    public TileBase banTile;
    public void Init()
    {
        GameObject go = Util.FindChild(gameObject, "Wall");
        walls = go.GetComponent<Tilemap>();
    }

    public bool CheckCanBuild(Vector3Int pos)
    {
        Vector2 towerPos = Managers.Game.tower.transform.position;

        if(walls.HasTile(pos))
            return true;
        else if(banBuild.HasTile(new Vector3Int(pos.x - (int)towerPos.x,pos.y - (int)towerPos.y)))
            return true;

        return false;
    }
}
