using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    bool canMove = false;

    private void Start()
    {
        Managers.Game.tower = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && canMove)
        {
            Managers.Game.hotBar.GetTower();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            canMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            canMove=false;
        }
    }
}
