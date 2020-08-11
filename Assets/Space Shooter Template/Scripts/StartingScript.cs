using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Starting script is designed to load the main scene
/// </summary>

public class StartingScript : MonoBehaviour {

    public GameObject playButton;
    public Sprite loadingButtonSprite;
    public GameObject SettingsButton, MusicButton, SoundButton;
    public Sprite MusicOnSprite, MusicOffSprite, SoundOnSprite, SoundOffSprite;
    bool settingsAreOpen = false;

    private void Start()
    {
        //check if sound and music are on
        if (PlayerPrefs.GetString("Music") == "Off")
            MusicButton.GetComponent<Image>().sprite = MusicOffSprite;
        else
            MusicButton.GetComponent<Image>().sprite = MusicOnSprite;
        if (PlayerPrefs.GetString("Sound") == "Off")
            SoundButton.GetComponent<Image>().sprite = SoundOffSprite;
        else
            SoundButton.GetComponent<Image>().sprite = SoundOnSprite;
    }

    // Load the scene which is by index 1 in the builded scenes list
    public void LoadMainScene()
    {
        playButton.GetComponent<Image>().sprite = loadingButtonSprite; 
        SceneManager.LoadScene(1);
    }

    public void OpenSettings ()
    {
        if (settingsAreOpen)
            SettingsButton.GetComponent<Animator>().SetTrigger("Close");
        else
            SettingsButton.GetComponent<Animator>().SetTrigger("Open");
        settingsAreOpen = !settingsAreOpen;
    }

    public void SwitchMusic()
    {
        if (PlayerPrefs.GetString("Music") == "Off")
        {
            PlayerPrefs.SetString("Music", "On");
            MusicButton.GetComponent<Image>().sprite = MusicOnSprite;
        }
        else
        {
            PlayerPrefs.SetString("Music", "Off");
            MusicButton.GetComponent<Image>().sprite = MusicOffSprite;
        }
    }

    public void SwitchSound()
    {
        if (PlayerPrefs.GetString("Sound") == "Off")
        {
            PlayerPrefs.SetString("Sound", "On");
            SoundButton.GetComponent<Image>().sprite = SoundOnSprite;
        }
        else
        {
            PlayerPrefs.SetString("Sound", "Off");
            SoundButton.GetComponent<Image>().sprite = SoundOffSprite;
        }
    }

    public void OpenPolicy()
    {
        string url = "https://pasokonnoadd.hatenablog.com/entry/2020/08/01/222334";
        Application.OpenURL(url);
    }

    // ステージ1遷移
    public void LoadStage1Scene()
    {
        //playButton.GetComponent<Image>().sprite = loadingButtonSprite; 
        //SceneManager.LoadScene(1);

        Time.timeScale = 1;
        SceneManager.LoadScene("Stage1_Scene");
    }

    // ステージ2遷移
    public void LoadStage2Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage2_Scene");
    }
    // ステージ3遷移
    public void LoadStage3Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage3_Scene");
    }

    // ステージ4遷移
    public void LoadStage4Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage4_Scene");
    }
    // ステージ5遷移
    public void LoadStage5Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage5_Scene");
    }

    // ステージ6遷移
    public void LoadStage6Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage6_Scene");
    }
    // ステージ7遷移
    public void LoadStage7Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage7_Scene");
    }
    // ステージ8遷移
    public void LoadStage8Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage8_Scene");
    }
    // ステージ9遷移
    public void LoadStage9Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage9_Scene");
    }
    // ステージ10遷移
    public void LoadStage10Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage10_Scene");
    }
    // ステージ11遷移
    public void LoadStage11Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage11_Scene");
    }
    // ステージ12遷移
    public void LoadStage12Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage12_Scene");
    }
    // ステージ13遷移
    public void LoadStage13Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage13_Scene");
    }
    // ステージ14遷移
    public void LoadStage14Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage14_Scene");
    }
    // ステージ15遷移
    public void LoadStage15Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage15_Scene");
    }
    // ステージ16遷移
    public void LoadStage16Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage16_Scene");
    }
    // ステージ17遷移
    public void LoadStage17Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage17_Scene");
    }
    // ステージ18遷移
    public void LoadStage18Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage18_Scene");
    }
    // ステージ19遷移
    public void LoadStage19Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage19_Scene");
    }
    // ステージ20遷移
    public void LoadStage20Scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage20_Scene");
    }

}
