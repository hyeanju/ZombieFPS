using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animctrl : MonoBehaviour
{
    public Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            Anim.SetTrigger("A");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Anim.SetTrigger("B");
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Anim.SetTrigger("C");
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Anim.SetTrigger("D");
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            Anim.SetTrigger("E");
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Anim.SetTrigger("F");
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            Anim.SetTrigger("G");
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            Anim.SetTrigger("H");
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            Anim.SetTrigger("I");
        }
    }
}
