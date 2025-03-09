using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{

    [SerializeField] int max_health = 20;
    private int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public void ReactToHit(int _damage) {
        health -= _damage;
    }
}
