using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{

    [SerializeField] GameObject projectilePrefab;
    private GameObject projectile;

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public float viewRange = 10.0f;
    public float fallRange = 3.0f;

    private bool isAlive;

    public const float _baseSpeed = 3f;

    private void OnSpeedChanged(float value) {
        speed = _baseSpeed * value;
    }

    private void Start() {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive) {
            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            Ray downRay = new Ray(transform.position + transform.forward, Vector3.down);

            RaycastHit hit;

            if(Physics.SphereCast(ray, 0.75f, out hit, viewRange)) {

                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.GetComponent<PlayerController>()) {
                    if (projectile == null) {
                        projectile = Instantiate(projectilePrefab) as GameObject;
                        projectile.transform.position = transform.TransformPoint(Vector3.forward * 1.5f + new Vector3(0, 1.0f, 0));
                        projectile.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange) {
                    Turn();
                }

            }
            Vector3 frontLedgeOffset = transform.position + transform.forward * 2.0f + new Vector3(0, 0.5f, 0);
            if (!Physics.Raycast(frontLedgeOffset, Vector3.down, fallRange)) {
                Turn();
            }
        }
    }

    private void Turn() {
        float angle = Random.Range(-110, 110);
        transform.Rotate(0, angle, 0);
    }

    public void SetAlive(bool alive) {
        isAlive = alive;
    }

    public void SetFallDistance(float distance) {
        fallRange = distance;
    }
}