using System.Data;
using UnityEngine;

public class Gate : Collectible
{
    [SerializeField] bool isLocked = false;
    [SerializeField] GameObject rotationPoint;
    [SerializeField] GameObject lockObject;
    [SerializeField] AudioClip doorSound;
    [SerializeField] AudioSource audioSource;
    private bool isOpen = false;

    public override void Update()
    {
        return;   
    }

    public override void CollectibleAction(GameObject collidedObject)
    {
        PlayerData playerData = collidedObject.GetComponent<PlayerData>();
        if (playerData != null && !isOpen)
        {
            if (isLocked && playerData.keyCount > 0)
            {
                OpenDoor();
                deleteLock();
                playerData.loseKey();
            } else if (!isLocked)
            {
                OpenDoor();
            }
        }

    }

    public void OpenDoor()
    {
        if (rotationPoint != null)
        {
            gameObject.transform.RotateAround(rotationPoint.transform.position, Vector3.up, -90);
            isOpen = true;
            audioSource.PlayOneShot(doorSound);
        }
    }

    private void deleteLock()
    {
        if (lockObject != null)
        {
            Destroy(lockObject);
        }
    }
}
