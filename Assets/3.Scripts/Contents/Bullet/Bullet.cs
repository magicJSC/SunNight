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
                Debug.Log("Àå¾Ö¹° ÆÄ±«");
                Destroy(col);
            }
            else if (col.GetComponent<Tower>())
            {
                Debug.Log("±âÁö ÆÄ±«");
            }
        }
        Destroy(gameObject);
    }
}
