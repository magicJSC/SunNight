using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rigid;
    public UI_HotBar hotBar;

    private void Start()
    {
       Init();
    }

    public void Init()
    {
        hotBar = Util.FindChild(gameObject, "UI_HotBar").GetComponent<UI_HotBar>();
        if (hotBar == null)
        {
            hotBar = Instantiate(Resources.Load<GameObject>("UI/UI_HotBar/UI_HotBar")).GetComponent<UI_HotBar>();
        }
        rigid = GetComponent<Rigidbody2D>();
        Managers.Input.playTypeAct += ChangePlayT;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector3(x,y,0) * speed;
    }

    void ChangePlayT()
    {
         if (Managers.Game.PlayType == Define.PlayType.Survive)
             Managers.Game.PlayType = Define.PlayType.Building;
         else
             Managers.Game.PlayType= Define.PlayType.Survive;
    }
}
