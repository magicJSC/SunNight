using System.Collections;
using System.Collections.Generic;
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

    public void SetIcon(int id,int count)
    {

        if(id == 0)
        {
            icons.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
            return;
        }
        Item item = Resources.Load<GameObject>($"Prefabs/Items/{id}").GetComponent<Item>();

        icons.gameObject.SetActive(true);
        if(item.type != Define.ItemType.Tool)
             text.gameObject.SetActive(true);
        icons.sprite = item.itemIcon;

        text.text = count.ToString();
    }

    public void Choice()
    {
        choice.gameObject.SetActive(true);
    }

    public void UnChoice()
    {
        choice.gameObject.SetActive(false);
    }
}
