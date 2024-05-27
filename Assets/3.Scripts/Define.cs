using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }

    public enum SceneType
    {
        None,
        GameScene
    }


    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum ItemType
    {
        None,           // ����
        Consumable,     // �Ҹ�ǰ
        Tool,           // ���
        Material,       // ���
        Food,           // ����
        Building,        // ����
        Tower,
        Etc,            // ��Ÿ
    }

    public enum KeyType
    {
        Empty,
        Exist
    }

    public enum State
    {
        Idle,
        Move,
        Attack,
        Die
    }

    public enum PlayType 
    {
       Surviver,
        Builder
    }

    public enum TimeType
    {
        Morning,
        Night
    }
}
