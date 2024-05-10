using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Item : MonoBehaviour
{
    public Sprite itemIcon;
    public int id;
    public Define.ItemType type;
    public TileBase tile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            collision.GetComponent<PlayerController>().hotBar.AddItem(id,type);
            Destroy(gameObject);
        }
    }
}
