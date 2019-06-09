using System;
using UnityEngine;
using UnityEngine.UI;

public class SlingShotSpawner : MonoBehaviour
{
    #region Variables
    public GameObject bulletPrefab;
    public Transform spawnTransform;
    public Camera firstpersonCamera;
    public Text debugText;
    private GameObject debugBullet;
    #endregion

    #region Initialization
    public void Start()
    {
       // CalculateSpawnPosition();
        debugBullet = Instantiate(bulletPrefab, spawnTransform.position, Quaternion.identity);
        Destroy(debugBullet.GetComponentInChildren<Rigidbody>());
        Destroy(debugBullet.GetComponentInChildren<Collider>());
        Destroy(debugBullet.GetComponentInChildren<MeshRenderer>());
    }
    #endregion

    #region Functionality
    private void Update()
    {
       // CalculateSpawnPosition();
        debugText.text = $"CameraPosition:{firstpersonCamera.transform.position }SpawnPosition{spawnTransform.position}";
      //debugBulletSpawnPoint.transform.position = spawnPosition.position;
    }

    public void FireBullet()
    {
         GameObject bullet = Instantiate(bulletPrefab, spawnTransform.position, Quaternion.identity);
         bullet.GetComponentInChildren<Rigidbody>().AddForce(spawnTransform.forward*10f,ForceMode.Impulse);
    }
    #endregion
}

