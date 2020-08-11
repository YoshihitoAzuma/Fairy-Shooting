using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BossDisplay : MonoBehaviour
{
    public int health;

    public GameObject destructionVFX;
    public GameObject bossEnemy;
    public GameController gameController;

    public GameObject Slider;

    // 敵のHPスライダー
    private Slider enemyHPSlider;
    void Start()
    {

        StartCoroutine(CreateBossEnemy(60.0f)); 

    }

  public void BossEnemyCreate()
    {
        bossEnemy.SetActive(true);

        // HPスレイダーを表示
        Slider.SetActive(true);

        enemyHPSlider = GameObject.Find("EnemyHPSlider").GetComponent<Slider>();
        enemyHPSlider.maxValue = health;
        enemyHPSlider.value = health;

        //bossEnemy.SetActive(true);

 
    }

    IEnumerator CreateBossEnemy(float delay)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (Player.instance != null)
            BossEnemyCreate();
            //Debug.Log("CreateBossEnemy"+BossEnemy);
    }

    public void GetDamage(int damage) 
    {
        health -= damage;           
        //Debug.Log("ボスのHPは"+health);
        // 敵のHPを更新
        enemyHPSlider.value = health;

        if (health <= 0) 
            Destruction();
    }
    void Destruction()                           
    {
        // エフェクト
        Instantiate(destructionVFX, transform.position, Quaternion.identity); 
        // 爆破の音を鳴らす
        SoundManager.instance.PlaySound("explosion"); //playing sound
        Destroy(gameObject);

        // ゲームクリア
        gameController.GameClear();

    }

    // Update is called once per frame
    void Update()
    {       

    }
}
