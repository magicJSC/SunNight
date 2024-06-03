using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Define;

public class Builder : MonoBehaviour
{
    public GameManager.ItemInfo info;

    public GameObject sample;

   
    private void Start()
    {
        if(Managers.Game.mouse == null)
         Init();
    }

    public void Init()
    {
        Managers.Game.build = this;
        sample = Util.FindChild(gameObject, "Sample");
    }

    private void Update()
    {
        if (info == null)
            return;
        MoveMouse();
        MoveTower();
    }

    void MoveTower()
    {
        if (info.itemType != ItemType.Tower)
            return;
        Managers.Game.tower.transform.position = transform.position;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Managers.Game.hotBar_choice == Managers.Game.hotBar_itemInfo.Length - 1)
            {
                BuildTower();
            }
        }
    }

    void MoveMouse()
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

       if(info.itemType == ItemType.Building)
        {
            Managers.Input.mouse0Act += DrawTile;
            Managers.Input.mouse1Act += DeleteTile;
        }
    }

    #region ����
    void DrawTile()
    {
        if (Managers.Game.mouse.CursorType != CursorType.Builder)
            return;
        //Ÿ���� ���� �ϰ� ������
        if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            return;

        Vector2 tower = Managers.Game.tower.transform.position; //���� ��ġ �޾ƿ���
        if (Managers.Game.tower.tilemap.HasTile(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0)) || info.count <= 0)
            return;
        Managers.Game.tower.tilemap.SetTile(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0), info.tile);
        Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_choice].count--;

        if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_choice].count == 0)
        {
            Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_choice].keyType = Define.KeyType.Empty;
            Managers.Game.Set_HotBar_Choice();
        }
        Managers.Game.hotBar.SetKeys(Managers.Game.hotBar_choice);
    }

    void DeleteTile()
    {
        if (Managers.Game.mouse.CursorType != CursorType.Builder)
            return;
        //Ÿ���� ���� �ϰ� ������
        if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            return;

        Vector2 tower = Managers.Game.tower.transform.position; //���� ��ġ �޾ƿ���
        GameObject go = Managers.Game.tower.tilemap.GetInstantiatedObject(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0));
        if (go != null)
        {
            Managers.Game.AddItem(go.GetComponent<Item>().id);
            Managers.Game.Set_HotBar_Choice();
            go.GetComponent<Item_Buliding>().DeleteBuilding();
        }
    }

    public void ShowBuildSample()
    {
        //�������� ��ġ �������϶�
        if (Managers.Game.hotBar_itemInfo[Managers.Game.HotBar_Choice].keyType == Define.KeyType.Exist)
        {
            sample.SetActive(true);
            sample.GetComponent<SpriteRenderer>().sprite = Managers.Game.hotBar_itemInfo[Managers.Game.HotBar_Choice].icon;
        }

        //���� ��尡 �ƴҶ� �����ϰ� ������ 
        if (Managers.Game.mouse.CursorType != Define.CursorType.Builder && Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            Managers.Game.tower.gameObject.SetActive(false);

        //���� ����϶� ������ �����ϰ� �ְ� �ٸ� ������ �ϰ� ������
        if (Managers.Game.mouse.CursorType == Define.CursorType.Builder && Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist && Managers.Game.HotBar_Choice != Managers.Game.hotBar_itemInfo.Length - 1)
            Managers.Game.tower.gameObject.SetActive(false);
    } 

   

    public void BuildTower(bool force = false)
    {
        //���� ��ġ
        if (force)
            Managers.Game.tower.transform.position = Managers.Game.player.transform.position;

        Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType = Define.KeyType.Empty;
        Managers.Game.hotBar.SetKeys(Managers.Game.hotBar_itemInfo.Length - 1);
        Managers.Game.tower.gameObject.SetActive(true);
        Managers.Game.tower.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        Managers.Game.tower.tilemap.color = new Color(1,1,1,1);
        Managers.Game.Set_HotBar_Choice();
        Managers.Game.tower.ChangeVisable();
    }

    public void ShowTowerSample()
    {
         //������ �����Ҷ� ������ �����Ѵٸ�
        if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
        {
            Managers.Game.tower.gameObject.SetActive(true);
            Managers.Game.tower.GetComponent<SpriteRenderer>().color = new Color32(225, 225, 225, 120);
            Managers.Game.tower.tilemap.color = new Color32(225, 225, 225, 120);
            Managers.Game.tower.ChangeInvisable();
        }
        sample.SetActive(false);
    }

    public void HideSample()
    {
        //���� ��尡 �ƴҶ� �����ϰ� ������ 
        if (Managers.Game.mouse.CursorType != Define.CursorType.Builder && Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            Managers.Game.tower.gameObject.SetActive(false);
        
        //���� ����϶� ������ �����ϰ� �ְ� �ٸ� ������ �ϰ� ������
        if (Managers.Game.mouse.CursorType == Define.CursorType.Builder && Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist && Managers.Game.HotBar_Choice != Managers.Game.hotBar_itemInfo.Length - 1)
            Managers.Game.tower.gameObject.SetActive(false);
        sample.SetActive(false);
    }
    #endregion

   
}