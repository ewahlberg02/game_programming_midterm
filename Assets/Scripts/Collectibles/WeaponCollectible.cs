using UnityEngine;
using System.Collections.Generic;

public class WeaponCollectible : Collectible
{
    [SerializeField] GameObject weaponPrefab;

    public override void CollectibleAction(GameObject collidedObject)
    {
        WeaponHandler weaponHandler = collidedObject.GetComponentInChildren<WeaponHandler>();
        if (weaponPrefab != null)
        {
            weaponHandler.SendMessage("AddWeapon", weaponPrefab, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }        
    }
}
