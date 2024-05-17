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
        hotBar = Util.FindChild(gameObject,"UI_HotBar").GetComponent<UI_HotBar>();
        if(hotBar == null)
        {
            hotBar = Instantiate(Resources.Load<GameObject>("UI/UI_HotBar/UI_HotBar")).GetComponent<UI_HotBar>();
        }
        rigid = GetComponent<Rigidbody2D>();
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
}
