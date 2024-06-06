using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : UI_Base
{
    //마우스
    public GameObject _icon;
    public GameObject _count;

    enum GameObjects
    {
        Icon,
        Count
    }

    bool _init = false;

    public override void Init()
    {
        if (_init)
            return;

        _init = true;
        Bind<GameObject>(typeof(GameObjects));
        _icon = Get<GameObject>((int)GameObjects.Icon);
        _count = Get<GameObject>((int)GameObjects.Count);
    }

    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0,0,10);
    }

    public void Set_Mouse_ItemIcon(Image icon,Text count)
    {
        if(_icon == null)
            Init();
        icon.gameObject.SetActive(false);
        count.gameObject.SetActive(false);
        _icon.GetComponent<SpriteRenderer>().sprite = icon.sprite;
        _count.GetComponent<Text>().text = count.text;
    }

    #region 플레이 타입
    Define.CursorType _cursorType = Define.CursorType.Normal;
    public Define.CursorType CursorType
    {
        get { return _cursorType; }
        set
        {
            _cursorType = value;

            switch (value)
            {
                case Define.CursorType.Normal:
                    SetNormalMode();
                    break;
                case Define.CursorType.Builder:
                    SetBuildingMode();
                    break;
                case Define.CursorType.Drag:
                    SetDragMode();
                    break;
            }
        }
    }

    public Texture2D normal;
    public Texture2D drag;
   
    void SetNormalMode()
    {
        gameObject.SetActive(false);
        Cursor.SetCursor(normal, Vector2.zero, CursorMode.Auto);
        Managers.Input.mouse0Act = null;
        Managers.Input.mouse1Act = null;
        Managers.Game.build.gameObject.SetActive(false);
    }

    void SetBuildingMode()
    {
        Managers.Game.build.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void SetDragMode()
    {
        gameObject.SetActive(true);
        Cursor.SetCursor(drag, Vector2.zero,CursorMode.Auto);
        Managers.Input.mouse0Act = null;
        Managers.Input.mouse1Act = null;
        Managers.Game.build.gameObject.SetActive(false);
    }
    #endregion
}
