using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject default_weapon;
    [SerializeField] GameObject default_weapon2;
    [SerializeField] GameObject default_weapon3;

    private GameObject _heldWeapon;
    private List<GameObject> _weapons;
    private MeshFilter _mesh;
    private MeshRenderer _renderer;
    private int currentWeaponIndex = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _weapons = new List<GameObject>();
        _mesh = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();

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

        if(Input.GetButtonUp("Fire3")) {
            AddWeapon(default_weapon);
            AddWeapon(default_weapon2);
            AddWeapon(default_weapon3);
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
    }

    public void RemoveCurrentWeapon() {
        _weapons.RemoveAt(currentWeaponIndex);
        Destroy(_heldWeapon);
        _mesh.mesh.Clear();
        EquipPrev();
    }

    void EquipWeapon(GameObject weapon) {
        if(_heldWeapon) {
            _heldWeapon.SetActive(false);
        }
        _heldWeapon = weapon;
        _heldWeapon.SetActive(true);
        _mesh.mesh = _heldWeapon.GetComponent<WeaponBehavior>().weaponModel.GetComponent<MeshFilter>().sharedMesh;
        _renderer.materials = _heldWeapon.GetComponent<WeaponBehavior>().weaponModel.GetComponent<MeshRenderer>().sharedMaterials;
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
