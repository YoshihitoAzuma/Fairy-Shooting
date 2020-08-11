using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

//Sprites for every kind of ship
[System.Serializable]
public class ShipSkins
{
    public Sprite short_LazerShip, rocketShip, swirlingShip, rayShip;
}

//Images for every stage of health bar on Main Canvas
[System.Serializable]
public class HealthIndicators
{
    public Sprite health_0, health_1, health_2, health_3;
}

public class Player : MonoBehaviour
{
    #region FIELDS

    int health;

    [Tooltip("time after receiving the damage when 'Player' will be non-vulnerable to the new damage")]
    public float damageFrequency;


    public ShipSkins shipSkins;
    public HealthIndicators healthIndicatorsSprites;

    [Tooltip("Object 'Shield' of the 'Player' located in his hierarchy")]
    public GameObject shield;

    [Tooltip("VFX's prefab emerging after the Player is destroyed")]
    public GameObject destructionFX;

    Image healthIndicatorImage;
    float nextDamage;
    SpriteRenderer playerSprite;

    public static Player instance; 

    // ★（追加）
    private Slider playerHPSlider;
    public GameObject[] playerIcons;

    // プレーヤーが破壊された回数のデータを入れる箱
    public int destroyCount = 0;

#endregion

    private void Awake()
    {
        if (instance == null) 
            instance = this;
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        healthIndicatorImage = GameObject.FindWithTag("Health").GetComponent<Image>();  //set the health to 3 and update the health bar
        // プレイヤーのHP
        health = 10;

        playerHPSlider = GameObject.Find("PlayerHPSlider").GetComponent<Slider>();
        playerHPSlider.maxValue = health;
        playerHPSlider.value = health;

        UpdateHealthIndicator();
        UpdateSkin();
    }

    //プレイヤーにダメージを与える
    public void GetDamage(int damage)   
    {
        if (Time.time > nextDamage)     //checking if the time comes for the new damage, and if it does, decreasing health and setting the new time
        {
            health -= damage;
            UpdateHealthIndicator();  //プレイヤーのライフゲージを更新

            // 敵のHPを更新
            playerHPSlider.value = health;

            nextDamage = Time.time + damageFrequency;
            // プレイヤーを爆破
            if (health <= 0)
            {    
                // ★（追加）
                // HPが０になったら破壊された回数を１つ増加させる。
                destroyCount += 1;    
                // 残機数を更新
                UpdatePlayerIcons();

                Destruction();
            }
            else
            {
                shield.SetActive(false); //deactivate and activate Shield to set up animation
                shield.SetActive(true);
            }
        }
    }
    // ★（追加）
    // プレーヤーの残機数を表示する命令ブロック（メソッド）
    void UpdatePlayerIcons()
    {
        // for文（繰り返し文）
        for (int i = 0; i < playerIcons.Length; i++)
        {
            if (destroyCount <= i)
            {
                playerIcons[i].SetActive(true);
            }
            else
            {
                playerIcons[i].SetActive(false);
            }
        }
    }

    
    //プレイヤーのライフゲージを更新
    void UpdateHealthIndicator()
    {
        switch (health)
        {
            case 0:
                healthIndicatorImage.sprite = healthIndicatorsSprites.health_0;
                break;
            case 1:
                healthIndicatorImage.sprite = healthIndicatorsSprites.health_1;
                break;
            case 2:
                healthIndicatorImage.sprite = healthIndicatorsSprites.health_2;
                break;
            case 3:
                healthIndicatorImage.sprite = healthIndicatorsSprites.health_3;
                break;
            // 追加
            default:
                healthIndicatorImage.sprite = healthIndicatorsSprites.health_3;
                break;
                                
        }
    }

    // プレイヤーが爆破された場合
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        
		// ★★★（追加）
		// 破壊された回数によって場合分けを行います。
		if(destroyCount < 5)
		{
            // リトライの命令ブロック（メソッド）を１秒後に呼び出す。
			Invoke("Retry", 2.0f);
            //Debug.Log("リトライします");
        }
		else
        {  
            GameController.instance.GameOver();
            Destroy(gameObject);
        }
    }

    // ゲームを再プレイ
    void Retry(){
		this.gameObject.SetActive (true);
		health = 10;
        //Debug.Log("リトライ");
		playerHPSlider.value = health;
	}


    //
    public void UpdateSkin()
    {
        switch (PlayerShooting.instance.activeShootingMode)
        {
            case ActiveShootingMode.Short_Lazer:
                playerSprite.sprite = shipSkins.short_LazerShip;
                break;
            case ActiveShootingMode.Rocket:
                playerSprite.sprite = shipSkins.rocketShip;
                break;
            case ActiveShootingMode.Swirling:
                playerSprite.sprite = shipSkins.swirlingShip;
                break;
            case ActiveShootingMode.Ray:
                playerSprite.sprite = shipSkins.rayShip;
                break;
        }
    }


}

