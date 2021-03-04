using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera[] cameras;
    private int activeCamera = 0;

    private void Start()
    {
        InitCameras();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            ChangeCamera(0);
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            ChangeCamera(1);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            ChangeCamera(2);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            ChangeCamera(3);
        }
    }

    private void InitCameras()
    {
        for (int i = 1; i < cameras.Length; i++)
        {
            if (cameras[i] != null)
                cameras[i].enabled = false;
        }
        cameras[0].enabled = true;
    }

    private void ChangeCamera(int camera)
    {
        cameras[activeCamera].enabled = false;
        activeCamera = camera;
        cameras[activeCamera].enabled = true;
    }
}
