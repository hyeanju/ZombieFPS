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

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooltime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
            timer = 0;
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
