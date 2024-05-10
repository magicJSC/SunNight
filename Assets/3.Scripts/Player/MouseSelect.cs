using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseSelect : MonoBehaviour
{
    public Tilemap tilemap;

    public GameManager.ItemInfo info;

    private void Start()
    {
        Managers.Game.select = this;
    }

    private void Update()
    {
        Move();
        Use();
    }

    void Move()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
        transform.position = mousePosition;

        if (Input.GetKey(KeyCode.Q))
        {
            Managers.Game.tower.transform.position = transform.position;
        }
    }

    void Use()
    {
        if(info == null)
        {
            info = Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_choice];
        }

        if(info.itemType == Define.ItemType.Building)
            ChangeTile();
    }

    void ChangeTile()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) //그리기
        {
            Vector2 tower = Managers.Game.tower.transform.position; //기지 위치 받아오기
            if (tilemap.HasTile(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0)) || info.count <= 0)
                return;
            tilemap.SetTile(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0), info.tile);
            Managers.Game.hotBar_itemInfo[Managers.Game.hotBar_choice].count--;
            Managers.Game.hotBar.SetKeys();
        }
        else if (Input.GetKey(KeyCode.Mouse1)) //지우기
        {
            Vector2 tower = Managers.Game.tower.transform.position; //기지 위치 받아오기
            tilemap.SetTile(new Vector3Int((int)(transform.position.x - tower.x), (int)(transform.position.y - tower.y), 0), null);
        }
    }
}
