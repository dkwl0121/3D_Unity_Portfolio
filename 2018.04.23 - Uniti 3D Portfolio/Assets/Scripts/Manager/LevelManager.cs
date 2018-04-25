using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
    private static LevelManager sInstance = null;
    public static LevelManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                //GameObject newObject = new GameObject("_LevelManager");
                sInstance = new LevelManager();
                sInstance.Setup();
            }
            return sInstance;
        }
    }

    level levelDB = null;

    // ================================================== 한번에 셋업 관리하기!!!!
    private void Setup()
    {
        levelDB = Resources.Load<level>("DB/Level");
    }

    public int GetHpByLevel(int level)
    {
        if (level <= 0)
            return Util.NumValue.INVALID_NUMBER;
        
        return levelDB.dataArray[level -1].Maxhp;
    }

    public int GetMpByLevel(int level)
    {
        if (level <= 0)
            return Util.NumValue.INVALID_NUMBER;

        return levelDB.dataArray[level - 1].Maxmp;
    }
}
