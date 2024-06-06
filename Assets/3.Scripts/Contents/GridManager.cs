using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap walls;
    public Tilemap build;
    public Tilemap matter;
    public TileBase matterTile;

    public void Init()
    {
        GameObject go = Util.FindChild(gameObject, "Wall");
        walls = go.GetComponent<Tilemap>();
        go = Util.FindChild(gameObject, "Matter");
        matter = go.GetComponent<Tilemap>();
        matterTile = matter.GetTile(Vector3Int.zero);
    }

    public bool CheckCanBuild(Vector3Int pos)
    {
        Vector2 towerPos = Managers.Game.tower.transform.position;

        if(walls.HasTile(pos))
            return false;
        else if(build.HasTile(new Vector3Int(pos.x - (int)towerPos.x,pos.y - (int)towerPos.y)))
            return false;
        else if (matter.HasTile(pos))
            return false;
        else if(new Vector3Int(pos.x - (int)towerPos.x, pos.y - (int)towerPos.y) == Vector3Int.zero)
            return false;

        return true;
    }
}
