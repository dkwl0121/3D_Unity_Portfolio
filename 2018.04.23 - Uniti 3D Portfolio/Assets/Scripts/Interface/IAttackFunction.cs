using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAttackFunction : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    public AnimatorStateInfo animState;

    [HideInInspector] public bool isAttack = false;

    protected string[] arrAnimParam;

    public SphereCollider attackCol;            // 어텍 콜리더

    protected void Awake()
    {
        attackCol.enabled = false;
    }

    public void ResetAttack()
    {
        isAttack = false;
        for (int i = 0; i < arrAnimParam.Length; ++i)
        {
            anim.SetBool(arrAnimParam[i], false);
        }
    }

    protected void StartAttack(int index)
    {
        ResetAttack();
        anim.SetBool(arrAnimParam[index], true);
        isAttack = true;
    }
}
