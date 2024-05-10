using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private void Start()
    {
        Managers.Game.tower = this;
    }

}
