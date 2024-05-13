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
        Managers.Game.hotBar = this;
        for(int i = 0;i<5;i++)
        {
            UI_HotBar_Key go = Instantiate(Resources.Load<GameObject>("UI/Hotbar/Key"),transform.GetChild(0)).GetComponent<UI_HotBar_Key>();
            keys.Add(go);
            keys[i].GetComponent<UI_HotBar_Key>().Init_Key();
            keys[i].GetComponent<UI_HotBar_Key>().choice.SetActive(false);
        }
        keys[keys.Count - 1].GetComponent<Image>().color = Color.yellow;
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

    #region ������ ����
    //�� ��������
    void Getinfo()
    {
        int a =5;
        Managers.Game.hotBar_itemInfo[0] = new GameManager.HotBarInfo(3, 10);
        for(int i = 1; i < a; i++)
        {
            Managers.Game.hotBar_itemInfo[i] = new GameManager.HotBarInfo(0, 0);
        }
        Managers.Game.hotBar_itemInfo[keys.Count - 1].itemType = Define.ItemType.Tower; //�������� ������ Ÿ����
        keys[keys.Count - 1].SetTowerIcon();
    }

    //�ֹٿ� ���� �����ֱ�
   public void SetKeys()
    {
        for(int i = 0;i<keys.Count;i++)
        {
            keys[i].GetComponent<UI_HotBar_Key>().SetIcon(i);
        }
    }
    #endregion

    #region ���� ����

    public void GetTower()
    {
        Managers.Game.hotBar_itemInfo[keys.Count - 1].keyType = Define.KeyType.Exist;
        keys[keys.Count - 1].SetTowerIcon();
        Managers.Game.Set_HotBar_Choice();
    } 
    #endregion
}
