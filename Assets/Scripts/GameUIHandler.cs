using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    
    public PlayerData PlayerData;
    public UIDocument UIDoc;

    int maxHeal = 3;
    public int currHeal;

    private VisualElement _HealthBarMask;
 

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SetHealth(int Health)
    {
        currHeal = Health;
        Debug.Log(currHeal);
        HealthChanged();
    }

    void HealthChanged()
    {
        _HealthBarMask = UIDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
        
        float healthRatio = (float)currHeal / maxHeal;
        float healthPercent = Mathf.Lerp(8, 88, healthRatio);
        _HealthBarMask.style.width = Length.Percent(healthPercent);

        Debug.Log(healthPercent);
    }
}