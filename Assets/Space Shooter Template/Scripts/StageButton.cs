using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum StageStatus
{
    Unlocked, Locked, Cleared
}

public class StageButton : MonoBehaviour
{
    public StageStatus stageStatus;

    public bool flgFirstStage;
    public string nameStage;
    //public Color colorOnClear;
    public GameObject imageLocked;

    private string prefixKeyStageStatus = "StageStatus";
    private string keyStageStatus;    
    
    void Awake()
    {
        // ステージの状態を管理する PlayerPrefs のキーを作成します。
        // 接頭辞にステージのシーン名を足した文字列がキーになります。
        keyStageStatus = prefixKeyStageStatus + nameStage;

        // ボタンのコンポーネントを取得します。
        UnityEngine.UI.Button coButton = GetComponent<UnityEngine.UI.Button>();

        // ステージの状態の記録がない場合は新規で設定します。
        // インストール直後のゲームの起動の際に行われます。
        if (PlayerPrefs.HasKey(keyStageStatus) == false)
        {
            // 最初のステージだけを「選択可能」にして、他は「選択不可」にします。
            if (flgFirstStage == true)
            {
                PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Unlocked);
            }
            else
            {
                PlayerPrefs.SetInt(keyStageStatus, (int)StageStatus.Locked);
            }
        }

        // ステージの状態を取得します
        int stageStatus = PlayerPrefs.GetInt(keyStageStatus);
        //Debug.Log(coButton+"ステージステータス："+stageStatus);

        // ステージの状態がクリア済みの場合
        if (stageStatus == (int)StageStatus.Cleared)
        {
            // すでにクリアしている場合は色をクリア済みのものに変えます
            UnityEngine.UI.ColorBlock colorBlock = coButton.colors;
            colorBlock.normalColor = Color.green;
            colorBlock.highlightedColor = Color.green;  //colorOnClear;
            coButton.colors = colorBlock;

            imageLocked.SetActive(false);

            //Debug.Log(coButton+"ボタンをクリア済みにします");
        }

        // ステージの状態が選択不可の場合
        if (stageStatus == (int)StageStatus.Locked)
        {
            // 最初のステージ
            if (flgFirstStage == true)
            {
                imageLocked.SetActive(false);
                //Debug.Log("最初のステージのボタンは、強制的にロックを解除する");
                // ただし、最初のステージのボタンは、強制的にロックを解除する
            }
            else
            {
                // ボタンコンポーネントを押せないように無効にする
                coButton.enabled = false;
                // ロック状態を表す画像のゲームオブジェクトを有効にする
                imageLocked.SetActive(true);
                
            }
        }

        if (stageStatus == (int)StageStatus.Unlocked)
        {
            coButton.enabled = true;
            imageLocked.SetActive(false);
            //Debug.Log(coButton+"を選択可能でアンロックします");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonStage()
    {
        SceneManager.LoadScene(this.nameStage);
    }
    

}
