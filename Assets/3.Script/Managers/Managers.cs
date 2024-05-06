using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance { get { if (instance == null) Debug.Log("Manager스크립트가 존재하지 않습니다"); return instance;  } }
    static Managers instance;

    GameManager _game = new GameManager();
    public GameManager Game { get { return instance._game; } }

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        if (instance == null)
        {
            instance = GetComponent<Managers>();
            DontDestroyOnLoad(gameObject);
        }
    }
}
