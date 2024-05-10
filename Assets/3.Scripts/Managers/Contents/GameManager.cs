using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Tower tower;

    public MouseSelect select;

    #region ÇÖ¹Ù
    public UI_HotBar hotBar;

    public int hotBar_choice = 0;
    public int HotBar_Choice { get { return hotBar_choice; } set { hotBar_choice = value; select.info = hotBar_itemInfo[value]; } }

    public ItemInfo[] hotBar_itemInfo;
   
    public class ItemInfo
    {
        public int id;
        public int count;
        public Define.ItemType itemType;
        public TileBase tile;
        public ItemInfo(int id, int cnt)
        {
            if (id == 0)
            {
                this.id = id;
                return;
            }

            Item i = Resources.Load<GameObject>($"Prefabs/Items/{id}").GetComponent<Item>();
            this.id = id;
            itemType = i.type;
            count = cnt;
            if (itemType == Define.ItemType.Building)
                tile = i.tile;

    }
    }
    #endregion
}
