using System.Threading;
using UnityEngine;

public class JumpRecharge : Collectible
{
    public override void CollectibleAction(GameObject collidedObject){
        if (collidedObject.GetComponent<PlayerController>() != null)
        {
            PlayerController player = collidedObject.GetComponent<PlayerController>();
            player.ResetDoubleJump();
            Debug.Log("Double Jump Reset");
            Destroy(gameObject);
        }
    }
}
