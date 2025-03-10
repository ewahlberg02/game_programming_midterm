using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] GameUIHandler gameUI;

    public int maxHealth = 3;
    public int currentHealth;
    public int playerScore;
    public int keyCount;

    void Start()
    {
        currentHealth = 3;
        gameUI.SetHealth(currentHealth);
    }
    
    public void Update()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
    public void damagePlayer()
    {
        currentHealth -= 1;
        gameUI.SetHealth(currentHealth);
    }
    public void healPlayer()
    {
        currentHealth += 1;
        gameUI.SetHealth(currentHealth);
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
