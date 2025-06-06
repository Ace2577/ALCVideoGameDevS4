using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [Header("Projectile")]
    public GameObject projectile;
    public Transform firePoint;
    public float projectileSpeed;
    private bool isPlayer;
    [Header("Shoot Rate and Time")]
    private float lastShootTime;
    public float shootRate;
    [Header("Ammo")]
    public int curAmmo;
    public int maxAmmo;
    public bool infiniteAmmo;
    
    void Awake()
    {
        curAmmo=maxAmmo;
        if(GetComponent<PlayerControllerFPS>())
            isPlayer=true;
    }
    public bool CanShoot()
    {
        if(Time.time - lastShootTime >=shootRate)
        {
            if(curAmmo>0 || infiniteAmmo)
                return true;
        }
        return false;
    }
    public void Shoot()
    {
        lastShootTime=Time.time;
        curAmmo--;
        GameObject projectileObject=Instantiate(projectile,firePoint.position,firePoint.rotation);
        projectileObject.GetComponent<Rigidbody>().velocity=firePoint.forward*projectileSpeed;
    }
}
