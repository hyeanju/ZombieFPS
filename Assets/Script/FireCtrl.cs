using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;

    public AudioClip fireSfx;
    private AudioSource source = null;

    public float cooltime = 5f;
    private float timer = 0;

    public int bulletcnt = 9;

    private GameUI gameUI;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (bulletcnt > 0)
        {
            //발사 쿨타임
            if (timer >= cooltime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Fire();
                    timer = 0;

                    bulletcnt--;
                    gameUI.DispBullet(1);
                }
            }
        }
        //재장전
        if (bulletcnt == 0)
        {
            //장전 쿨타임
            if (timer >= cooltime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    timer = 0;

                    bulletcnt = 9;
                    gameUI.DispBullet(-9);
                }
            }
        }
    }

    void Fire()
    {
        CreateBullet();
    }

    void CreateBullet()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
        source.PlayOneShot(fireSfx, 0.9f);
    }
}
