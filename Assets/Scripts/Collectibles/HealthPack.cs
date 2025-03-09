using UnityEngine;

public class HealthPack : Collectible
{
    public override void CollectibleAction(GameObject collidedObject)
    {
        if (collidedObject.GetComponent<PlayerData>() != null)
        {
            PlayerData playerData = collidedObject.GetComponent<PlayerData>();
            playerData.healPlayer();
            Debug.Log("Player Healed");
            Destroy(gameObject);
        }
    }
}
