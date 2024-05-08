using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HotBar_Key : UI_Base
{
    Image icons;
    Text text;
    enum Images
    {
        item,
    }

    enum Texts
    {
        Count
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        icons = Get<Image>((int)Images.item);
        text = Get<Text>((int)Texts.Count);
    }

    public void SetIcon(int id,int count,Define.ItemType itemType)
    {
        if(id == 0)
        {
            icons.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
            return;
        }
        icons.gameObject.SetActive(true);

        if(itemType != Define.ItemType.Tool)
             text.gameObject.SetActive(true);

        Item data = Resources.Load<Item>($"Prefabs/Items/{id}");
        icons.sprite = data.itemIcon;

        text.text = count.ToString();
    }
}
