using UnityEngine;
using System.Collections;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] GameObject weaponCollectiblePrefab;
    private GameObject collectible;
    private float spawnCooldown = 4.0f;
    private bool spawningSoon;

    void Start()
    {
        collectible = Instantiate(weaponCollectiblePrefab, gameObject.transform.position + new Vector3(0, 2.0f, 0), Quaternion.identity, gameObject.transform);
        spawningSoon = false;
    }

    void Update()
    {
        if (!spawningSoon && !collectible) {
            StartCoroutine(SpawnWeapon());
        }
    }

    private IEnumerator SpawnWeapon() {
        spawningSoon = true;
        yield return new WaitForSeconds(spawnCooldown);
        collectible = Instantiate(weaponCollectiblePrefab, gameObject.transform.position + new Vector3(0, 2.0f, 0), Quaternion.identity, gameObject.transform);
        spawningSoon = false;
    }
}
