using UnityEngine;

public class JumpRecharge : Collectible
{
    public override void CollectibleAction(PlayerController player){
            player.ResetDoubleJump();
            Debug.Log("Double Jump Reset");
            Destroy(gameObject);
    }
}
