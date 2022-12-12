using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public int damage = 20;
    public float speed = 1000.0f;

    MonsterCtrl monsterCtrl;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        monsterCtrl = GameObject.FindWithTag("Monster").GetComponent<MonsterCtrl>();

        Invoke("DestroyBullet", 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyBullet();

        if (collision.gameObject.tag == "Head")
        {
            Debug.Log("Çìµå¼¦");
            monsterCtrl.hp = 0;
            monsterCtrl.MonsterDie();
        }
    }
}