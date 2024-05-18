using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_Monster : MonsterController
{
    void Atk()
    {
        Collider2D col = Physics2D.OverlapBox(transform.position + (target.position - transform.position).normalized / 2, new Vector2(1, 1), (target.position - transform.position).normalized.z, player);
        if (col != null)
        {
            col.GetComponent<Stat>().Hp -= _stat.Dmg;
            if (col.GetComponent<Stat>().Hp <= 0)
            {
                if (col.GetComponent<Item>())
                {
                    Vector2 tower = Managers.Game.tower.transform.position;
                    Managers.Game.tilemap.SetTile(new Vector3Int((int)(col.transform.position.x - tower.x),(int)(col.transform.position.y - tower.y), 0),null);
                }
                else if (col.GetComponent<Tower>())
                {
                    Debug.Log("±âÁö ÆÄ±«");
                }
            }
        }
    }
}
