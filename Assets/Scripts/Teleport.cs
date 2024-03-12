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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventSystem.CallTeleportToScene(SceneFrom, SceneTo, positionToGo);
        }
    }

}
