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

    #region 아이템 관련
    //값 가져오기
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

    //핫바에 정보 보여주기
   public void SetKeys(int index)
    {
        keys[index].GetComponent<UI_HotBar_Key>().SetIcon();
    }

    //아이템 정보를 넣어줌
    public void Set_HotBar_Info(int key_index, int id, int count)
    {
        if(id == 0)
        {
            keys[key_index].GetComponent<UI_HotBar_Key>().EmptyKey();
            return;
        }

        Item item = Resources.Load<GameObject>($"Prefabs/Items/{id}").GetComponent<Item>(); //id에 따른 아이템 정보


        Managers.Game.hotBar_itemInfo[key_index].id = id;
        Managers.Game.hotBar_itemInfo[key_index].itemType = item.itemType;
        Managers.Game.hotBar_itemInfo[key_index].count = count;
        Managers.Game.hotBar_itemInfo[key_index].icon = item.itemIcon;
        if (Managers.Game.hotBar_itemInfo[key_index].itemType == Define.ItemType.Building)   //건설 아이템은 타일을 따로 가지고 있는다
            Managers.Game.hotBar_itemInfo[key_index].tile = item.tile;
        Managers.Game.hotBar_itemInfo[key_index].keyType = Define.KeyType.Exist;

        Managers.Game.hotBar.SetKeys(key_index);
    }
    #endregion

    #region 기지 관련

    public void GetTower()
    {
        Managers.Game.hotBar_itemInfo[keys.Count - 1].itemType = Define.ItemType.Tower;
        Managers.Game.hotBar_itemInfo[keys.Count - 1].keyType = Define.KeyType.Exist;
        keys[keys.Count - 1].SetTowerIcon();
        Managers.Game.Set_HotBar_Choice();
    } 
    #endregion
}
