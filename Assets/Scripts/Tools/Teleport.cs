using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string SceneFrom;
    public string SceneTo;
    public Vector3 positionToGo;

    public void TransformToScene()
    {
        EventSystem.CallTeleportToScene(SceneFrom, SceneTo,positionToGo);
    }


    //此处仅为测试！！！！
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           // EventSystem.CallTeleportToScene(SceneFrom, SceneTo, positionToGo);//场景转换
            EventSystem.CallOnCameraShake();//相机抖动
            EventSystem.CallOntransToFire();//火焰状态转换
        }
    }

}
