using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventSystem 
{
    //场景转换事件
    public static event Action<string,string,Vector3> TeleportToScene;

    public static void CallTeleportToScene(string from, string to, Vector3 positionToGo)
    {
        TeleportToScene?.Invoke(from, to, positionToGo);
    }

}
