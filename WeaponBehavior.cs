using UnityEngine;
using UnityEngine.Rendering;

public class WeaponBehavior : MonoBehaviour
{
    [SerializeField] public GameObject weaponModel;
    [SerializeField] public int _max_ammo = 10;
    [SerializeField] public int _damage = 1;
    [SerializeField] public float _fire_rate = 1.0f;
    [SerializeField] public float _reload_time = 0.5f;

    private ShootMode shootMode;
    private int ammo;
    private float last_fired;

    public void Start() {
        ammo = _max_ammo;
        last_fired = Time.realtimeSinceStartup;
        shootMode = GetComponent<ShootMode>();
        Debug.Log("Hello");
    }

    public void Shoot() {
        if (Time.realtimeSinceStartup - last_fired < _fire_rate) return;

        shootMode.AttackRoutine();
    }

}
