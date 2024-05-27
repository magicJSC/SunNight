using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tower : MonoBehaviour
{
    bool canHold = false;
    LayerMask playerLayer;
    LayerMask buildLayer;
    LayerMask inviLayer;

    public Tilemap tilemap;

    public void Init()
    {
        Managers.Game.tower = this;
        tilemap = Util.FindChild(gameObject,"Building",true).GetComponent<Tilemap>();
        playerLayer = 6;
        buildLayer.value = 9;
        inviLayer.value = 8;
    }

    private void Update()
    {
        if (Managers.Game.timeType == Define.TimeType.Night)
            return;

        if (Input.GetKeyDown(KeyCode.F) && canHold)
        {
            Managers.Game.hotBar.GetTower();
        }
    }

    public void ChangeInvisable()
    {
        gameObject.layer = inviLayer;
        for (int i = 0; i < tilemap.transform.childCount; i++)
        {
            tilemap.transform.GetChild(i).gameObject.layer = inviLayer;
        }
    }

    public void ChangeVisable()
    {
        gameObject.layer = buildLayer;
        for (int i = 0; i < tilemap.transform.childCount; i++)
        {
            tilemap.transform.GetChild(i).gameObject.layer = buildLayer;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            canHold = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            canHold = false;
        }
    }
}
