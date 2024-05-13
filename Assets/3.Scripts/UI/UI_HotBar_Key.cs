using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_HotBar_Key : UI_Base
{
    Image icons;
    Text text;
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

    public void Init_Key()
    {
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        icons = Get<Image>((int)Images.item);
        text = Get<Text>((int)Texts.Count);
        choice = Get<GameObject>((int)GameObjects.Choice);
    }

    public override void Init()
    {

    }

    public void SetIcon(int index)
    {
        GameManager.HotBarInfo hotBar = Managers.Game.hotBar_itemInfo[index];
        if(hotBar.itemType == Define.ItemType.Tower)
        {
            SetTowerIcon();
            return;
        }

        if (hotBar.keyType == Define.KeyType.Empty)
        {
            EmptyKey(index);
            return;
        }
        icons.gameObject.SetActive(true);
        if(hotBar.itemType != Define.ItemType.Tool && hotBar.itemType != Define.ItemType.Tower)
             text.gameObject.SetActive(true);
        icons.sprite = hotBar.icon;

        text.text = hotBar.count.ToString();
    }

    public void Choice()
    {
        choice.gameObject.SetActive(true);
    }

    public void UnChoice()
    {
        choice.gameObject.SetActive(false);
    }

    public void EmptyKey(int index)  //Ű ����ְ� �����
    {
        icons.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        Managers.Game.hotBar_itemInfo[index].keyType = Define.KeyType.Empty;
    }

    public void SetTowerIcon()
    {
        if (Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Empty)
        {
            icons.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
            return;
        }
        icons.gameObject.SetActive(true);
        icons.sprite = Managers.Game.tower.GetComponent<SpriteRenderer>().sprite;
    }
}
