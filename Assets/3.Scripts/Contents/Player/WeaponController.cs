using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    Vector2 size;
    [SerializeField]
    int _damage;
    LayerMask monsterLayer;
    Vector3 point;
    float angle;
    bool isAttacking;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        monsterLayer = 7;
    }
    void Update()
    {
        if (isAttacking)
            return;

        Rotate();
        Attack();
    }

    void Rotate()
    {
        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(point.y - transform.position.y, point.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.AngleAxis(angle, transform.forward),0.4f);
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.Play("Attack");
            isAttacking = true;
        }
    }

    void EndAtk()
    {
        isAttacking = false;
    }

    void Check()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.GetChild(0).position + (point - transform.position).normalized, size, angle);
        foreach(Collider2D col in cols)
        {
            if(col.gameObject.layer == monsterLayer)
            {
                MonsterStat stat = col.GetComponent<MonsterStat>();
                stat.Hp = Util.GetTotalHp(stat.Hp,stat.Def,_damage);
                if(stat.Hp <= 0)
                {
                    Destroy(col.gameObject);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
       Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.GetChild(0).position, size);
    }
}
