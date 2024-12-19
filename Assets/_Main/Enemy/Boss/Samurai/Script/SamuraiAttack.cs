using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SamuraiAttack : MonoBehaviour
{
    SamuraiController samuraicontroller;
    Animator animator;
    Vector3 _targetpos = Vector3.zero;
    public GameObject clone;
    bool b_isattacking;

    private float f_cooldown = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        samuraicontroller = GetComponent<SamuraiController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _targetpos = samuraicontroller.enemystatus.programstatus.currentTarget.transform.position;
        float _distance = (_targetpos - transform.position).magnitude;
        if (!b_isattacking)
        {
            int attacktype = Random.Range(0, 2);
            switch (attacktype)
            {
                case 0:
                    Attack();
                    break;
                case 1:
                    Combo("Combo1");
                    break;
            }

            b_isattacking = true;
            f_cooldown = Random.Range(3f, 10f);
        }
        else
        {
            f_cooldown -= Time.deltaTime;
            if (f_cooldown <= 0) b_isattacking = false;
        }

        if (Input.GetKeyDown(KeyCode.F)) Bunshin();
    }

    void Attack()
    {
        int attacknum = Random.Range(0, 4);
        animator.SetInteger("AttackNum", attacknum);
    }

    void Combo(string _comboname)
    {
        animator.SetTrigger(_comboname);
    }


    void Bunshin()
    {
        clone.transform.position = transform.position;
        clone.transform.rotation = transform.rotation;
        
        foreach (HumanBodyBones bone in System.Enum.GetValues(typeof(HumanBodyBones)))
        {
            if (bone == HumanBodyBones.LastBone) continue; // 忽略无效骨骼

            // 获取本体和分身的对应骨骼
            Transform sourceBone = animator.GetBoneTransform(bone);
            Transform targetBone = clone.GetComponent<Animator>().GetBoneTransform(bone);

            if (sourceBone != null && targetBone != null)
            {
                // 同步骨骼的局部位置和旋转
                targetBone.localPosition = sourceBone.localPosition;
                targetBone.localRotation = sourceBone.localRotation;
            }
        }
        
        clone.SetActive(true);

        f_cooldown = 0;
        transform.position = samuraicontroller.enemystatus.programstatus.currentTarget.transform.position -
                             samuraicontroller.enemystatus.programstatus.currentTarget.transform.forward * 2;
        transform.LookAt(samuraicontroller.enemystatus.programstatus.currentTarget.transform.position);
    }
}