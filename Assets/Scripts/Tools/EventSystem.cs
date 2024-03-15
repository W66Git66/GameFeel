using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TarodevController;

public static class EventSystem
{
    //����ת���¼�
    public static event Action<string, string, Vector3> TeleportToScene;

    public static void CallTeleportToScene(string from, string to, Vector3 positionToGo)
    {
        TeleportToScene?.Invoke(from, to, positionToGo);
    }

    //��������¼�
    public static event Action OnCameraShake;

    public static void CallOnCameraShake()
    {
        OnCameraShake?.Invoke();
    }

    //��ɫ״̬ת����ľ����

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

    //��ľ
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
