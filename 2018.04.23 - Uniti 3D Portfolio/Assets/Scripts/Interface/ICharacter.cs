using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IChracter : MonoBehaviour
{
    private E_CHARACTER_TYPE eCharType;
    private E_CHARACTER_STAT eCharStat;

    protected Animator anim;                    // 애니메이터
    protected AnimatorStateInfo animState;      // 애니메이터 스텟

    protected IAttackFunction attackFunc;       // 공격 함수 클래스

    protected CapsuleCollider BodyCol;          // 몸 콜리더

    protected int nLevel;

    protected float fMaxHP;
    protected float fCurrHP;
    protected float fMaxMP;
    protected float fCurrMP;

    protected float fMoveSpeed;
    protected float fJumpPower;

    protected void Awake()
    {
        anim = this.GetComponentInChildren<Animator>();
        animState = anim.GetCurrentAnimatorStateInfo(0);
        attackFunc = this.GetComponentInChildren<IAttackFunction>();
        attackFunc.anim = anim;
        attackFunc.animState = animState;

        fMoveSpeed = 2.0f;

        BodyCol = this.GetComponent<CapsuleCollider>();
    }
}
