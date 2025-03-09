using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class ShootRaycast : MonoBehaviour, ShootMode
{
    [SerializeField] ParticleSystem hitParticles;
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackRoutine(int _damage) {
        Vector3 point = new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0);
        Ray ray = cam.ScreenPointToRay(point);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Debug.Log("Hit something");
            GameObject hitObject = hit.transform.gameObject;
            if (hitParticles != null) {
                var mainVFX = hitParticles.main;
                Renderer rend = hitObject.GetComponent<Renderer>();

                if (rend && rend.sharedMaterial.color != null) {
                    mainVFX.startColor = rend.sharedMaterial.color;
                }
                else {
                    mainVFX.startColor = Color.white;
                }

                ParticleSystem particles = Instantiate(hitParticles, hit.point, quaternion.identity);
            }

            ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

            if (target != null) {
                target.ReactToHit(_damage);
            }
            
        }
    }
}
