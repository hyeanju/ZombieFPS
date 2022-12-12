using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Anim
{
    public AnimationClip M1911_Idle_;
    public AnimationClip M1911_Fire_;
    public AnimationClip M1911_Reload_;
}

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;

    public AudioClip FireSound;
    public AudioClip CatridgeSound;
    public AudioClip ReloadSound;
    private AudioSource Firesource = null;

    public float cooltime = 1f;
    public float Reloadcooltime = 3f;
    private float timer = 0f;
    public int bulletcnt = 7;

    public bool isreload = false;

    private GameUI gameUI;
    private ArmAnimCtrl Armanim;

    public Anim anim;
    public Animation _animation;

    private void Start()
    {
        Firesource = GetComponent<AudioSource>();

        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
        Armanim = GameObject.Find("PlayerArm").GetComponent<ArmAnimCtrl>();

        _animation = GetComponentInChildren<Animation>();
        _animation.clip = anim.M1911_Idle_;
        _animation.Play();
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (bulletcnt == 0)
        {
            reload();
        }

        if(isreload == false)
        {
            //발사 쿨타임
            if (timer >= cooltime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    fire();

                    timer = 0;
                    Armanim.Arm_fire();
                    _animation.CrossFade(anim.M1911_Fire_.name, 0.5f);
                    bulletcnt--;
                    gameUI.DispBullet(1);
                }
                else
                {
                    Armanim.Arm_idle();
                    _animation.CrossFade(anim.M1911_Idle_.name, 0.5f);
                }
            }
        }

    }

    //발사
    void fire()
    {
        CreateBullet();
    }

    //총알생성
    void CreateBullet()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
        Firesource.PlayOneShot(FireSound, 0.9f);
    }

    //재장전
    void reload()
    {
            //장전 쿨타임
        if (timer >= cooltime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(isreload == false)
                {
                    Firesource.PlayOneShot(CatridgeSound, 0.9f);
                }
                isreload = true;
                Armanim.Arm_reload();
                _animation.CrossFade(anim.M1911_Reload_.name, 0.5f);
                
            }
            if(isreload==true)
            {
                if (timer >= Reloadcooltime)
                {
                    Firesource.PlayOneShot(ReloadSound, 0.9f);
                    timer = 0;
                    bulletcnt = 7;
                    gameUI.DispBullet(-7);
                    isreload = false;
                }
            }
        }
    }
}
