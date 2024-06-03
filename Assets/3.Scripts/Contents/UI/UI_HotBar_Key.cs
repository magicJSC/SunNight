using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Managers;

public class UI_HotBar_Key : UI_Base
{
    public int keyId;

    Image icon;
    Text count;
    public GameObject choice;
    enum Images
    {
        item,
    }

    enum Texts
    {
        Count
    }

    enum GameObjects
    {
        Choice
    }

    public new void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        icon = Get<Image>((int)Images.item);
        count = Get<Text>((int)Texts.Count);
        choice = Get<GameObject>((int)GameObjects.Choice);
        choice.SetActive(false);

        UI_EventHandler evt = GetComponent<UI_EventHandler>();
        evt._OnEnter += (PointerEventData p) =>
        {
            
            if (Game.mouse.CursorType == Define.CursorType.Drag)
            {
                if (keyId != Game.hotBar_itemInfo.Length - 1)
                {
                    Game.changeSpot.index = keyId;
                    Game.changeSpot.invenType = Define.InvenType.HotBar;
                }
            }
            else
                Game.mouse.CursorType = Define.CursorType.Normal;
            
        };
        evt._OnExit += (PointerEventData p) => 
        {
            if (Game.mouse.CursorType == Define.CursorType.Drag)
                Game.changeSpot.invenType = Define.InvenType.None;
            else
                Game.Set_HotBar_Choice();
        };
        evt._OnDown += (PointerEventData p) => 
        {
            if (Game.hotBar_itemInfo[keyId].keyType == Define.KeyType.Empty)
                return;
            Game.mouse.CursorType = Define.CursorType.Drag;
            Game.mouse.Set_Mouse_ItemIcon(icon,count);
        };
        evt._OnUp += (PointerEventData p) => 
        {
            Define.DropType drop = Drop();
            switch (drop) 
            {
                case Define.DropType.Move:
                    MoveItemSpot();
                    break;
                case Define.DropType.Change:
                    ChangeItemSpot();
                    break;
                case Define.DropType.Add:
                    CombineItem();
                    break;
                case Define.DropType.Return:
                    ShowIcon();
                    break;
            }
            Game.mouse.CursorType = Define.CursorType.Normal;
        };
    }


    public void SetIcon()
    {
        GameManager.ItemInfo hotBar = Game.hotBar_itemInfo[keyId];
        if(hotBar.itemType == Define.ItemType.Tower)
        {
            SetTowerIcon();
            return;
        }

        if (hotBar.keyType == Define.KeyType.Empty)
        {
            EmptyKey();
            return;
        }
        count.gameObject.SetActive(true);
        icon.gameObject.SetActive(true);
        if(hotBar.itemType == Define.ItemType.Tool || hotBar.itemType == Define.ItemType.Tower)
            count.gameObject.SetActive(false);
        icon.sprite = hotBar.icon;

        count.text = hotBar.count.ToString();
    }

    public void Choice()
    {
        choice.gameObject.SetActive(true);
    }

    public void UnChoice()
    {
        choice.gameObject.SetActive(false);
    }

    public void EmptyKey()  //키 비어있게 만들기
    {
        HideIcon();
        Game.hotBar_itemInfo[keyId].keyType = Define.KeyType.Empty;
        Game.hotBar_itemInfo[keyId].itemType = Define.ItemType.None;
        Game.hotBar_itemInfo[keyId].count = 0;
        Game.hotBar_itemInfo[keyId].id = 0;
    }

    public void HideIcon()
    {
        icon.gameObject.SetActive(false);
        count.gameObject.SetActive(false);
    }
    public void ShowIcon()
    {
        icon.gameObject.SetActive(true);
        if(Game.hotBar_itemInfo[keyId].itemType == Define.ItemType.Tool)
        count.gameObject.SetActive(true);
    }
    public void SetTowerIcon()
    {

        if (Game.hotBar_itemInfo[Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Empty)
        {
            icon.gameObject.SetActive(false);
            count.gameObject.SetActive(false);
            Game.hotBar_itemInfo[Game.hotBar_itemInfo.Length - 1].itemType = Define.ItemType.None;
            return;
        }
        icon.gameObject.SetActive(true);
        icon.sprite = Game.GetComponent<SpriteRenderer>().sprite;
    }

    Define.DropType Drop()
    {
        if (Managers.Game.changeSpot.invenType == Define.InvenType.Inven)
        {
            if (Managers.Game.changeSpot.invenType == Define.InvenType.None)
                return Define.DropType.Return;
            else if (Managers.Game.inven_itemInfo[Managers.Game.changeSpot.index].id == Managers.Game.hotBar_itemInfo[keyId].id)
                return Define.DropType.Add;
            else if (Managers.Game.inven_itemInfo[Managers.Game.changeSpot.index].keyType == Define.KeyType.Empty)
                return Define.DropType.Move;
        }
        else
        {
            if (Managers.Game.changeSpot.invenType == Define.InvenType.None)
                return Define.DropType.Return;
            else if (Managers.Game.hotBar_itemInfo[Managers.Game.changeSpot.index].id == Managers.Game.hotBar_itemInfo[keyId].id)
                return Define.DropType.Add;
            else if (Managers.Game.hotBar_itemInfo[Managers.Game.changeSpot.index].keyType == Define.KeyType.Empty)
                return Define.DropType.Move;
        }

        return Define.DropType.Change;
    }

    public void ChangeItemSpot()
    {
        if (Managers.Game.changeSpot.invenType == Define.InvenType.None)
            ShowIcon();
        //키 자신의 값
        int id = Managers.Game.hotBar_itemInfo[keyId].id;
        int count = Managers.Game.hotBar_itemInfo[keyId].count;
        if(Managers.Game.changeSpot.invenType == Define.InvenType.Inven)
        {
            Managers.Game.hotBar.Set_HotBar_Info(keyId, Managers.Game.inven_itemInfo[Managers.Game.changeSpot.index].id, Managers.Game.inven_itemInfo[Managers.Game.changeSpot.index].count);
            Managers.Game.inven.Set_Inven_Info(Managers.Game.changeSpot.index,id,count);
        }
        else
        {
            Managers.Game.hotBar.Set_HotBar_Info(keyId, Managers.Game.hotBar_itemInfo[Managers.Game.changeSpot.index].id, Managers.Game.hotBar_itemInfo[Managers.Game.changeSpot.index].count);
            Managers.Game.hotBar.Set_HotBar_Info(Managers.Game.changeSpot.index, id, count);
        }
    }

    void MoveItemSpot()
    {
        int id = Managers.Game.hotBar_itemInfo[keyId].id;
        int count = Managers.Game.hotBar_itemInfo[keyId].count;
        if (Managers.Game.changeSpot.invenType == Define.InvenType.Inven)
        {
            Managers.Game.hotBar.Set_HotBar_Info(keyId, 0, 0);
            Managers.Game.inven.Set_Inven_Info(Managers.Game.changeSpot.index, id, count);
        }
        else
        {
            Managers.Game.hotBar.Set_HotBar_Info(keyId, 0, 0);
            Managers.Game.hotBar.Set_HotBar_Info(Managers.Game.changeSpot.index, id, count);
        }
    }

    void CombineItem()
    {
        int id = Managers.Game.hotBar_itemInfo[keyId].id;
        int count = Managers.Game.hotBar_itemInfo[keyId].count;
        if (Managers.Game.changeSpot.invenType == Define.InvenType.Inven)
        {
            Managers.Game.inven.Set_Inven_Info(keyId, 0, 0);
            Managers.Game.inven.Set_Inven_Info(Managers.Game.changeSpot.index, id, count + Managers.Game.inven_itemInfo[Managers.Game.changeSpot.index].count);
        }
        else
        {
            Managers.Game.inven.Set_Inven_Info(keyId, 0, 0);
            Managers.Game.hotBar.Set_HotBar_Info(Managers.Game.changeSpot.index, id, count + Managers.Game.hotBar_itemInfo[Managers.Game.changeSpot.index].count);
        }
    }
}
