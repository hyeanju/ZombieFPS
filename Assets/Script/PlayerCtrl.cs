using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private CameraCtrl cameractrl;
    private PlayerMove move;

    float mousex = 0f;
    float mousey = 0f;
    float h = 0;
    float v = 0;


    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cameractrl = GetComponent<CameraCtrl>();
        move = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotate();
        UpdateMove();
    }

    void UpdateRotate()
    {
        mousex = Input.GetAxis("MouseX");
        mousey = Input.GetAxis("MouseY");

        cameractrl.UpdateRotate(mousex, mousey);
    }

    void UpdateMove()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        
    }
}
