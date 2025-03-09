using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float speed = 10f;

    void Start()
    {
        Destroy(gameObject, 7.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null) {
            other.GetComponent<PlayerData>().damagePlayer();
        }

        Collider collider = other.GetComponent<Collider>();
        if (collider != null && collider.isTrigger == true)
            return; 
        Destroy(this.gameObject);
    }
}
