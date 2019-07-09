using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerPrefs : Singleton<CustomPlayerPrefs>
{
    #region 私有方法
    public int GetInt(string code) {
       return PlayerPrefs.GetInt(code,default(int));
    }

    private void SetInt(string code,int num) {
        PlayerPrefs.SetInt(code, num);
    }

    private int GetFloat(string code) {
        return PlayerPrefs.GetInt(code, default(int));
    }

    private void SetFloat(string code, int num) {
        PlayerPrefs.SetInt(code, num);
    }

    private int GetString(string code) {
        return PlayerPrefs.GetInt(code, default(int));
    }

    private void SetString(string code, int num) {
        PlayerPrefs.SetInt(code, num);
    }
    #endregion
    #region 公共方法
    public int Level {
        get {
            if (GetInt(CustomCode.LEVEL) < CustomVariable.LEVELMIN)
                SetInt(CustomCode.LEVEL, CustomVariable.LEVELMIN);
            return GetInt(CustomCode.LEVEL);
        }
        set {
            if (value > CustomVariable.LEVEL_MAX)
                SetInt(CustomCode.LEVEL, CustomVariable.LEVEL_MAX);
            else
                SetInt(CustomCode.LEVEL, value);
        }
    }

    public int Health {
        get {
            if (GetInt(CustomCode.HEALTH) < CustomVariable.HEALTH_MIN)
                SetInt(CustomCode.HEALTH, CustomVariable.HEALTH_MIN);
            return GetInt(CustomCode.HEALTH);
        }
        set {

            if (value > CustomVariable.HEALTH_MAX)
                SetInt(CustomCode.HEALTH, CustomVariable.HEALTH_MAX);
            else
                SetInt(CustomCode.HEALTH, value);
        }
    }
    #endregion
}
