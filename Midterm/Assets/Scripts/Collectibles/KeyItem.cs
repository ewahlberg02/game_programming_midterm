using UnityEngine;

public class KeyItem : Collectible
{
    public override void CollectibleAction(GameObject collidedObject)
    {
        if (collidedObject.GetComponent<PlayerData>() != null)
        {
            PlayerData playerData = collidedObject.GetComponent<PlayerData>();
            playerData.gainKey();
            Debug.Log("Got Key");
            Destroy(gameObject);
        }
    }
}
