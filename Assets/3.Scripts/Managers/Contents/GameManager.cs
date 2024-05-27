using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

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
        public Define.ItemType itemType;
        public Define.KeyType keyType;
        public TileBase tile;

        public ItemInfo(int id, int count)
        {

            if (id == 0)
            {
                this.id = id;
                keyType = Define.KeyType.Empty;
                return;
            }
            Item i = Resources.Load<GameObject>($"Prefabs/Items/{id}").GetComponent<Item>(); //id�� ���� ������ ����
            if (i == null)
                Debug.Log($"id�� {id}�� �������� �����ϴ�");
            keyType = Define.KeyType.Exist;
            this.id = id;
            itemType = i.itemType;
            icon = i.itemIcon;
            this.count = count;
            if (itemType == Define.ItemType.Building)
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
        if (mouse == null)
        {
            mouse = FindAnyObjectByType<MouseController>();
            if (mouse == null)
            {
                mouse = Instantiate(Resources.Load<GameObject>("Prefabs/Mouse")).GetComponent<MouseController>();
            }
            mouse.Init();
        }
        if(tower == null)
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

    #region �÷��� Ÿ��
    public Define.PlayType PlayType { get { return _playType; }
        set 
        {
            _playType = value;
            
            switch(value)
            {
                case Define.PlayType.Surviver:
                    SetSurviveMode();
                    break;
                case Define.PlayType.Builder:
                    SetBuildingMode();
                    break;
            }
        } 
    }
    Define.PlayType _playType = Define.PlayType.Surviver;

    void SetSurviveMode()
    {
        Managers.Input.mouse0Act = null;
        Managers.Input.mouse1Act = null;
        mouse.gameObject.SetActive(false);
    }

    void SetBuildingMode()
    {
        mouse.gameObject.SetActive(true);
    }
    #endregion

    public Tower tower;
    public PlayerController player;
    public MouseController mouse;

    

    #region �ֹ�
    public UI_HotBar hotBar;

    public int hotBar_choice = 0;
    public int HotBar_Choice { get { return hotBar_choice; } set { hotBar_choice = value; Set_HotBar_Choice(); } }

    //������ ���� ���� �ٸ��� ����
    public void Set_HotBar_Choice()
    {
        //�޶��� ���� �������� �Ѵ�
        mouse.SetInfo();

        if(player.toolParent.transform.childCount != 0)
            Destroy(player.toolParent.transform.GetChild(0).gameObject);
        switch (hotBar_itemInfo[hotBar_choice].itemType)
        {
             case Define.ItemType.Building:
                PlayType = Define.PlayType.Builder;
                mouse.ShowBuildSample();
                break;
             case Define.ItemType.Tower:
                PlayType = Define.PlayType.Builder;
                mouse.ShowTowerSample();
                break;
             case Define.ItemType.Tool:
                PlayType = Define.PlayType.Surviver;
                Instantiate(Resources.Load<GameObject>($"Prefabs/Items/{hotBar_itemInfo[hotBar_choice].id}"),player.toolParent.transform);
                mouse.HideSample();
                break;
             default:
                PlayType = Define.PlayType.Surviver;
                mouse.HideSample();
                break;
        }
    }

    //������ ������ �־���
    public void Add_HotBar_Info(int key_index,int id,int count)
    {
        Item item = Resources.Load<GameObject>($"Prefabs/Items/{id}").GetComponent<Item>(); //id�� ���� ������ ����


        hotBar_itemInfo[key_index].id = id;
        hotBar_itemInfo[key_index].itemType = item.itemType;
        hotBar_itemInfo[key_index].count = count;
        hotBar_itemInfo[key_index].icon = item.itemIcon;
        if (hotBar_itemInfo[key_index].itemType == Define.ItemType.Building)   //�Ǽ� �������� Ÿ���� ���� ������ �ִ´�
            hotBar_itemInfo[key_index].tile = item.tile;
        hotBar_itemInfo[key_index].keyType = Define.KeyType.Exist;

        hotBar.SetKeys();
    }


    public void AddItem(Item item)
    {
        if (item.itemType != Define.ItemType.Tool)   //��� �������϶�
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
            //�߰� ���� ���ߴٸ� ����ִ� ĭ�� �ֱ�
            if (!added)
            {
                Add_HotBar_Info(empty, item.id, hotBar_itemInfo[empty].count + 1);
            }
        }
        else //���� �������϶�
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


    public Define.TimeType timeType = Define.TimeType.Morning;
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
                    timeType = Define.TimeType.Morning;
                else if (hour == 18)
                {
                    timeType = Define.TimeType.Night;

                    if (hotBar_itemInfo[hotBar_itemInfo.Length-1].keyType == Define.KeyType.Exist)
                     mouse.BuildTower(true);
                }
                if (hour == 24)
                    hour = 0;
            }
        }
        else
            curTime += Time.deltaTime;
    }
}
