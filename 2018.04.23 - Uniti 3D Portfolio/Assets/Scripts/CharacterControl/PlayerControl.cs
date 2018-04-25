using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : IChracter
{
    public GameObject objWeaponParent;
    public JoyStickControl joystick;

    private float fCurrMoveSpeed = 0.0f;

    private new void Awake()
    {
        GameObject weapon = Resources.Load("Weapon/Staff_01") as GameObject;
        Instantiate(weapon, objWeaponParent.transform);

        LoadData();

        base.Awake();   // 부모의 Awake() 호출
    }

    private void OnDestroy()
    {
        SaveData();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // 이동 값이 있을 때 (조이스틱)
        if (!attackFunc.isAttack && (joystick.Horizontal != 0.0f || joystick.Vertical != 0.0f))
        {
            // 조이스틱 방향으로 회전
            transform.rotation = Quaternion.LookRotation(new Vector3(joystick.Horizontal, 0, joystick.Vertical));

            // 이동
            if (fCurrMoveSpeed < fMoveSpeed)
            {
                fCurrMoveSpeed += Time.deltaTime * 2.0f;
                Mathf.Clamp(fCurrMoveSpeed, 0.0f, fMoveSpeed);
            }

            transform.Translate(Vector3.forward * (Mathf.Abs(joystick.Horizontal) + Mathf.Abs(joystick.Vertical)) * fCurrMoveSpeed * Time.deltaTime);
        }
        // 이동 값이 있을 때 (키보드)
        else if (!attackFunc.isAttack && (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f))
        {
            float fH = Input.GetAxis("Horizontal");
            float fV = Input.GetAxis("Vertical");

            transform.rotation = Quaternion.LookRotation(new Vector3(fH, 0, fV));

            // 이동
            if (fCurrMoveSpeed < fMoveSpeed)
            {
                fCurrMoveSpeed += Time.deltaTime * 2.0f;
                Mathf.Clamp(fCurrMoveSpeed, 0.0f, fMoveSpeed);
            }

            transform.Translate(Vector3.forward * (Mathf.Abs(fH) + Mathf.Abs(fV)) * fCurrMoveSpeed * Time.deltaTime);
        }
        // 이동 값이 없거나 어택 중일 때
        else
        {
            if (fCurrMoveSpeed > 0.0f)
            {
                fCurrMoveSpeed -= Time.deltaTime * 2.0f;
                Mathf.Clamp(fCurrMoveSpeed, 0.0f, fMoveSpeed);
                transform.Translate(Vector3.forward * fCurrMoveSpeed * Time.deltaTime);
            }
        }

        // 이동 값 설정
        anim.SetFloat(Util.AnimParam.MOVE_SPEED, fCurrMoveSpeed / fMoveSpeed);
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("nLevel"))
            nLevel = PlayerPrefs.GetInt("nLevel");
        else
            nLevel = 1;

        // 레벨에 따른 스탯 셋팅
        fMaxHP = LevelManager.Instace.GetHpByLevel(nLevel);
        fMaxMP = LevelManager.Instace.GetMpByLevel(nLevel);

        if (PlayerPrefs.HasKey("fCurrHP"))
            fCurrHP = PlayerPrefs.GetInt("fCurrHP");
        else
            fCurrHP = fMaxHP;

        if (PlayerPrefs.HasKey("fCurrMP"))
            fCurrMP = PlayerPrefs.GetInt("fCurrMP");
        else
            fCurrMP = fMaxMP;
    }

    private void SaveData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("nLevel", nLevel);
        PlayerPrefs.SetInt("fCurrHP", (int)fCurrHP);
        PlayerPrefs.SetInt("fCurrMP", (int)fCurrMP);
        PlayerPrefs.Save();
    }
}
