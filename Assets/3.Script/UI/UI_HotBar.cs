using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_HotBar : UI_Base
{
    List<GameObject> keys = new List<GameObject>();

    public override void Init()
    {
        Managers.Game.hotBar = this;
        for(int i = 0;i<5;i++)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("UI/Hotbar/Key"),transform.GetChild(0));
            keys.Add(go);
            keys[i].GetComponent<UI_HotBar_Key>().Init_Key();
            keys[i].GetComponent<UI_HotBar_Key>().choice.SetActive(false);
        }
        keys[keys.Count -1].GetComponent<Image>().color = Color.yellow;
        keys[Managers.Game.hotBar_choice].GetComponent<UI_HotBar_Key>().choice.SetActive(true);
        Getinfo();
        SetKeys();
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

    void ChangeChoice(int change)
    {
        keys[Managers.Game.HotBar_Choice].GetComponent<UI_HotBar_Key>().UnChoice();
        Managers.Game.HotBar_Choice = change;
        keys[Managers.Game.HotBar_Choice].GetComponent<UI_HotBar_Key>().Choice();
    }

    #region 핫바 값관련
    //값 가져오기
    void Getinfo()
    {
        int a =5;
        Managers.Game.hotBar_itemInfo = new GameManager.ItemInfo[a];
        Managers.Game.hotBar_itemInfo[0] = new GameManager.ItemInfo(3,10);
        for(int i = 1; i < a; i++)
        {
            Managers.Game.hotBar_itemInfo[i] = new GameManager.ItemInfo(0,0);
        }
    }

    //핫바에 정보 보여주기
   public void SetKeys()
    {
        for(int i = 0;i<keys.Count;i++)
        {
            keys[i].GetComponent<UI_HotBar_Key>().SetIcon(Managers.Game.hotBar_itemInfo[i].id, Managers.Game.hotBar_itemInfo[i].count);
        }
    }

    //얻은 아이템 데이터 넣어주기
    public void AddItem(int a,Define.ItemType itemType)
    {

        if (itemType != Define.ItemType.Tool)   //재료 아이템일때
        {
            bool added = false;
            int empty = -1;
            for (int i = 0; i < keys.Count-1; i++)
            {
                if (a == Managers.Game.hotBar_itemInfo[i].id && Managers.Game.hotBar_itemInfo[i].count < 99)
                {
                    Managers.Game.hotBar_itemInfo[i].count++;
                    added = true;
                    break;
                }
                else
                {
                    if(empty == -1 && Managers.Game.hotBar_itemInfo[i].id == 0)
                        empty = i;
                }
            }
            //추가 하지 못했다면 비어있는 칸에 넣기
            if(!added)
            {
                Managers.Game.hotBar_itemInfo[empty].id = a;
                Managers.Game.hotBar_itemInfo[empty].count++;
            }
        }
        else //도구 아이템일때
        {
            for (int i = 0; i < keys.Count-1; i++)
            {
                if (0 == Managers.Game.hotBar_itemInfo[i].id)
                {
                    Managers.Game.hotBar_itemInfo[i].id = a;
                    Managers.Game.hotBar_itemInfo[i].itemType = itemType;
                    break;
                }
            }
        }
        SetKeys();
    }
    #endregion


}
