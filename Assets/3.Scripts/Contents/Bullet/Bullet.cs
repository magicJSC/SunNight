using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    public LayerMask player;

    private void Start()
    {
        Destroy(gameObject,5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Mathf.Log(player.value,2))
        {
            Hit(collision);
        }
    }

    void Hit(Collider2D col)
    {
        col.GetComponent<Stat>().Hp -= damage; 
        if(col.GetComponent<Stat>().Hp <= 0)
        {
            if (col.GetComponent<Item>())
            {
                Vector2 tower = Managers.Game.tower.transform.position;
                Managers.Game.tilemap.SetTile(new Vector3Int((int)(col.transform.position.x - tower.x), (int)(col.transform.position.y - tower.y), 0), null);
            }
            else if (col.GetComponent<Tower>())
            {
                Debug.Log("±âÁö ÆÄ±«");
            }
        }
        Destroy(gameObject);
    }
}
