using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_HotBar : UI_Base
{
    public ItemInfo[] itemInfo;
    GameObject[] keys = new GameObject[5];

    public class ItemInfo
    {
        public int id;
        public int count;
        public Define.ItemType itemType;

        public ItemInfo(int a,int b,Define.ItemType c)
        {
            id = a; 
            count = b;
            itemType = c;
        }
    }

    enum Keys
    {
        key1,
        key2,
        key3,
        key4,
        key5,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(Keys));
        keys[0] = Get<GameObject>((int)Keys.key1);
        keys[1] = Get<GameObject>((int)Keys.key2);
        keys[2] = Get<GameObject>((int)Keys.key3);
        keys[3] = Get<GameObject>((int)Keys.key4);
        keys[4] = Get<GameObject>((int)Keys.key5);

        Getinfo();
        SetKeys();
    }

    //값 가져오기
    void Getinfo()
    {
        int a =10;
        itemInfo = new ItemInfo[a];
        itemInfo[0] = new ItemInfo(1, 99,Define.ItemType.Material);
        for(int i = 1; i < a; i++)
        {
            itemInfo[i] = new ItemInfo(0, 0, Define.ItemType.None);
        }
    }

    //핫바에 정보 보여주기
   void SetKeys()
    {
        for(int i = 0;i<keys.Length;i++)
        {
            keys[i].GetComponent<UI_HotBar_Key>().SetIcon(itemInfo[i].id, itemInfo[i].count, itemInfo[i].itemType);
        }
    }

    public void AddItem(int a,Define.ItemType itemType)
    {

        if (itemType == Define.ItemType.Material)
        {
            int cand = -1;
            for (int i = 0; i < keys.Length-1; i++)
            {
                if (a == itemInfo[i].id && itemInfo[i].count < 99)
                {
                    itemInfo[i].count++;
                    cand = -1;
                    break;
                }
                else
                {
                    if (itemInfo[i].id == 0 && cand < 0)
                     cand = i;
                }
            }

            if(cand >= 0)
            {
                itemInfo[cand].id = a;
                itemInfo[cand].count++;
                itemInfo[cand].itemType = itemType;
            }
            else
            {
                // 인벤토리로 이동
            }
        }
        else if(itemType == Define.ItemType.Tool)
        {
            for (int i = 0; i < keys.Length-1; i++)
            {
                if (0 == itemInfo[i].id)
                {
                    itemInfo[i].id = a;
                    itemInfo[i].itemType = itemType;
                    break;
                }
            }
        }
        SetKeys();
    }
}
