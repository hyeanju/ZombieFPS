using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Camera main;

    private float h = 0.0f;
    private float v = 0.0f;
    private Transform tr;
    public float moveSpeed = 10f;
    private CameraCtrl cameractrl;
    float mousex = 0f;
    float mousey = 0f;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cameractrl = GetComponent<CameraCtrl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        UpdateRotate();
    }

    void PlayerMove()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);
    }

    void UpdateRotate()
    {
        mousex = Input.GetAxis("MouseX");
        mousey = Input.GetAxis("MouseY");

        cameractrl.UpdateRotate(mousex, mousey);
    }
}
