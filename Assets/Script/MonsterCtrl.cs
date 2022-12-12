using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    public enum MonsterState { idle, trace, attack, die };
    public MonsterState monsterState = MonsterState.idle;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    public float traceDist = 10.0f;
    public float attackDist = 3.0f;
    private bool isDie = false;

    public int hp = 100;
    private float attackTime = 1.0f;
    private float timer;
    private PlayerCtrl playerCtrl;

    //¿Àµð¿À
    public AudioClip[] Attack = new AudioClip[4];
    public AudioClip[] Idle = new AudioClip[4];
    public AudioClip[] Trace = new AudioClip[4];
    public AudioClip[] Die = new AudioClip[3];

    private AudioSource Monstersource = null;

    private float SndTimer;
    private float Sndcooltime = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        IdleSound();
        monsterTr = this.gameObject.GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponent<Animator>();
        playerCtrl = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();
        Monstersource = GetComponent<AudioSource>();

        StartCoroutine(this.CheckMonsterState());
        StartCoroutine(this.MonsterAction());
    }

    // Update is called once per frame
    void Update()
    {
        nvAgent.destination = playerTr.position;
        timer += Time.deltaTime;
        SndTimer += Time.deltaTime;
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);
            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if (dist <= attackDist)
            {
                monsterState = MonsterState.attack;
                Sndcooltime = 1.0f;
                AttackSound();
            }
            else if (dist <= traceDist)
            {
                Sndcooltime = 1.5f;
                monsterState = MonsterState.trace;
                TraceSound();
            }
            else
            {
                Sndcooltime = 2.0f;
                monsterState = MonsterState.idle;
                IdleSound();
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.isStopped = true;
                    animator.SetBool("IsTrace", false);
                    break;
                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false;
                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", true);
                    break;
                case MonsterState.attack:
                    nvAgent.isStopped = true;
                    animator.SetBool("IsAttack", true);
                    if (timer >= attackTime)
                    {
                        playerCtrl.PlayerAttacted();
                        timer = 0;
                    }
                    if (playerCtrl.hp <= 0)
                    {
                        playerCtrl.PlayerDie();
                        OnPlayerDie();
                    }
                    break;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;

            if (hp <= 0)
            {
                MonsterDie();
            }
            else
            {
                animator.SetTrigger("IsHit");
            }
        }
    }

    void OnPlayerDie()
    {
        StopAllCoroutines();
        nvAgent.isStopped = true;
    }

    public void MonsterDie()
    {
        StopAllCoroutines();

        isDie = true;
        monsterState = MonsterState.die;
        nvAgent.isStopped = true;
        animator.SetTrigger("IsDie");
        DieSound();

        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;

        foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }
    }

    public void IdleSound()
    {
        if (SndTimer >= Sndcooltime)
        { 
            Monstersource.clip = Idle[Random.Range(0, Idle.Length)];
            Monstersource.Play();

            SndTimer = 0;
        }
    }

    public void AttackSound()
    {
        if (SndTimer >= Sndcooltime)
        {
            Monstersource.clip = Trace[Random.Range(0, Trace.Length)];
            Monstersource.Play();

            SndTimer = 0;
        }
    }

    public void TraceSound()
    {
        if (SndTimer >= Sndcooltime)
        {
            Monstersource.clip = Attack[Random.Range(0, Attack.Length)];
            Monstersource.Play();

            SndTimer = 0;
        }
    }

    public void DieSound()
    {
        Monstersource.clip = Die[Random.Range(0, Die.Length)];
        Monstersource.PlayOneShot(Monstersource.clip);
    }
}
