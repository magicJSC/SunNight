using Google.Protobuf.WellKnownTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public void Init()
    {
        if(mouse == null)
        {
            mouse = FindAnyObjectByType<MouseController>();
            if(mouse == null)
            {
                mouse = Instantiate(Resources.Load<GameObject>("Prefabs/Mouse")).GetComponent<MouseController>();
            }
        }
        if(hotBar == null)
        {
            hotBar = FindAnyObjectByType<UI_HotBar>();
            if (hotBar == null)
            {
                mouse = Instantiate(Resources.Load<GameObject>("UI/UI_HotBar/UI_HotBar")).GetComponent<MouseController>();
            }
        }
        Set_HotBar_Choice();
    }


    public Tower tower;

    public MouseController mouse;

    #region 핫바
    public UI_HotBar hotBar;

    public int hotBar_choice = 0;
    public int HotBar_Choice { get { return hotBar_choice; } set { hotBar_choice = value; Set_HotBar_Choice(); } }

    public HotBarInfo[] hotBar_itemInfo = new HotBarInfo[5];
   
    public class HotBarInfo
    {
        public int id;
        public int count;
        public Sprite icon;
        public Define.ItemType itemType;
        public Define.KeyType keyType;
        public TileBase tile;

        public HotBarInfo(int id,int count)
        {

            if (id == 0)
            {
                this.id = id;
                keyType = Define.KeyType.Empty;
                return;
            }
            Item i = Resources.Load<GameObject>($"Prefabs/Items/{id}").GetComponent<Item>(); //id에 따른 아이템 정보
            if (i == null)
                Debug.Log($"id가 {id}인 아이템은 없습니다");
            keyType = Define.KeyType.Exist;
            this.id = id;
            itemType = i.itemType;
            icon = i.itemIcon;
            this.count = count;
            if (itemType == Define.ItemType.Building)
                tile = i.tile;
        }
    }

    //선택한 값에 따라 다르게 실행
    public void Set_HotBar_Choice()
    {
        if (mouse.info == null)
            return;

        //달라진 값을 가져오게 한다
        mouse.SetInfo();

        //아이템이 설치 아이템일때
        if (hotBar_itemInfo[HotBar_Choice].itemType == Define.ItemType.Building && hotBar_itemInfo[HotBar_Choice].keyType == Define.KeyType.Exist)
        {
            mouse.sample.SetActive(true);
            mouse.sample.GetComponent<SpriteRenderer>().sprite = hotBar_itemInfo[HotBar_Choice].icon;
        }
        else
            mouse.sample.SetActive(false);

        //기지를 소장하고 있지만 선택하고 있지 않을때
        if (hotBar_itemInfo[hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist && HotBar_Choice == hotBar_itemInfo.Length - 1)
        {
            tower.gameObject.SetActive(true);
            tower.GetComponent<SpriteRenderer>().color = new Color32(225, 225, 225, 120);
            Managers.Game.tilemap.color = new Color32(225, 225, 225, 120);
        }
        else if (hotBar_itemInfo[hotBar_itemInfo.Length - 1].keyType == Define.KeyType.Exist && HotBar_Choice != hotBar_itemInfo.Length - 1)
        {
            tower.gameObject.SetActive(false);
        }
    }

    //아이템 정보를 넣어줌
    public void Add_HotBar_Info(int key_index,int id,int count)
    {
        Item item = Resources.Load<GameObject>($"Prefabs/Items/{id}").GetComponent<Item>(); //id에 따른 아이템 정보


        hotBar_itemInfo[key_index].id = id;
        hotBar_itemInfo[key_index].itemType = item.itemType;
        hotBar_itemInfo[key_index].count = count;
        hotBar_itemInfo[key_index].icon = item.itemIcon;
        if (hotBar_itemInfo[key_index].itemType == Define.ItemType.Building)   //건설 아이템은 타일을 따로 가지고 있는다
            hotBar_itemInfo[key_index].tile = item.tile;
        hotBar_itemInfo[key_index].keyType = Define.KeyType.Exist;

        hotBar.SetKeys();
    }


    public void AddItem(Item item)
    {
        if (item.itemType != Define.ItemType.Tool)   //재료 아이템일때
        {
            bool added = false;
            int empty = -1;
            for (int i = 0; i < hotBar_itemInfo.Length - 1; i++)
            {
                if (item.id == hotBar_itemInfo[i].id && hotBar_itemInfo[i].count < 99)
                {
                    Add_HotBar_Info(i, item.id, hotBar_itemInfo[i].count + 1);
                    added = true;
                    break;
                }
                else
                {
                    if (empty == -1 && Define.KeyType.Empty == hotBar_itemInfo[i].keyType)
                        empty = i;
                }
            }
            //추가 하지 못했다면 비어있는 칸에 넣기
            if (!added)
            {
                Add_HotBar_Info(empty, item.id, hotBar_itemInfo[empty].count + 1);
            }
        }
        else //도구 아이템일때
        {
            for (int i = 0; i < hotBar_itemInfo.Length - 1; i++)
            {
                if (Define.KeyType.Empty == hotBar_itemInfo[i].keyType)
                {
                    Add_HotBar_Info(i, item.id, 1);
                    break;
                }
            }
        }
    }
    #endregion

    public Tilemap tilemap;

    //저녁과 아침이 목표 우선순위를 다르게 하기
    public void SetTarget()
    {

    }
}
