using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearScript : MonoBehaviour
{
    //public GameObject playButton;
    //public Sprite loadingButtonSprite;
    //bool settingsAreOpen = false;

    private void Start()
    {

    }

    // ステージ選択へ遷移
    public void LoadStageSelectScene()
    {
        //playButton.GetComponent<Image>().sprite = loadingButtonSprite; 
        //SceneManager.LoadScene(1);

        Time.timeScale = 1;
        SceneManager.LoadScene("StageSelectScreen");
    }

    // // スタートメニューへ遷移
    // public void LoadMainScene()
    // {
    //     //playButton.GetComponent<Image>().sprite = loadingButtonSprite; 
    //     //SceneManager.LoadScene(1);

    //     Time.timeScale = 1;
    //     SceneManager.LoadScene(0);
    // }

}
