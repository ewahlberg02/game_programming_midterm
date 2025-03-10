using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] AudioClip weaponSwapSound;
    [SerializeField] AudioSource audioSource;

    private GameObject _heldWeapon;
    private List<GameObject> _weapons;
    private MeshFilter _mesh;
    private MeshRenderer _renderer;
    private int currentWeaponIndex = 0;
    private GameUIHandler GameUI;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _weapons = new List<GameObject>();
        _mesh = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        GameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUIHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && _heldWeapon /* && !EventSystem.current.IsPointerOverGameObject()*/) {
            _heldWeapon.GetComponent<WeaponBehavior>().Shoot();
        }
        if(Input.GetMouseButtonDown(1) && _heldWeapon) {
            _heldWeapon.GetComponent<WeaponBehavior>().Activate();
            RemoveCurrentWeapon();
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0f) {
            EquipNext();
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f) {
            EquipPrev();
        }
    }

    public void AddWeapon(GameObject weapon) {
        _weapons.Add(Instantiate(weapon, this.gameObject.transform));
        if (_weapons.Count == 1) {
            EquipWeapon(_weapons[0]);
            currentWeaponIndex = 0;
        }
        if (weaponSwapSound != null)
        {
            audioSource.PlayOneShot(weaponSwapSound);
        }        
    }

    public void RemoveCurrentWeapon() {
        _weapons.RemoveAt(currentWeaponIndex);
        Destroy(_heldWeapon);
        _mesh.mesh.Clear();
        GameUI.SetAmmo(0);
        GameUI.setMaxAmmo(0);
        EquipPrev();
    }

    void EquipWeapon(GameObject weapon) {
        if(_heldWeapon) {
            _heldWeapon.SetActive(false);
        }
        _heldWeapon = weapon;
        _heldWeapon.SetActive(true);
        WeaponBehavior behav = _heldWeapon.GetComponent<WeaponBehavior>();
        GameUI.SetAmmo(behav.ammo);
        GameUI.setMaxAmmo(behav._max_ammo);
        _mesh.mesh = behav.weaponModel.GetComponent<MeshFilter>().sharedMesh;
        _renderer.materials = behav.weaponModel.GetComponent<MeshRenderer>().sharedMaterials;
        if (weaponSwapSound != null)
        {
            audioSource.PlayOneShot(weaponSwapSound);
        }
        
    }

    void EquipNext() {
        if(_weapons.Count == 0) {
            return;
        }

        currentWeaponIndex = (currentWeaponIndex < _weapons.Count - 1) ? currentWeaponIndex + 1 : 0;
        EquipWeapon(_weapons[currentWeaponIndex]);
    }

    void EquipPrev() {
        if(_weapons.Count == 0) {
            return;
        }

        currentWeaponIndex = (currentWeaponIndex > 0) ? currentWeaponIndex - 1 : _weapons.Count - 1;
        EquipWeapon(_weapons[currentWeaponIndex]);
    }
}
