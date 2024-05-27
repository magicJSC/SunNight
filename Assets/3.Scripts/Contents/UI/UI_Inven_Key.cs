using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Key : UI_Base
{
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
    }

    public void SetIcon(int i)
    {
        icon.sprite = Managers.Game.inven_itemInfo[i].icon; 
        count.text = Managers.Game.inven_itemInfo[i].count.ToString();
    }
}
