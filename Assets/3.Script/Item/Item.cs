using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite itemIcon;
    public int id;
    public Define.ItemType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            collision.GetComponent<PlayerController>().hotBar.AddItem(id,type);
            Destroy(gameObject);
        }
    }
}
