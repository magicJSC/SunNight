using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance;
    public static Managers Instance { get { Init(); return instance; } }

    #region Contents
    GameManager _game = new GameManager();
    ObjectMananger _obj = new ObjectMananger();
    NetworkManager _network = new NetworkManager();

    public static ObjectMananger Object { get { return Instance._obj; } }
    public static NetworkManager Network { get { return Instance._network; } }
    public static GameManager Game { get { return Instance._game; } }
    #endregion

    void Awake()
    {
        Init();
    }

    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();

            Network.Init();
        }
    }
}
