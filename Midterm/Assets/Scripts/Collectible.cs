using UnityEngine;

public class Collectible : MonoBehaviour
{
    
    public void Update()
    {
        transform.Rotate(0, 25 * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {   
        // Pass the collided object to the collectible action method
        GameObject collidedObject = other.gameObject;
        CollectibleAction(collidedObject);
    }

    public virtual void CollectibleAction(GameObject collidedObject) {
        return;
    }
}
