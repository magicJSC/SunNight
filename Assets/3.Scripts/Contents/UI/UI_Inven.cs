using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven : UI_Base
{
    List<GameObject> keys = new List<GameObject>();

    GameObject back;
    GameObject grid;
    Vector3 startPos;

    enum GameObjects 
    {
        Background,
        Grid
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        back = Get<GameObject>((int)GameObjects.Background);
        grid = Get<GameObject>((int)GameObjects.Grid);

        UI_EventHandler evt = back.GetComponent<UI_EventHandler>();
        evt._OnDrag += (PointerEventData p) => { back.transform.position = new Vector3(p.position.x + startPos.x, p.position.y + startPos.y);  };
        evt._OnDown += (PointerEventData p) => { startPos = new Vector3(back.transform.position.x - p.position.x, back.transform.position.y - p.position.y); };

        GetData();
        MakeKeys();
    }

    void GetData()
    {
        for (int i = 0; i < Managers.Game.inven_itemInfo.Length; i++)
        {
            Managers.Game.inven_itemInfo[i] = new GameManager.ItemInfo(1,10);
        }
    }

    void MakeKeys()
    {
        for (int i = 0; i < Managers.Game.inven_itemInfo.Length; i++)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("UI/UI_Inven/UI_Inven_Key"), grid.transform);
            keys.Add(go);
            go.GetComponent<UI_Inven_Key>().Init();
            go.GetComponent<UI_Inven_Key>().SetIcon(i);
        }
    }
}
