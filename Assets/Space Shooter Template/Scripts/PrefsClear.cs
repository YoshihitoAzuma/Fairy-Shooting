using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsClear : MonoBehaviour
{
    // Prefs情報をクリアする
    public void OnDebugButtonPrefsClear()
    {
        PlayerPrefs.DeleteAll();
    }

}
