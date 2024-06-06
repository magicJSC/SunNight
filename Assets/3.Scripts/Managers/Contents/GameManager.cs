using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Define;

public class GameManager : MonoBehaviour
{
    #region ���� ������
    public ItemInfo[] hotBar_itemInfo = new ItemInfo[5];
    public ItemInfo[] inven_itemInfo = new ItemInfo[24];

    public class ItemInfo
    {
        public int id;
        public int count;
        public Sprite icon;
        public ItemType itemType;
        public KeyType keyType;
        public TileBase tile;

        public ItemInfo(int id, int count)
        {

            if (id == 0)
            {
                this.id = id;
                keyType = KeyType.Empty;
                return;
            }
            Item i = Resources.Load<GameObject>($"Prefabs/Items/{id}").GetComponent<Item>(); //id�� ���� ������ ����
            if (i == null)
                Debug.Log($"id�� {id}�� �������� �����ϴ�");
            keyType = KeyType.Exist;
            this.id = id;
            itemType = i.itemType;
            icon = i.itemIcon;
            this.count = count;
            if (itemType == ItemType.Building)
                tile = i.tile;
        }
    }

    public Dictionary<Vector2,int> tileData = new Dictionary<Vector2, int>();
    #endregion

    public void Init()
    {
        if (hotBar == null)
        {
            hotBar = FindAnyObjectByType<UI_HotBar>();
            if (hotBar == null)
            {
                hotBar = Instantiate(Resources.Load<GameObject>("UI/UI_HotBar/UI_HotBar")).GetComponent<UI_HotBar>();
            }
            hotBar.Init();
        }
        if (inven == null)
        {
            inven = FindAnyObjectByType<UI_Inven>();
            if (inven == null)
            {
                inven = Instantiate(Resources.Load<GameObject>("UI/UI_Inven/UI_Inven")).GetComponent<UI_Inven>();
            }
            inven.Init();
        }
        if (mouse == null)
        {
            mouse = FindAnyObjectByType<MouseController>();
            if (mouse == null)
            {
                mouse = Instantiate(Resources.Load<GameObject>("Prefabs/Mouse")).GetComponent<MouseController>();
            }
        }
        if (build == null)
        {
            build = FindAnyObjectByType<BuildController>();
            if (build == null)
            {
                build = Instantiate(Resources.Load<GameObject>("Prefabs/Builder")).GetComponent<BuildController>();
            }
            build.Init();
        }
        if (tower == null)
        {

            tower = FindAnyObjectByType<Tower>();
            if (tower == null)
            {
                tower = Instantiate(Resources.Load<GameObject>("Prefabs/Tower")).GetComponent<Tower>();

            }
            tower.Init();
        }
        if (player == null)
        {
            player = FindAnyObjectByType<PlayerController>();
            if (player == null)
            {
                player = Instantiate(Resources.Load<GameObject>("Prefabs/Player")).GetComponent<PlayerController>();
            }
            player.Init();
        }
        if (lights == null)
        {
            lights = FindAnyObjectByType<LightController>();
        }

        Set_HotBar_Choice();
    }

   

    public void OnUpdate()
    {
        SetTime();
    }

    public TowerController tower;
    public PlayerController player;
    public BuildController build;
    public MouseController mouse;
    public GridManager grid;


    #region �κ��丮
    public UI_HotBar hotBar;

    public int hotBar_choice = 0;
    public int HotBar_Choice { get { return hotBar_choice; } set { hotBar_choice = value; Set_HotBar_Choice(); } }

    //������ ���� ���� �ٸ��� ����
    public void Set_HotBar_Choice()
    {
        //�޶��� ���� �������� �Ѵ�
        build.SetInfo();
        if (player.toolParent.transform.childCount != 0)
            Destroy(player.toolParent.transform.GetChild(0).gameObject);
        switch (hotBar_itemInfo[hotBar_choice].itemType)
        {
             case ItemType.Building:
                mouse.CursorType = CursorType.Builder;
                build.ShowBuildSample();
                break;
             case ItemType.Tower:
                mouse.CursorType = CursorType.Builder;
                build.ShowTowerSample();
                break;
             case ItemType.Tool:
                mouse.CursorType =CursorType.Normal;
                Instantiate(Resources.Load<GameObject>($"Prefabs/Items/{hotBar_itemInfo[hotBar_choice].id}"),player.toolParent.transform);
                build.HideSample();
                break;
             default:
                mouse.CursorType = CursorType.Normal;
                build.HideSample();
                break;
        }
    }

   

