using UnityEngine;

public class WeaponActivateJump : MonoBehaviour, IWeaponActivate
{
    [SerializeField] float jump_strength = 10f;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Activate() {
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().AddForce(Vector3.up * jump_strength, ForceMode.Impulse);
    }
}
