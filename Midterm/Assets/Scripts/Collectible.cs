using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {   
        // Check if the collided object is the player
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            CollectibleAction(playerController);
        }
    }

    public virtual void CollectibleAction(PlayerController player) {
        return;
    }
}
