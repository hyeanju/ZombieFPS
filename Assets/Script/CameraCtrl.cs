using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    private float rotSpeed = 5f;

    private float MinX = -50f;
    private float MaxX = 50;
    private float rotX = 0f;
    private float rotY = 0f;

    public void UpdateRotate(float mouseX, float mouseY)
    {
        rotY += mouseX * rotSpeed;
        rotX -= mouseY * rotSpeed;

        rotX = ClampAngle(rotX, MinX, MaxX);
        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if(angle < -360)
        {
            angle += 360;
        }
        if(angle > 360)
        {
            angle -= 360;
        }

        return Mathf.Clamp(angle, min, max);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
