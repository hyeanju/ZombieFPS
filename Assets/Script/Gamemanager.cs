using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public Camera playerCamera;
    public float rotspeed = 15f;
    float rotation = 0f;
    private float Timer;
    private float rotatetime = 3f;

    private bool isdie = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isdie == true)
        {
            camerarotate();
            Timer += Time.deltaTime;
        }

        if (Timer >= rotatetime)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void OnPlayerDie()
    {
        isdie = true;
    }

    public void camerarotate()
    {
        playerCamera.transform.Rotate(rotspeed*Time.deltaTime*-1, 0, 0);
    }
}
