using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TarodevController;

public static class EventSystem
{
    //场景转换事件
    public static event Action<string, string, Vector3> TeleportToScene;

    public static void CallTeleportToScene(string from, string to, Vector3 positionToGo)
    {
        TeleportToScene?.Invoke(from, to, positionToGo);
    }

    //相机抖动事件
    public static event Action OnCameraShake;

    public static void CallOnCameraShake()
    {
        OnCameraShake?.Invoke();
    }

    //角色状态转换，木到火

    public static event Action OnTransToFire;

    public static void CallOntransToFire()
    {
        if(PlayerController.Instance.curState==PlayerStats.fire)
        {
            return;
        }
        OnTransToFire?.Invoke();
        PlayerController.Instance.curState = PlayerStats.fire;
    }

    //火到木
    public static event Action OnTransToTree;

    public static void CallOntransToTree()
    {
        if (PlayerController.Instance.curState == PlayerStats.tree)
        {
            return;
        }
        OnTransToTree?.Invoke();
        PlayerController.Instance.curState = PlayerStats.tree;
        
    }
}
