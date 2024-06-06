using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action mouse0Act = null;
    public Action mouse1Act = null;
    public Action playTypeAct = null;

    public void UpdateInput()
    {
        if (!Input.anyKey)
            return;

        if(Input.GetKeyDown(KeyCode.Mouse0) && mouse0Act != null)
            mouse0Act.Invoke();
        if (Input.GetKeyDown(KeyCode.Mouse1) && mouse1Act != null)
            mouse1Act.Invoke();
        if(Input.GetKeyDown(KeyCode.Q) && playTypeAct != null)
            playTypeAct.Invoke();
    }
}
