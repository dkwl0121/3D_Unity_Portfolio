using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float PosY;
    public float PosZ;
    public float RotX;

    private GameObject objPlayer;
    private Vector3 vPlusPos;
    private Vector3 vRotation;

    private void Awake()
    {
        vPlusPos = new Vector3(0, PosY, PosZ);
        vRotation = new Vector3(RotX, 0, 0);
        objPlayer = GameObject.FindGameObjectWithTag(Util.Tag.PLAYER);
        this.transform.position = objPlayer.transform.position + vPlusPos;
        this.transform.eulerAngles = vRotation;
    }

    private void Update()
    {
        this.transform.position = objPlayer.transform.position + vPlusPos;
    }
}
