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
    public enum Scene
    {
        Unknown,
        GameScene,
        LoginScene


    }
    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum ItemType
    {
        None,           // 없음
        Consumable,     // 소모품
        Tool,           // 장비
        Material,       // 재료
        Food,           // 음식
        Building,        // 건축
        Tower,
        Etc,            // 기타
    }

    public enum KeyType
    {
        Empty,
        Exist
    }
}
