using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// This script counts the amount of points and coins and shows it on the screen.
/// </summary>
[System.Serializable]
public class MenuIcons
{
    public GameObject short_lazerMenuIcon, rocketMenuIcon, swirlingMenuIcon, rayMenuIcon;
    public Sprite short_lazerMenuIconColor, rocketMenuIconColor, swirlingMenuIconColor, rayMenuIconColor;
    public Sprite short_lazerMenuIconWhite, rocketMenuIconWhite, swirlingMenuIconWhite, rayMenuIconWhite;
}

public class GameController : MonoBehaviour {

    public GameObject coinPrefab;
    [Range(0,100)]
    public int chanceForCoin = 10;

    
    Text scoreText;     //text displayed in 'Score' field to 'Main Canvas'
    Text coinsText;

    // ★シングルトン（シーン間のデータ共有）
    public static int score=0;
    public static int coins=0;
    //int coins;

    public GameObject gameOverScreen;

    public Text finalScoreText;

    [HideInInspector] public bool gameIsActive;

    public static GameController instance;  //static reference for access from other classes

    #region menu fields

    public bool ChooseShipAtStart = false;
    public GameObject backPanel;
    public GameObject mainMenuPanel;
    public GameObject frontPanelShip;
    public GameObject frontPanelPause;
    public GameObject pauseButton;
    public MenuIcons menuShipIcons;
      //
    private GameObject[] enemyObjects;
    bool settingsAreOpen = false;

    // セーブ用のプレフィックス
    private string prefixKeyStageStatus = "StageStatus";

#endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
        coinsText = GameObject.FindWithTag("CoinsText").GetComponent<Text>();
        gameOverScreen.SetActive(false);

        if (ChooseShipAtStart)
            OpenChooseShipMenu();
        else
            CloseMenuPanel();
    }

    private void Start()
    {
        //score = 0;
        scoreText.text = score.ToString();
        //coins = 0;
        coinsText.text = coins.ToString();


    }

    public void AddScore(int count)     //updating 'Score' and display on 'Main Canvas'
    {
        score += count;
        scoreText.text = score.ToString();
    }

    public void AddCoin()
    {
        coins++;
        coinsText.text = coins.ToString();
        SoundManager.instance.PlaySound("coin");
    }

#region Menu Controlling
    public void SettingsButtonActivation()
    {
        if (!settingsAreOpen)
        {
            backPanel.GetComponent<Animator>().SetTrigger("On");
            settingsAreOpen = true;
        }
        else
        {
            backPanel.GetComponent<Animator>().SetTrigger("Off");
            settingsAreOpen = false;
        }
    }
    
    void OpenChooseShipMenu()
    {
        mainMenuPanel.SetActive(true);
        frontPanelShip.SetActive(true);
        frontPanelPause.SetActive(false);
        StopTheGame();
    }

    public void OpenPauseMenu()
    {
        mainMenuPanel.SetActive(true);
        frontPanelShip.SetActive(false);
        frontPanelPause.SetActive(true);
        StopTheGame();        
    }

    void StopTheGame()
    {
        Time.timeScale = 0;
        PlayerMoving.instance.controlIsActive = false;
        pauseButton.SetActive(false);
    }

    public void CloseMenuPanel()
    {
        mainMenuPanel.SetActive(false);
        PlayerMoving.instance.controlIsActive = true;
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        gameIsActive = true;
    }

    public void SetStartSkin(string mode)
    {
        switch (mode)
        {
            case "short_lazer":
                PlayerShooting.instance.activeShootingMode = ActiveShootingMode.Short_Lazer;
                DisableMenuIcons();
                menuShipIcons.short_lazerMenuIcon.GetComponent<Image>().sprite = menuShipIcons.short_lazerMenuIconColor;
                break;
            case "rocket":
                PlayerShooting.instance.activeShootingMode = ActiveShootingMode.Rocket;
                DisableMenuIcons();
                menuShipIcons.rocketMenuIcon.GetComponent<Image>().sprite = menuShipIcons.rocketMenuIconColor;
                break;
            case "ray":
                PlayerShooting.instance.activeShootingMode = ActiveShootingMode.Ray;
                DisableMenuIcons();
                menuShipIcons.rayMenuIcon.GetComponent<Image>().sprite = menuShipIcons.rayMenuIconColor;
                break;
            case "swirling":
                PlayerShooting.instance.activeShootingMode = ActiveShootingMode.Swirling;
                DisableMenuIcons();
                menuShipIcons.swirlingMenuIcon.GetComponent<Image>().sprite = menuShipIcons.swirlingMenuIconColor;
                break;
        }
        Player.instance.UpdateSkin();
    }

    void DisableMenuIcons()
    {
        menuShipIcons.short_lazerMenuIcon.GetComponent<Image>().sprite = menuShipIcons.short_lazerMenuIconWhite;
        menuShipIcons.rocketMenuIcon.GetComponent<Image>().sprite = menuShipIcons.rocketMenuIconWhite;
        menuShipIcons.rayMenuIcon.GetComponent<Image>().sprite = menuShipIcons.rayMenuIconWhite;
        menuShipIcons.swirlingMenuIcon.GetComponent<Image>().sprite = menuShipIcons.swirlingMenuIconWhite;
    }
    #endregion

    public void GameOver()
    {
        StartCoroutine(GameOverCor());
    }

    IEnumerator GameOverCor()
    {
        yield return new WaitForSeconds(0.5f);
        gameOverScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        finalScoreText.text = score + "\n"+"scores";
        if (Services.ServicesAreEnable)
            Services.instance.ReportToLeaderboard(score);

        // スコアをクリア       
        //ScoreReset();
    }

    public void Restart()
    {
        // 現在のシーン名を取得して、リロードする
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenStartScreen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    // ゲームクリアシーンへ遷移
    public void GameClear()
    {
        StartCoroutine(GameClearCor());
    }
    IEnumerator GameClearCor() {
        yield return new WaitForSeconds(5f);

        var activeScene = SceneManager.GetActiveScene().name;
        if (activeScene == "Stage1_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage2_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {                
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage2_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage3_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {              
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage3_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage4_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {                
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
		else if(activeScene == "Stage4_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage5_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {               
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage5_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage6_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {   
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage6_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage7_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {                
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage7_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage8_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage8_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage9_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }            
        else if(activeScene == "Stage9_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage10_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage10_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage11_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        } 
        else if(activeScene == "Stage11_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage12_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage12_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage13_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }   
        else if(activeScene == "Stage13_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage14_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }           
        else if(activeScene == "Stage14_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage15_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage15_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage16_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage16_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage17_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage17_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage18_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage18_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage19_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage19_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage20_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }
        else if(activeScene == "Stage20_Scene")
        {
            // 現在のステージ状態を取得
            string keyStageStatus = prefixKeyStageStatus + activeScene;
            // 次のステージ名を設定
            string nextStagesName = "Stage21_Scene";
            // 次のステージ状態を取得
            string keyNextStageStatus = prefixKeyStageStatus + nextStagesName;

            // 現在のステージステータスを「クリア済み」にする
            PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Cleared);

            if (PlayerPrefs.GetInt(keyNextStageStatus) == (int)StageStatus.Locked)
            {
                // 次のステージステータスを「unlocked」にする
                PlayerPrefs.SetInt(keyNextStageStatus, (int)StageStatus.Unlocked);
            }
            // クリアシーンに遷移する。
		    SceneManager.LoadScene("Clear_Stage");
        }

	}
    // ★

    // ★ゲームオーバ時にスコアとコインをクリアする
    public void ScoreReset()
    {
        score = 0;
        coins = 0;
    }


}
