using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Define;

public class Builder : MonoBehaviour
{
    public InvenManager.ItemInfo info;

    public GameObject sample;

    public bool canBuild;
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
            if (Managers.Inven.hotBar_choice == Managers.Inven.hotBar_itemInfo.Length - 1)
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
        info = Managers.Inven.hotBar_itemInfo[Managers.Inven.HotBar_Choice];

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
        if (Managers.Inven.hotBar_itemInfo[Managers.Inven.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            return;

        Vector2 tower = Managers.Game.tower.transform.position; //���� ��ġ �޾ƿ���
        if (Managers.Game.grid.CheckCanBuild(new Vector3Int((int)(transform.position.x), (int)(transform.position.y), 0)))
            return;
        Managers.Game.tower.build.SetTile(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0), info.tile);
        Managers.Inven.hotBar_itemInfo[Managers.Inven.hotBar_choice].count--;
        if(Managers.Inven.hotBar_itemInfo[Managers.Inven.hotBar_choice].count <=0)
            Managers.Game.mouse.CursorType = CursorType.Normal;
        Managers.Inven.hotBar.SetKeys(Managers.Inven.hotBar_choice);
    }

    void DeleteTile()
    {
        if (Managers.Game.mouse.CursorType != CursorType.Builder)
            return;
        //Ÿ���� ���� �ϰ� ������
        if (Managers.Inven.hotBar_itemInfo[Managers.Inven.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            return;

        Vector2 tower = Managers.Game.tower.transform.position; //���� ��ġ �޾ƿ���
        GameObject go = Managers.Game.tower.build.GetInstantiatedObject(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0));
        if (go != null)
        {
            Managers.Inven.AddItem(go.GetComponent<Item>().id);
            Managers.Inven.Set_HotBar_Choice();
            go.GetComponent<Item_Buliding>().DeleteBuilding();
        }
    }

    public void ShowBuildSample()
    {
        //�������� ��ġ �������϶�
        if (Managers.Inven.hotBar_itemInfo[Managers.Inven.HotBar_Choice].keyType == Define.KeyType.Exist)
        {
            sample.SetActive(true);
            sample.GetComponent<SpriteRenderer>().sprite = Managers.Inven.hotBar_itemInfo[Managers.Inven.HotBar_Choice].icon;
        }

        //���� ��尡 �ƴҶ� �����ϰ� ������ 
        if (Managers.Game.mouse.CursorType != Define.CursorType.Builder && Managers.Inven.hotBar_itemInfo[Managers.Inven.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            Managers.Game.tower.gameObject.SetActive(false);

        //���� ����϶� ������ �����ϰ� �ְ� �ٸ� ������ �ϰ� ������
        if (Managers.Game.mouse.CursorType == Define.CursorType.Builder && Managers.Inven.hotBar_itemInfo[Managers.Inven.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist && Managers.Inven.HotBar_Choice != Managers.Inven.hotBar_itemInfo.Length - 1)
            Managers.Game.tower.gameObject.SetActive(false);
    } 

   

    public void BuildTower(bool force = false)
    {
        //���� ��ġ
        if (force)
            Managers.Game.tower.transform.position = Managers.Game.player.transform.position;

        Managers.Inven.hotBar_itemInfo[Managers.Inven.hotBar_itemInfo.Length - 1].keyType = Define.KeyType.Empty;
        Managers.Inven.hotBar.SetKeys(Managers.Inven.hotBar_itemInfo.Length - 1);
        Managers.Game.tower.gameObject.SetActive(true);
        Managers.Game.tower.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        Managers.Game.tower.build.color = new Color(1,1,1,1);
        Managers.Inven.Set_HotBar_Choice();
        Managers.Game.tower.ChangeVisable();
    }

    public void ShowTowerSample()
    {
         //������ �����Ҷ� ������ �����Ѵٸ�
        if (Managers.Inven.hotBar_itemInfo[Managers.Inven.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
        {
            Managers.Game.tower.gameObject.SetActive(true);
            Managers.Game.tower.GetComponent<SpriteRenderer>().color = new Color32(225, 225, 225, 120);
            Managers.Game.tower.build.color = new Color32(225, 225, 225, 120);
            Managers.Game.tower.ChangeInvisable();
        }
        sample.SetActive(false);
    }

    public void HideSample()
    {
        //���� ��尡 �ƴҶ� �����ϰ� ������ 
        if (Managers.Game.mouse.CursorType != Define.CursorType.Builder && Managers.Inven.hotBar_itemInfo[Managers.Inven.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            Managers.Game.tower.gameObject.SetActive(false);
        
        //���� ����϶� ������ �����ϰ� �ְ� �ٸ� ������ �ϰ� ������
        if (Managers.Game.mouse.CursorType == Define.CursorType.Builder && Managers.Inven.hotBar_itemInfo[Managers.Inven.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist && Managers.Inven.HotBar_Choice != Managers.Inven.hotBar_itemInfo.Length - 1)
            Managers.Game.tower.gameObject.SetActive(false);
        sample.SetActive(false);
    }
    #endregion

   
}
