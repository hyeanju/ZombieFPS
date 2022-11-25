using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private Transform tr;
    public float moveSpeed = 10.0f;
    public float rotSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Debug.Log("h=" + ((int)h).ToString());
        Debug.Log("v=" + ((int)v).ToString());

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //tr.Translate(Vector3.right * moveSpeed * h * Time.deltaTime, Space.Self);
        tr.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);

        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

    }
}
