using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Arm_Anim
{
    public AnimationClip Arm_M1911_Idle_;
    public AnimationClip Arm_M1911_Fire_;
    public AnimationClip Arm_M1911_Reload_;
}

public class ArmAnimCtrl : MonoBehaviour
{

    public Arm_Anim Arm_anim;
    public Animation Arm_animation;

    // Start is called before the first frame update
    void Start()
    {
        Arm_animation = GetComponentInChildren<Animation>();
        Arm_animation.clip = Arm_anim.Arm_M1911_Idle_;
        Arm_animation.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Arm_idle()
    {
        Arm_animation.CrossFade(Arm_anim.Arm_M1911_Idle_.name, 0.5f);
    }

    public void Arm_fire()
    {
        Arm_animation.CrossFade(Arm_anim.Arm_M1911_Fire_.name, 0.5f);
    }

    public void Arm_reload()
    {
        Arm_animation.CrossFade(Arm_anim.Arm_M1911_Reload_.name, 0.5f);
    }
}
