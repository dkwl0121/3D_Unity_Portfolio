﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton
{
    public GameObject objRoot = null;
    public GameObject objText;
    public Image imgFront;
    public Text text;
    public float fMaxCount;
    public float fCurrCount;

    public AttackButton() { }
}

public class PlayerAttackFunction : IAttackFunction
{
    public GameObject objAttackBtn;

    private AttackButton[] stAttackBtn;

    private new void Awake()
    {
        base.Awake();

        arrAnimParam = new string[(int)E_PLAYER_ATTACK_NO.MAX];
        arrAnimParam[(int)E_PLAYER_ATTACK_NO.DEFAULT] = Util.AnimParam.DEFAULT_ATTACK;
        arrAnimParam[(int)E_PLAYER_ATTACK_NO.SKILL_01] = Util.AnimParam.SKILL_01;
        arrAnimParam[(int)E_PLAYER_ATTACK_NO.SKILL_02] = Util.AnimParam.SKILL_02;
        arrAnimParam[(int)E_PLAYER_ATTACK_NO.SKILL_03] = Util.AnimParam.SKILL_03;

        stAttackBtn = new AttackButton[(int)E_PLAYER_ATTACK_NO.MAX];
        SetAttackBtn((int)E_PLAYER_ATTACK_NO.DEFAULT, "DefaultAttack");
        SetAttackBtn((int)E_PLAYER_ATTACK_NO.SKILL_01, "Skill_01");
        SetAttackBtn((int)E_PLAYER_ATTACK_NO.SKILL_02, "Skill_02");
        SetAttackBtn((int)E_PLAYER_ATTACK_NO.SKILL_03, "Skill_03");
    }

    private void SetAttackBtn(int index, string btnName)
    {
        stAttackBtn[index] = new AttackButton();

        if (objAttackBtn.transform.Find(btnName) == null) return;

        stAttackBtn[index].objRoot = objAttackBtn.transform.Find(btnName).gameObject;
        stAttackBtn[index].objText = stAttackBtn[index].objRoot.transform.Find("Text").gameObject;
        stAttackBtn[index].imgFront = stAttackBtn[index].objRoot.transform.Find("Front").GetComponent<Image>();
        stAttackBtn[index].text = stAttackBtn[index].objText.GetComponent<Text>();
        stAttackBtn[index].fMaxCount = index * 5;
        stAttackBtn[index].fCurrCount = 0;
    }

    private void Update()
    {
        // 공격 여부에 따라 버튼(front) 활성화 설정
        for (int i = 0; i < stAttackBtn.Length; ++i)
        {
            if (stAttackBtn[i].objRoot == null
                || stAttackBtn[i].objRoot.activeSelf == false)  // 버튼이 비활성화 되있으면
                continue;

            if (isAttack)
                stAttackBtn[i].imgFront.gameObject.SetActive(false);
            else
                stAttackBtn[i].imgFront.gameObject.SetActive(true);
        }

        KeyControl();

        CoolTimeUpdate();
    }

    private void KeyControl()
    {
        // 키보드 공격
        if (Input.GetKeyDown(KeyCode.Alpha1))
            StartAttack((int)E_PLAYER_ATTACK_NO.DEFAULT);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            StartAttack((int)E_PLAYER_ATTACK_NO.SKILL_01);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            StartAttack((int)E_PLAYER_ATTACK_NO.SKILL_02);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            StartAttack((int)E_PLAYER_ATTACK_NO.SKILL_03);
    }

    private void CoolTimeUpdate()
    {
        for (int i = 0; i < stAttackBtn.Length; ++i)
        {
            if (stAttackBtn[i].objRoot == null
                || stAttackBtn[i].objRoot.activeSelf == false  // 버튼이 비활성화 되있거나
                || stAttackBtn[i].objText.activeSelf == false)  // 카운트가 비활성화 되있으면
                continue;

            stAttackBtn[i].fCurrCount -= Time.deltaTime;

            if (stAttackBtn[i].fCurrCount <= 0)
            {
                stAttackBtn[i].fCurrCount = 0;
                stAttackBtn[i].objText.SetActive(false);
            }

            else
            {
                stAttackBtn[i].text.text = ((int)stAttackBtn[i].fCurrCount + 1).ToString();
            }

            stAttackBtn[i].imgFront.fillAmount = 1.0f - (stAttackBtn[i].fCurrCount / stAttackBtn[i].fMaxCount);
        }
    }

    public new void StartAttack(int index)
    {
        if (isAttack || stAttackBtn[index].objRoot.activeSelf == false  // 버튼이 비활성화 되있거나
            || stAttackBtn[index].fCurrCount > 0)           // 카운트가 끝나지 않았으면
            return;

        stAttackBtn[index].objText.SetActive(true);
        stAttackBtn[index].fCurrCount = stAttackBtn[index].fMaxCount;
        stAttackBtn[index].text.text = ((int)stAttackBtn[index].fCurrCount + 1).ToString();

        base.StartAttack(index);
    }

    public void AttackColOn(int index)
    {
        float size = ((float)index * 0.5f) + 1;

        attackCol.enabled = true;
        attackCol.radius = size;
        attackCol.transform.position = new Vector3(0, size, size);
    }

    public void AttackColOff(int index)
    {
        attackCol.enabled = false;
    }
}
