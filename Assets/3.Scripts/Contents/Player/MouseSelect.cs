using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseSelect : MonoBehaviour
{
    public Tilemap tilemap;
    public GameManager.HotBarInfo info;

    public GameObject sample;

    private void Start()
    {
        Managers.Game.select = this;
        sample = Util.FindChild(gameObject, "Sample");
    }

    private void Update()
    {
        Move();
        Use();
    }

    void Move()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
        transform.position = mousePosition;
    }

    void Use()
    {
        if(info == null)
        {
            info = Managers.Game.hotBar_itemInfo[Managers.Game.HotBar_Choice];
            Managers.Game.Set_HotBar_Choice();
        }

        switch(info.itemType)
        {
            case Define.ItemType.Building:
                ChangeTile();
                break;
            case Define.ItemType.Tower:
                MoveTower();
                break;
        }
    }

    #region 건축
    void ChangeTile()
    {
        //타워를 소장 하고 있을때
        if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0)) //그리기
        {
            Vector2 tower = Managers.Game.tower.transform.position; //기지 위치 받아오기
            if (tilemap.HasTile(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0)) || info.count <= 0)
                return;
            tilemap.SetTile(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0), info.tile);
            Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_choice].count--;

            if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_choice].count == 0)
            {
                Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_choice].keyType = Define.KeyType.Empty;
                Managers.Game.Set_HotBar_Choice();
            }
            Managers.Game.hotBar.SetKeys();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1)) //지우기
        {
            Vector2 tower = Managers.Game.tower.transform.position; //기지 위치 받아오기
            GameObject go = tilemap.GetInstantiatedObject(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0));
            if (go != null)
            {
                Managers.Game.AddItem(go.GetComponent<Item>());
                Managers.Game.Set_HotBar_Choice();
                tilemap.SetTile(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0), null);
            }
        }
    }
    #endregion

    void MoveTower()
    {
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
                tilemap.color = new Color(225, 225, 225, 120);
            }
        }
    }
}