    public bool AddItem(int id,int count = 1)
    {
        Item item = Resources.Load<Item>($"Prefabs/Items/{id}");
        return Add_Item_Inven(item,count);
    }

    bool Add_Item_HotBar(Item item, int count = 1)
    {
        if (item.itemType != ItemType.Tool)   //��� �������϶�
        {
            int empty = -1;
            for (int i = 0; i < hotBar_itemInfo.Length - 1; i++)
            {
                if (item.id == hotBar_itemInfo[i].id && hotBar_itemInfo[i].count < 99)
                {
                    hotBar.Set_HotBar_Info(i, item.id, hotBar_itemInfo[i].count + count);
                    return true;
                }
                else
                {
                    if (empty == -1 && KeyType.Empty == hotBar_itemInfo[i].keyType)
                        empty = i;
                }
            }
            //�߰� ���� ���ߴٸ� ����ִ� ĭ�� �ֱ�
            if (empty != -1)
            {
                hotBar.Set_HotBar_Info(empty, item.id, hotBar_itemInfo[empty].count + count);
                return true;
            }

            return false;
        }
        else //���� �������϶�
        {
            for (int i = 0; i < hotBar_itemInfo.Length - 1; i++)
            {
                if (KeyType.Empty == hotBar_itemInfo[i].keyType)
                {
                    hotBar.Set_HotBar_Info(i, item.id, 1);
                    return true;
                }
            }
            return false;
        }
    }

    bool Add_Item_Inven(Item item, int count = 1)
    {
        if (item.itemType != ItemType.Tool)   //��� �������϶�
        {
            int empty = -1;
            for (int i = 0; i < inven_itemInfo.Length - 1; i++)
            {
                if (item.id == inven_itemInfo[i].id && inven_itemInfo[i].count < 99)
                {
                    inven.Set_Inven_Info(i, item.id, inven_itemInfo[i].count + count);
                    return true;
                }
                else
                {
                    if (empty == -1 && KeyType.Empty == inven_itemInfo[i].keyType)
                        empty = i;
                }
            }
            //�߰� ���� ���ߴٸ� ����ִ� ĭ�� �ֱ�
            if (empty != -1)
            {
                inven.Set_Inven_Info(empty, item.id, inven_itemInfo[empty].count + count);
                return true;
            }

            //�߰� ���� ���ߴµ� ����ִ� ĭ�� ������
            return Add_Item_HotBar(item, count);
        }
        else //���� �������϶�
        {
            for (int i = 0; i < inven_itemInfo.Length - 1; i++)
            {
                if (KeyType.Empty == inven_itemInfo[i].keyType)
                {
                    inven.Set_Inven_Info(i, item.id, 1);
                    return true;
                }
            }

            return Add_Item_HotBar(item,count);
        }
    }
    #endregion

    public UI_Inven inven;
    public (int index, InvenType invenType) changeSpot; //�ι�°�� �޾ƿ��� ����

   
    public TimeType timeType = TimeType.Morning;
    public LightController lights;
    public float curTime = 0;
    public float hour = 6;
    public float minute = 0;

    void SetTime()
    {
        if (curTime >= 1)
        {
            curTime = 0;
            lights.SetLight();

            minute++;
            if (minute == 60)
            {
                minute = 0;
                hour++;
                if (hour == 6)
                    timeType = TimeType.Morning;
                else if (hour == 18)
                {
                    timeType = TimeType.Night;

                    if (hotBar_itemInfo[hotBar_itemInfo.Length-1].keyType == KeyType.Exist)
                     build.BuildTower(true);
                }
                if (hour == 24)
                    hour = 0;
            }
        }
        else
            curTime += Time.deltaTime;
    }
}
