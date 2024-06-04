using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rigid;
    public UI_HotBar hotBar;

    public GameObject toolParent;

    public void Init()
    {
        toolParent = Util.FindChild(gameObject, "Tool");
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(!Managers.Inven.inven.gameObject.activeSelf)
                Managers.Inven.inven.gameObject.SetActive(true);
            else
                Managers.Inven.inven.gameObject.SetActive(false);
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector3(x,y,0) * speed;
    }
}
