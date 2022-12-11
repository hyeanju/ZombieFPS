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
    private AudioSource source = null;

    public float cooltime = 1f;
    private float timer = 0;

    public bool isreload = false;
    public int bulletcnt = 9;

    private GameUI gameUI;

    public Anim anim;
    public Animation _animation;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();

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
            isreload = false;
        }

        if (isreload == false)
        {
            //발사 쿨타임
            if (timer >= cooltime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    fire();
                    
                    timer = 0;
                    _animation.CrossFade(anim.M1911_Fire_.name, 0.5f);
                    bulletcnt--;
                    gameUI.DispBullet(1);
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
                isreload = true;
                _animation.CrossFade(anim.M1911_Reload_.name, 0.5f);

                timer = 0;
                bulletcnt = 9;
                gameUI.DispBullet(-9);
            }
        }
    }
}
