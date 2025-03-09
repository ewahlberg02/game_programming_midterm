using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnCooldown = 1;
    [SerializeField] int maxEnemies = 1;
    [SerializeField] int startingHealth = 20;

    public List<GameObject> enemyList;
    private float spawnTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnCooldown;
        enemyList = new List<GameObject>(maxEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        AttemptSpawnEnemy();
        enemyList.RemoveAll(obj => obj == null);
    }

    private void AttemptSpawnEnemy() {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f && enemyList.Count < maxEnemies) {
            SpawnEnemy();
            spawnTimer = spawnCooldown;
        }
    }

    private void SpawnEnemy() {
        GameObject enemy = Instantiate(enemyPrefab, gameObject.transform);
        enemy.transform.position = this.gameObject.transform.position;
        float angle = UnityEngine.Random.Range(0, 360);
        enemy.transform.Rotate(0, angle, 0);
        ReactiveTarget rt = enemy.GetComponent<ReactiveTarget>();
        if(rt) {
            rt.SetStartHealth(startingHealth);
        }
        enemyList.Add(enemy);
    }
}