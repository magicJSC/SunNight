using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Define;

public class MouseController : MonoBehaviour
{
    public GameManager.HotBarInfo info;

    public GameObject sample;

    private void Start()
    {
        if(Managers.Game.mouse == null)
         Init();
    }

    public void Init()
    {
        Managers.Game.mouse = this;
        sample = Util.FindChild(gameObject, "Sample");
    }

    private void Update()
    {
        if (info == null)
            return;
        MoveMouse();
        MoveTower();
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

       if(info.itemType == Define.ItemType.Building)
        {
            Managers.Input.mouse0Act += DrawTile;
            Managers.Input.mouse1Act += DeleteTile;
        }
    }

    #region ����
    void DrawTile()
    {
        //Ÿ���� ���� �ϰ� ������
        if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            return;

        Vector2 tower = Managers.Game.tower.transform.position; //���� ��ġ �޾ƿ���
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
        //Ÿ���� ���� �ϰ� ������
        if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            return;

        Vector2 tower = Managers.Game.tower.transform.position; //���� ��ġ �޾ƿ���
        GameObject go = Managers.Game.tilemap.GetInstantiatedObject(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0));
        if (go != null)
        {
            Managers.Game.AddItem(go.GetComponent<Item>());
            Managers.Game.Set_HotBar_Choice();
            Managers.Game.tilemap.SetTile(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0), null);
        }
    }

    public void ShowBuildSample()
    {
        //�������� ��ġ �������϶�
        if (Managers.Game.hotBar_itemInfo[Managers.Game.HotBar_Choice].keyType == Define.KeyType.Exist)
        {
            Managers.Game.mouse.sample.SetActive(true);
            Managers.Game.mouse.sample.GetComponent<SpriteRenderer>().sprite = Managers.Game.hotBar_itemInfo[Managers.Game.HotBar_Choice].icon;
        }

        //���� ��尡 �ƴҶ� �����ϰ� ������ 
        if (Managers.Game.PlayType != Define.PlayType.Builder && Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            Managers.Game.tower.gameObject.SetActive(false);

        //���� ����϶� ������ �����ϰ� �ְ� �ٸ� ������ �ϰ� ������
        if (Managers.Game.PlayType == Define.PlayType.Builder && Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist && Managers.Game.HotBar_Choice != Managers.Game.hotBar_itemInfo.Length - 1)
            Managers.Game.tower.gameObject.SetActive(false);
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
    public void ShowTowerSample()
    {
            //������ �����ϰ� ������ �����ϰ� ���� ������
        if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
        {
            Managers.Game.tower.gameObject.SetActive(true);
            Managers.Game.tower.GetComponent<SpriteRenderer>().color = new Color32(225, 225, 225, 120);
            Managers.Game.tilemap.color = new Color32(225, 225, 225, 120);
        }
        Managers.Game.mouse.sample.SetActive(false);
    }

    public void HideSample()
    {
        //���� ��尡 �ƴҶ� �����ϰ� ������ 
        if (Managers.Game.PlayType != Define.PlayType.Builder && Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist)
            Managers.Game.tower.gameObject.SetActive(false);
        
        //���� ����϶� ������ �����ϰ� �ְ� �ٸ� ������ �ϰ� ������
        if (Managers.Game.PlayType == Define.PlayType.Builder && Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist && Managers.Game.HotBar_Choice != Managers.Game.hotBar_itemInfo.Length - 1)
            Managers.Game.tower.gameObject.SetActive(false);
        Managers.Game.mouse.sample.SetActive(false);
    }
}
