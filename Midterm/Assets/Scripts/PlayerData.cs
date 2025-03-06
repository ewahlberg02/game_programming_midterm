using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public int playerScore;
    public int keyCount;

    void Start()
    {
        currentHealth = 3;
    }
    
    public void damagePlayer()
    {
        currentHealth -= 1;
    }
    public void healPlayer()
    {
        currentHealth += 1;
    }
    public void gainKey()
    {
        keyCount +=1;
    }
    public void loseKey()
    {
        if (keyCount > 0)
        {
            keyCount -= 1;
        }else{
            Debug.Log("No Keys!");
        }
    }
}
