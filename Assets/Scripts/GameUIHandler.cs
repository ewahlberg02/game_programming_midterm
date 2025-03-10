using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    
    public PlayerData PlayerData;
    public UIDocument UIDoc;

    int maxHeal = 3;
    public int currHeal;
    public int currAmmo;
    public int maxAmmo;

    private VisualElement _HealthBarMask;
    private Label _Ammo;
 

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SetHealth(int Health)
    {
        currHeal = Health;
        Debug.Log(currHeal);
        HealthChanged();
    }
    
    public void SetAmmo(int Ammo)
    {
        currAmmo = Ammo;
        AmmoChanged();
    }

    public void setMaxAmmo(int max)
    {
        maxAmmo = max;
        AmmoChanged();
    }

    void HealthChanged()
    {
        _HealthBarMask = UIDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
        
        float healthRatio = (float)currHeal / maxHeal;
        float healthPercent = Mathf.Lerp(0, 100, healthRatio);
        _HealthBarMask.style.width = Length.Percent(healthPercent);

        Debug.Log(healthPercent);
    }

    void AmmoChanged()
    {
         _Ammo = UIDoc.rootVisualElement.Q<Label>("AmmoCount");
         _Ammo.text = $"{currAmmo}/{maxAmmo}";
    }

    private void OnGUI() {
        int size = 36;
        float posX = Camera.main.pixelWidth/2 - size/4;
        float posY = Camera.main.pixelHeight/2 - size/4;

        GUI.Label(new Rect(posX, posY, size, size), "+");
    }
}