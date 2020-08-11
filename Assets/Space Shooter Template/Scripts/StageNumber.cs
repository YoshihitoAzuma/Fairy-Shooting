using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageNumber : MonoBehaviour
{
    private Text stageNumberText;

    void Start()
    {
        // 「Text」コンポーネントにアクセスして取得する。
        stageNumberText = this.gameObject.GetComponent<Text>();
        var activeSceneName = SceneManager.GetActiveScene().name;
        
        if(activeSceneName=="Stage1_Scene")
        {
            stageNumberText.text = "Stage1スタート";    
        }
        else if(activeSceneName=="Stage2_Scene")
        {
            stageNumberText.text = "Stage2スタート";    
        }
        else if(activeSceneName=="Stage3_Scene")
        {
            stageNumberText.text = "Stage3スタート";    
        }
        else if(activeSceneName=="Stage4_Scene")
        {
            stageNumberText.text = "Stage4スタート";    
        }
        else if(activeSceneName=="Stage5_Scene")
        {
            stageNumberText.text = "Stage5スタート";    
        }
        else if(activeSceneName=="Stage6_Scene")
        {
            stageNumberText.text = "Stage6スタート";    
        }
        else if(activeSceneName=="Stage7_Scene")
        {
            stageNumberText.text = "Stage7スタート";    
        }
        else if(activeSceneName=="Stage8_Scene")
        {
            stageNumberText.text = "Stage8スタート";    
        }
        else if(activeSceneName=="Stage9_Scene")
        {
            stageNumberText.text = "Stage9スタート";    
        }
        else if(activeSceneName=="Stage10_Scene")
        {
            stageNumberText.text = "Stage10スタート";    
        }
        else if(activeSceneName=="Stage11_Scene")
        {
            stageNumberText.text = "Stage11スタート";    
        }
        else if(activeSceneName=="Stage12_Scene")
        {
            stageNumberText.text = "Stage12スタート";    
        }
        else if(activeSceneName=="Stage13_Scene")
        {
            stageNumberText.text = "Stage13スタート";    
        }
        else if(activeSceneName=="Stage14_Scene")
        {
            stageNumberText.text = "Stage14スタート";    
        }            
        else if(activeSceneName=="Stage15_Scene")
        {
            stageNumberText.text = "Stage15スタート";    
        }
        else if(activeSceneName=="Stage16_Scene")
        {
            stageNumberText.text = "Stage16スタート";    
        }
        else if(activeSceneName=="Stage17_Scene")
        {
            stageNumberText.text = "Stage17スタート";    
        }
        else if(activeSceneName=="Stage18_Scene")
        {
            stageNumberText.text = "Stage18スタート";    
        }
        else if(activeSceneName=="Stage19_Scene")
        {
            stageNumberText.text = "Stage19スタート";    
        }
        else if(activeSceneName=="Stage20_Scene")
        {
            stageNumberText.text = "Stage20スタート";    
        }
    }

    // Update is called once per frame
    void Update()
    {
        stageNumberText.color = Color.Lerp(stageNumberText.color, new Color(1, 1, 1, 0), 0.5f * Time.deltaTime);       
    }
}
