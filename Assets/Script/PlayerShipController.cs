using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefabs;
    [Range(0.01f, 2f)] public float fireRate;

    private float nextFire;
    private GameObject cloneBullet;
    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bulletPrefabs,transform.position,Quaternion.identity);
            Instantiate(bulletPrefabs,transform.position,Quaternion.Euler(new Vector3(0,0,-25)));
            Instantiate(bulletPrefabs,transform.position,Quaternion.Euler(new Vector3(0,0,25)));
        }
    }
}
