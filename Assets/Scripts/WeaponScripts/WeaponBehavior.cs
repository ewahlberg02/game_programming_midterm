using UnityEngine;
using UnityEngine.Rendering;


[RequireComponent(typeof(IWeaponActivate))]
public class WeaponBehavior : MonoBehaviour
{
    [SerializeField] public GameObject weaponModel;
    [SerializeField] public ParticleSystem muzzleFlash;
    [SerializeField] public int _max_ammo = 10;
    [SerializeField] public int _damage = 1;
    [SerializeField] public float _fire_rate = 1.0f;
    [SerializeField] public float _reload_time = 0.5f;
    [SerializeField] AudioClip weaponFireSound;
    [SerializeField] AudioClip weaponReloadSound;
    

    private ShootMode shootMode;
    private IWeaponActivate weaponActivation;
    private AudioSource audioSource;
    private GameUIHandler GameUI;
    
    public int ammo;
    private float last_fired;

    public void Awake()
    {
        weaponActivation = GetComponent<IWeaponActivate>();
        ammo = _max_ammo;
    }

    public void Start() {
        
        GameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUIHandler>();

        last_fired = Time.realtimeSinceStartup;
        shootMode = GetComponent<ShootMode>();
        audioSource = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();
    }

    public void Shoot() {
        if (Time.realtimeSinceStartup - last_fired < _fire_rate) return;

        if (ammo <= 0) {
            last_fired = Time.realtimeSinceStartup + _reload_time;
            ammo = _max_ammo;
            GameUI.SetAmmo(ammo);
            if (weaponReloadSound != null)
            {
                audioSource.PlayOneShot(weaponReloadSound);
            } 
            return;
        }

        last_fired = Time.realtimeSinceStartup;
        ammo--;
        GameUI.SetAmmo(ammo);

        muzzleFlash.Play();
        if (weaponFireSound != null)
        {
            audioSource.PlayOneShot(weaponFireSound);
        } 
        
        shootMode.AttackRoutine(_damage);
    }

    public void Activate() {
        Debug.Log(weaponActivation);
        weaponActivation.Activate();
    }
}
