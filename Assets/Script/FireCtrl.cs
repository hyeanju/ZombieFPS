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

    public AudioClip fireSfx;
    public AudioClip catridgeload;
    public AudioClip reloading;
    private AudioSource source = null;

    public float cooltime = 1f;
    private float timer = 0f;

    public float Reloadcooltime = 3f;

    public bool isreload = false;
    public int bulletcnt = 7;

    private GameUI gameUI;
    private ArmAnimCtrl Armanim;

    public Anim anim;
    public Animation _animation;

    private void Start()
    {
        source = GetComponent<AudioSource>();

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
        source.PlayOneShot(fireSfx, 0.9f);
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
                    source.PlayOneShot(catridgeload, 0.9f);
                }
                isreload = true;
                Armanim.Arm_reload();
                _animation.CrossFade(anim.M1911_Reload_.name, 0.5f);
                
            }
            if(isreload==true)
            {
                if (timer >= Reloadcooltime)
                {
                    source.PlayOneShot(reloading, 0.9f);
                    timer = 0;
                    bulletcnt = 7;
                    gameUI.DispBullet(-7);
                    isreload = false;
                }
            }
        }
    }
}
