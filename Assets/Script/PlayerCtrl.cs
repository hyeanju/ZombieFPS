using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerCtrl : MonoBehaviour
{
    //이동
    public Camera playerCamera;
    public float walkSpeed = 1.15f;
    public float runSpeed = 4.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public float gravity = 150.0f;

    //체력
    public int hp = 100;
    private int initHp;
    public Image imgHpbar;
    public GameObject damageImage;

    //소리
    public bool soundnum = true;
    public AudioClip WalkSound;
    public AudioClip WalkSound2;
    public AudioClip RunSound;
    public AudioClip RunSound2;
    public AudioClip[] HurtSound = new AudioClip[4];
    private AudioSource Playersource = null;

    //소리재생 쿨타임
    private float Ptimer = 0f;
    private float Pcooltime = 1f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    private bool canMove = true;

    public GameObject goal;

    void Start()
    {
        initHp = hp;
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Playersource = GetComponent<AudioSource>();
        damageImage.SetActive(false);
    }

    void Update()
    {
        Ptimer += Time.deltaTime;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("MouseY") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("MouseX") * lookSpeed, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Pcooltime = 0.3f;
        }

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Pcooltime = 0.8f;
        }
        
        sound();
    }

    public void PlayerAttacted()
    {
        hp -= 10;
        imgHpbar.fillAmount = (float)hp / (float)initHp;
        damageImage.SetActive(true);
        Playersource.clip = HurtSound[Random.Range(0, HurtSound.Length)];
        Playersource.Play();

        Debug.Log("Player HP = " + hp.ToString());
        Invoke("DamageImgDisable", 1);
    }

    public void DamageImgDisable()
    {
        damageImage.SetActive(false);
    }

    public void PlayerDie()
    {
        Debug.Log("Player Die!!");
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject[] Manager = GameObject.FindGameObjectsWithTag("GameManager");

        foreach (GameObject monster in monsters)
        {
            monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        }

        foreach (GameObject manager in Manager)
        {
            manager.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Destination")
        {
            goal.SetActive(true);
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("스페이스 입력");
                SceneManager.LoadScene("Win");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Destination")
        {
            goal.SetActive(false);
        }
    }

    public void sound()
    {
        if(Ptimer >= Pcooltime)
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if(soundnum == true)
                    {
                        Playersource.PlayOneShot(RunSound, 0.9f);
                        soundnum = false;
                    }
                    else
                    {
                        Playersource.PlayOneShot(RunSound2, 0.9f);
                        soundnum = true;
                    }
                }
                if (soundnum == true)
                {
                    Playersource.PlayOneShot(WalkSound, 0.9f);
                    soundnum = false;
                }
                else
                {
                    Playersource.PlayOneShot(WalkSound2, 0.9f);
                    soundnum = true;
                }
            }
            Ptimer = 0f;
        }
    }
}
