using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tower : MonoBehaviour
{
    bool canMove = false;
    LayerMask playerLayer;

    public void Init()
    {
        Managers.Game.tower = this;
        Managers.Game.tilemap = Util.FindChild(gameObject, "Building",true).GetComponent<Tilemap>();
        playerLayer.value = 6;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canMove)
        {
            Managers.Game.hotBar.GetTower();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            canMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            canMove = false;
        }
    }
}
