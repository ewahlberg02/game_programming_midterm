using UnityEngine;
using System.Collections;

public class WeaponActivateWarp : MonoBehaviour, IWeaponActivate
{
    [SerializeField] float warp_amount = 10.0f;
    private GameObject player;
    private float min_warp = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Activate() {
        Vector3 direction = new Vector3(player.transform.forward.x, 0, player.transform.forward.z);
        
        Ray ray = new Ray(Camera.main.transform.position, direction);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, warp_amount)) {
            if(hit.distance >= min_warp) {
                
                player.transform.Translate(direction * hit.distance * 0.8f, Space.World);
            }
        }
        else {
            player.transform.Translate(direction * warp_amount, Space.World);
        }
    }
}
