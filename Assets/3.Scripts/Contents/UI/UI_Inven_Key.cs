using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Managers;

public class UI_Inven_Key : UI_Base
{
    public int keyId;

    Image icon;
    Text count;
    enum Images 
    {
        Icon
    }

    enum Texts 
    {
        Count
    }


    public new void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        icon = Get<Image>((int)Images.Icon);
        count = Get<Text>((int)Texts.Count);

        UI_EventHandler evt = GetComponent<UI_EventHandler>();
        evt._OnDown += (PointerEventData p) => 
        {
            if (Game.inven_itemInfo[keyId].keyType == Define.KeyType.Empty)
                return;
            
            Game.mouse.CursorType = Define.CursorType.Drag;
            Game.mouse.Set_Mouse_ItemIcon(icon,count);
        };
        evt._OnEnter += (PointerEventData p) =>
        {
            if (Game.mouse.CursorType != Define.CursorType.Drag)
                return;
            Game.changeSpot.index = keyId;
            Game.changeSpot.invenType = Define.InvenType.Inven;
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
        evt._OnExit += (PointerEventData p) =>
        {
            if (Game.mouse.CursorType == Define.CursorType.Drag)
             Game.changeSpot.invenType = Define.InvenType.None;
        };
    }

    public void SetIcon()
    {
        icon.sprite = Game.inven_itemInfo[keyId].icon; 
        count.text = Game.inven_itemInfo[keyId].count.ToString();

        count.gameObject.SetActive(true);
        icon.gameObject.SetActive(true);
        if (Game.inven_itemInfo[keyId].itemType == Define.ItemType.Tool)
          count.gameObject.SetActive(false);
    }

    public void EmptyKey()  //키 비어있게 만들기
    {
        HideIcon();
        Game.inven_itemInfo[keyId].keyType = Define.KeyType.Empty;
        Game.inven_itemInfo[keyId].itemType = Define.ItemType.None;
        Game.inven_itemInfo[keyId].count = 0;
        Game.inven_itemInfo[keyId].id = 0;
    }

    public void HideIcon()
    {
        icon.gameObject.SetActive(false);
        count.gameObject.SetActive(false);
    }

    public void ShowIcon()
    {
        icon.gameObject.SetActive(true);
        count.gameObject.SetActive(true);
    }

    Define.DropType Drop()
    {
        if (Game.changeSpot.invenType == Define.InvenType.Inven)
        {
            if (Game.changeSpot.invenType == Define.InvenType.None)
                return Define.DropType.Return;
            else if (Game.inven_itemInfo[Game.changeSpot.index].id == Game.inven_itemInfo[keyId].id)
                return Define.DropType.Add;
            else if (Game.inven_itemInfo[Game.changeSpot.index].keyType == Define.KeyType.Empty)
                return Define.DropType.Move;
        }
        else
        {
            if (Game.changeSpot.invenType == Define.InvenType.None)
                return Define.DropType.Return;
            else if (Game.hotBar_itemInfo[Game.changeSpot.index].id == Game.inven_itemInfo[keyId].id)
                return Define.DropType.Add;
            else if (Game.hotBar_itemInfo[Game.changeSpot.index].keyType == Define.KeyType.Empty)
                return Define.DropType.Move;
        }

        return Define.DropType.Change;
    }

    public void ChangeItemSpot()
    {
        if (Managers.Game.changeSpot.invenType == Define.InvenType.None)
           ShowIcon();
        //키 자신의 값
        int id = Managers.Game.inven_itemInfo[keyId].id;
        int count = Managers.Game.inven_itemInfo[keyId].count;
        if (Managers.Game.changeSpot.invenType == Define.InvenType.Inven)
        {
            Managers.Game.inven.Set_Inven_Info(keyId, Managers.Game.inven_itemInfo[Managers.Game.changeSpot.index].id, Managers.Game.inven_itemInfo[Managers.Game.changeSpot.index].count);
            Managers.Game.inven.Set_Inven_Info(Managers.Game.changeSpot.index, id, count);
        }
        else
        {
            Managers.Game.inven.Set_Inven_Info(keyId, Managers.Game.hotBar_itemInfo[Managers.Game.changeSpot.index].id, Managers.Game.hotBar_itemInfo[Managers.Game.changeSpot.index].count);
            Managers.Game.hotBar.Set_HotBar_Info(Managers.Game.changeSpot.index, id, count);
        }
    }

    void MoveItemSpot()
    {
        int id = Managers.Game.inven_itemInfo[keyId].id;
        int count = Managers.Game.inven_itemInfo[keyId].count;
        if (Managers.Game.changeSpot.invenType == Define.InvenType.Inven)
        {
            Managers.Game.inven.Set_Inven_Info(keyId, 0, 0);
            Managers.Game.inven.Set_Inven_Info(Managers.Game.changeSpot.index, id, count);
        }
        else
        {
            Managers.Game.inven.Set_Inven_Info(keyId, 0, 0);
            Managers.Game.hotBar.Set_HotBar_Info(Managers.Game.changeSpot.index, id, count);
        }
    }

    void CombineItem()
    {
        int id = Managers.Game.inven_itemInfo[keyId].id;
        int count = Managers.Game.inven_itemInfo[keyId].count;
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
