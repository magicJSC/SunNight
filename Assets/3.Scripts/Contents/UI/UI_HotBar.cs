using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_HotBar : UI_Base
{
    List<UI_HotBar_Key> keys = new List<UI_HotBar_Key>();

    public override void Init()
    {
        if (keys.Count != 0)
          return;
        GetData();
        MakeKeys();
        keys[keys.Count - 1].GetComponent<Image>().color = Color.yellow;
        keys[Managers.Game.hotBar_choice].GetComponent<UI_HotBar_Key>().choice.SetActive(true);
        keys[keys.Count - 1].SetTowerIcon();
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeChoice(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeChoice(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeChoice(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeChoice(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeChoice(4);
        }
    }

    void MakeKeys()
    {
        for (int i = 0; i < Managers.Game.hotBar_itemInfo.Length; i++)
        {
            UI_HotBar_Key go = Instantiate(Resources.Load<GameObject>("UI/UI_Hotbar/Key"), transform.GetChild(0)).GetComponent<UI_HotBar_Key>();
            keys.Add(go);
            keys[i].GetComponent<UI_HotBar_Key>().Init();
            go.keyId = i;
            SetKeys(i);
        }
    }

    void ChangeChoice(int change)
    {
        keys[Managers.Game.HotBar_Choice].GetComponent<UI_HotBar_Key>().UnChoice();
        Managers.Game.HotBar_Choice = change;
        keys[Managers.Game.HotBar_Choice].GetComponent<UI_HotBar_Key>().Choice();
    }

    #region ������ ����
    //�� ��������
    public void GetData()
    {
        int a =5;
        Managers.Game.hotBar_itemInfo[0] = new GameManager.ItemInfo(3, 10);
        Managers.Game.hotBar_itemInfo[1] = new GameManager.ItemInfo(4, 1);
        for(int i = 2; i < a; i++)
        {
            Managers.Game.hotBar_itemInfo[i] = new GameManager.ItemInfo(0, 0);
        }
    }

    //�ֹٿ� ���� �����ֱ�
   public void SetKeys(int index)
    {
        keys[index].GetComponent<UI_HotBar_Key>().SetIcon();
    }

    //������ ������ �־���
    public void Set_HotBar_Info(int key_index, int id, int count)
    {
        if(id == 0)
        {
            keys[key_index].GetComponent<UI_HotBar_Key>().EmptyKey();
            return;
        }

        Item item = Resources.Load<GameObject>($"Prefabs/Items/{id}").GetComponent<Item>(); //id�� ���� ������ ����

        if (count > 99)
        {
            Managers.Game.AddItem(id, count - 99);
            count = 99;
        }
        Managers.Game.hotBar_itemInfo[key_index].id = id;
        Managers.Game.hotBar_itemInfo[key_index].itemType = item.itemType;
        Managers.Game.hotBar_itemInfo[key_index].count = count;
        Managers.Game.hotBar_itemInfo[key_index].icon = item.itemIcon;
        if (Managers.Game.hotBar_itemInfo[key_index].itemType == Define.ItemType.Building)   //�Ǽ� �������� Ÿ���� ���� ������ �ִ´�
            Managers.Game.hotBar_itemInfo[key_index].tile = item.tile;
        Managers.Game.hotBar_itemInfo[key_index].keyType = Define.KeyType.Exist;

        Managers.Game.hotBar.SetKeys(key_index);
    }
    #endregion

    #region ���� ����

    public void GetTower()
    {
        Managers.Game.hotBar_itemInfo[keys.Count - 1].itemType = Define.ItemType.Tower;
        Managers.Game.hotBar_itemInfo[keys.Count - 1].keyType = Define.KeyType.Exist;
        keys[keys.Count - 1].SetTowerIcon();
        Managers.Game.Set_HotBar_Choice();
        
    } 
    #endregion
}
