using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraController : MonoBehaviour
{
    private CinemachineConfiner2D _confiner2D;
    public CinemachineImpulseSource _impulseSource;

    private void Awake()
    {
        _confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void Start()
    {
        GetNewCameraBounds();
    }

    private void OnEnable()
    {
        EventSystem.OnCameraShake += CameraShake;
    }

    private void OnDisable()
    {
        EventSystem.OnCameraShake -= CameraShake;
    }

    //获取场景里的相机碰撞边界
    private void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if(obj==null)
        {
            return;
        }
        _confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();

        _confiner2D.InvalidateCache();
    }

    private void CameraShake()
    {
        _impulseSource.GenerateImpulse();
    }
}
