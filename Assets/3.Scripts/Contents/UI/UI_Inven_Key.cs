using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
            if (Managers.Game.inven_itemInfo[keyId].keyType == Define.KeyType.Empty)
                return;
            
            Managers.Game.mouse.CursorType = Define.CursorType.Drag;
            Managers.Game.mouse.Set_Mouse_ItemIcon(icon,count);
        };
        evt._OnEnter += (PointerEventData p) =>
        {
            if (Managers.Game.mouse.CursorType != Define.CursorType.Drag)
                return;
            Managers.Game.changeSpot.index = keyId;
            Managers.Game.changeSpot.invenType = Define.InvenType.Inven;
        };
        evt._OnUp += (PointerEventData p) => 
        {
            if (Managers.Game.mouse.CursorType != Define.CursorType.Drag)
                return;
            Managers.Game.mouse.CursorType = Define.CursorType.Normal;
            ChangeItemSpot();
        };
    }

    public void SetIcon()
    {
        icon.sprite = Managers.Game.inven_itemInfo[keyId].icon; 
        count.text = Managers.Game.inven_itemInfo[keyId].count.ToString();

        count.gameObject.SetActive(true);
        icon.gameObject.SetActive(true);
        if (Managers.Game.inven_itemInfo[keyId].itemType == Define.ItemType.Tool)
          count.gameObject.SetActive(false);
    }

    public void EmptyKey()  //키 비어있게 만들기
    {
        HideIcon();
        Managers.Game.inven_itemInfo[keyId].keyType = Define.KeyType.Empty;
        Managers.Game.inven_itemInfo[keyId].itemType = Define.ItemType.None;
        Managers.Game.inven_itemInfo[keyId].count = 0;
        Managers.Game.inven_itemInfo[keyId].id = 0;
    }

    public void HideIcon()
    {
        icon.gameObject.SetActive(false);
        count.gameObject.SetActive(false);
    }

    public void ChangeItemSpot()
    {
        //키 자신의 값
        int id = Managers.Game.inven_itemInfo[keyId].id;
        int count = Managers.Game.inven_itemInfo[keyId].count;
        if (Managers.Game.changeSpot.invenType == Define.InvenType.Inven)
        {
            if (Managers.Game.inven_itemInfo[Managers.Game.changeSpot.index].keyType == Define.KeyType.Empty)
            {
                MoveItemSpot();
                return;
            }
            else if(Managers.Game.inven_itemInfo[Managers.Game.changeSpot.index].id == id)
            {
                CombineItem();
                return;
            }
            Managers.Game.inven.Set_Inven_Info(keyId, Managers.Game.inven_itemInfo[Managers.Game.changeSpot.index].id, Managers.Game.inven_itemInfo[Managers.Game.changeSpot.index].count);
            Managers.Game.inven.Set_Inven_Info(Managers.Game.changeSpot.index, id, count);
        }
        else
        {
            if (Managers.Game.hotBar_itemInfo[Managers.Game.changeSpot.index].keyType == Define.KeyType.Empty)
            {
                MoveItemSpot();
                return;
            }
            else if (Managers.Game.hotBar_itemInfo[Managers.Game.changeSpot.index].id == id)
            {
                CombineItem();
                return;
            }
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
