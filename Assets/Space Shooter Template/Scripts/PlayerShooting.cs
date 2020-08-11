﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// defines which shooting mode is activated. It defines characteristics of the shooting modes. Depending on the activated shooting mode, it makes a shot.
/// </summary>
 
//enumerator defining which shooting mode is currently active
#region Serializable classes
public enum ActiveShootingMode
{
    Short_Lazer, Swirling, Rocket, Ray
}

//serializable class describing all existing shooting modes
[System.Serializable]
public class ShootingMode
{
    public string name;

    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    [Tooltip("projectile prefab")] 
    public GameObject projectileObject;

    //time for a new shot
    [HideInInspector] public float nextFire; 
}

//guns objects in 'Player's' hierarchy
[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun, PlayerRay;
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX; 
}
#endregion

public class PlayerShooting : MonoBehaviour {

    public ActiveShootingMode activeShootingMode;

    [Tooltip("current weapon power")]
    [Range(1, 4)]       //change it if you wish
    public int weaponPower = 1; 

    public ShootingMode[] shootingModes;

    public Guns guns;
    bool shootingIsActive = true; 
    [HideInInspector] public int maxweaponPower = 4; 
    public static PlayerShooting instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        //receiving shooting visual effects components
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (shootingIsActive)
        {
            int mode = (int)activeShootingMode;  //defining active shooting mode index
            // 弾が光線の場合
            if (mode == (int)ActiveShootingMode.Ray) //if active shooting mode is ray, making a shot at once; if not, checking if the time for the next shot comes
                MakeAShot();
            // 弾がロケットの場合
            else if (mode == (int)ActiveShootingMode.Rocket) //if active shooting mode is rocket, shooting time depends on current weapon power
            {
                if (Time.time > shootingModes[mode].nextFire)   //making a shot and setting time for the next one
                {
                    MakeAShot();
                    shootingModes[mode].nextFire = Time.time + 1 / shootingModes[mode].fireRate / weaponPower;
                }
            }
            else
            {
                if (Time.time > shootingModes[mode].nextFire)
                {
                    MakeAShot();                                                         
                    shootingModes[mode].nextFire = Time.time + 1 / shootingModes[mode].fireRate;
                }
            }
        }
    }

    //method for a shot
    void MakeAShot() 
    {
        if (GameController.instance.gameIsActive) // if the game was started
        {
            switch (activeShootingMode) //depending on active shooting mode
            {
                #region Short_Lazer Shot
                case ActiveShootingMode.Short_Lazer:         // if shooting mode is short_lazer
                    {
                        if (guns.PlayerRay.activeSelf)  // if the ray was active, deactivating the ray
                            guns.PlayerRay.SetActive(false);

                        switch (weaponPower) // according to weapon power 'pooling' the defined anount of projectiles, on the defined position, in the defined rotation
                        {
                            case 1:
                                CreateLazerShot(PoolingController.instance.GetPoolingObject(shootingModes[(int)activeShootingMode].projectileObject), guns.centralGun.transform.position, Vector3.zero);
                                guns.centralGunVFX.Play();
                                break;
                            case 2:
                                CreateLazerShot(PoolingController.instance.GetPoolingObject(shootingModes[(int)activeShootingMode].projectileObject), guns.rightGun.transform.position, Vector3.zero);
                                guns.leftGunVFX.Play();
                                CreateLazerShot(PoolingController.instance.GetPoolingObject(shootingModes[(int)activeShootingMode].projectileObject), guns.leftGun.transform.position, Vector3.zero);
                                guns.rightGunVFX.Play();
                                break;
                            case 3:
                                CreateLazerShot(PoolingController.instance.GetPoolingObject(shootingModes[(int)activeShootingMode].projectileObject), guns.centralGun.transform.position, Vector3.zero);
                                CreateLazerShot(PoolingController.instance.GetPoolingObject(shootingModes[(int)activeShootingMode].projectileObject), guns.rightGun.transform.position, new Vector3(0, 0, -5));
                                guns.leftGunVFX.Play();
                                CreateLazerShot(PoolingController.instance.GetPoolingObject(shootingModes[(int)activeShootingMode].projectileObject), guns.leftGun.transform.position, new Vector3(0, 0, 5));
                                guns.rightGunVFX.Play();
                                break;
                            case 4:
                                CreateLazerShot(PoolingController.instance.GetPoolingObject(shootingModes[(int)activeShootingMode].projectileObject), guns.centralGun.transform.position, Vector3.zero);
                                CreateLazerShot(PoolingController.instance.GetPoolingObject(shootingModes[(int)activeShootingMode].projectileObject), guns.rightGun.transform.position, new Vector3(0, 0, -5));
                                guns.leftGunVFX.Play();
                                CreateLazerShot(PoolingController.instance.GetPoolingObject(shootingModes[(int)activeShootingMode].projectileObject), guns.leftGun.transform.position, new Vector3(0, 0, 5));
                                guns.rightGunVFX.Play();
                                // 左右の発射口からさらに分散して発射
                                CreateLazerShot(PoolingController.instance.GetPoolingObject(shootingModes[(int)activeShootingMode].projectileObject), guns.leftGun.transform.position, new Vector3(0, 0, 15));
                                CreateLazerShot(PoolingController.instance.GetPoolingObject(shootingModes[(int)activeShootingMode].projectileObject), guns.rightGun.transform.position, new Vector3(0, 0, -15));
                                break;
                        }
                        SoundManager.instance.PlaySound("shortLazer");     //play sound from 'SoundManager'
                        break;
                    }
                #endregion
                #region Swirling Shot 
                case ActiveShootingMode.Swirling:        // if shootingMode is 'swirling' generating Swirling prefab
                    {
                        if (guns.PlayerRay.activeSelf) // if the ray was active, deactivating the ray
                            guns.PlayerRay.SetActive(false);
                        Instantiate(shootingModes[(int)activeShootingMode].projectileObject, guns.centralGun.transform.position, guns.centralGun.transform.rotation);
                        SoundManager.instance.PlaySound("swirling");
                        break;
                    }
                #endregion
                #region Rocket Shot
                case ActiveShootingMode.Rocket:      // if shooting mode is 'rocket' 'pooling' the new rocket
                    {
                        if (guns.PlayerRay.activeSelf) // if the ray was active, deactivating the ray
                            guns.PlayerRay.SetActive(false);
                        GameObject newRocket = PoolingController.instance.GetPoolingObject(shootingModes[(int)activeShootingMode].projectileObject);
                        newRocket.transform.position = transform.position; // プレイヤーの位置に新しく生成したロケットを配置
                        newRocket.SetActive(true);
                        SoundManager.instance.PlaySound("rocket");     //play sound from 'SoundManager'
                        break;
                    }
                #endregion
                #region Ray
                case ActiveShootingMode.Ray:     // is the shooting mode is 'ray'
                    {
                        if (!guns.PlayerRay.activeSelf && PlayerMoving.instance.controlIsActive) // if the ray was active, deactivating the ray
                            guns.PlayerRay.SetActive(true);
                            //SoundManager.instance.PlaySound("ray");     //play sound from 'SoundManager'
                        break;
                    }
                #endregion*/
                default:
                    break;
            }
        }
    }

    void CreateLazerShot(GameObject lazer, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
        lazer.transform.position = pos;
        lazer.transform.rotation = Quaternion.Euler(rot);
        lazer.SetActive(true);
    }
}
