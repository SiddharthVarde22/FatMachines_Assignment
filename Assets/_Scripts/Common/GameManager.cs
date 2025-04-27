using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-20)]
public class GameManager : MonoBehaviour
{
    private static GameManager s_instance;

    [SerializeField]
    Camera m_camera;

    private void Awake()
    {
        s_instance = this;
    }
    private void OnDestroy()
    {
        s_instance = null;
    }

    public static Camera GetCamera { get { return s_instance.m_camera; } }
    public static void SetCameraPositionAndFov(Vector3 a_cameraPosition, float a_fov)
    {
        s_instance.m_camera.transform.position = a_cameraPosition;
        s_instance.m_camera.fieldOfView = a_fov;
    }
}
