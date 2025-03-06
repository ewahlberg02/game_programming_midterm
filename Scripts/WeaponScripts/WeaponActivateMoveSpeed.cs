using UnityEngine;

public class WeaponActivateMoveSpeed : MonoBehaviour, IWeaponActivate
{
    [SerializeField] float bonus_speed = 1.5f;
    [SerializeField] float duration = 3f;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Activate() {
        player.SendMessage("Speedup", new Vector2(bonus_speed, duration), SendMessageOptions.DontRequireReceiver);
    }
}
