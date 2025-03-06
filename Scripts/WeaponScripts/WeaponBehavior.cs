using UnityEngine;
using UnityEngine.Rendering;


[RequireComponent(typeof(IWeaponActivate))]
public class WeaponBehavior : MonoBehaviour
{
    [SerializeField] public GameObject weaponModel;
    [SerializeField] public int _max_ammo = 10;
    [SerializeField] public int _damage = 1;
    [SerializeField] public float _fire_rate = 1.0f;
    [SerializeField] public float _reload_time = 0.5f;

    private ShootMode shootMode;
    private IWeaponActivate weaponActivation;
    
    private int ammo;
    private float last_fired;

    public void Awake()
    {
        weaponActivation = GetComponent<IWeaponActivate>();
    }

    public void Start() {
        ammo = _max_ammo;
        last_fired = Time.realtimeSinceStartup;
        shootMode = GetComponent<ShootMode>();
    }

    public void Shoot() {
        if (Time.realtimeSinceStartup - last_fired < _fire_rate) return;

        if (ammo <= 0) {
            last_fired = Time.realtimeSinceStartup + _reload_time;
            ammo = _max_ammo;
            return;
        }

        last_fired = Time.realtimeSinceStartup;
        ammo--;

        shootMode.AttackRoutine();
    }

    public void Activate() {
        Debug.Log(weaponActivation);
        weaponActivation.Activate();
    }
}
