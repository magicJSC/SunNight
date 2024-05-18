using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    public GameManager.HotBarInfo info;

    public GameObject sample;

    private void Start()
    {
        Managers.Game.mouse = this;
        sample = Util.FindChild(gameObject, "Sample");
        SetInfo();
    }

    private void Update()
    {
        MoveSign();
        MoveTower();
    }

    void MoveSign()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
        transform.position = mousePosition;
    }

    public void SetInfo()
    {
        info = Managers.Game.hotBar_itemInfo[Managers.Game.HotBar_Choice];

        Managers.Input.mouse0Act = null;
        Managers.Input.mouse1Act = null;

       if(info.itemType == Define.ItemType.Building)
        {
            Managers.Input.mouse0Act += DrawTile;
            Managers.Input.mouse1Act += DeleteTile;
        }
    }

    #region 건축
    void DrawTile()
    {
        //타워를 소장 하고 있을때
        if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            return;

        Vector2 tower = Managers.Game.tower.transform.position; //기지 위치 받아오기
        if (Managers.Game.tilemap.HasTile(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0)) || info.count <= 0)
            return;
        Managers.Game.tilemap.SetTile(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0), info.tile);
        Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_choice].count--;

        if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_choice].count == 0)
        {
            Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_choice].keyType = Define.KeyType.Empty;
            Managers.Game.Set_HotBar_Choice();
        }
        Managers.Game.hotBar.SetKeys();
    }

    void DeleteTile()
    {
        //타워를 소장 하고 있을때
        if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            return;

        Vector2 tower = Managers.Game.tower.transform.position; //기지 위치 받아오기
        GameObject go = Managers.Game.tilemap.GetInstantiatedObject(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0));
        if (go != null)
        {
            Managers.Game.AddItem(go.GetComponent<Item>());
            Managers.Game.Set_HotBar_Choice();
            Managers.Game.tilemap.SetTile(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0), null);
        }
    }
    #endregion

    void MoveTower()
    {
        if (info.itemType != Define.ItemType.Tower)
            return;
        if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Empty)
            return;
        Managers.Game.tower.transform.position = transform.position;

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(Managers.Game.hotBar_choice == Managers.Game.hotBar_itemInfo.Length - 1)
            {
                Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_choice].keyType = Define.KeyType.Empty;
                Managers.Game.hotBar.SetKeys();
                Managers.Game.tower.GetComponent<SpriteRenderer>().color = new Color(225, 225, 225, 120);
                Managers.Game.tilemap.color = new Color(225, 225, 225, 120);
            }
        }
    }
}
