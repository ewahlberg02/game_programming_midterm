using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] Material baseMaterial;
    [SerializeField] Material damageMaterial;

    [SerializeField] int maxHealth = 20;
    private int health;
    private bool isActive;
    private MeshRenderer meshRen;

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        health = maxHealth;
        meshRen = GetComponent<MeshRenderer>();
        if(!meshRen) {
            meshRen = GetComponentInChildren<MeshRenderer>();
        }
        meshRen.material = baseMaterial;
    }


    public void ReactToHit(int _damage) {
        if (!isActive) return;

        health -= _damage;

        visualizeDamage();

        if (health <= 0) {
            WanderingAI behavior = GetComponent<WanderingAI>();
            if (behavior) {
                StartCoroutine(Die());
            }
            else {
                Destroy(gameObject);
            }
        }
    }

    private void visualizeDamage() {
        
        float healthLerp = Mathf.Lerp(1, 0, health/(float)maxHealth);
        meshRen.material.Lerp(baseMaterial, damageMaterial, healthLerp);
    }

    public IEnumerator Die() {

        GetComponent<WanderingAI>().SetAlive(false);

        isActive = false;
        this.transform.Rotate(-75, 0, 0);
        Collider col = GetComponent<Collider>();
        if(col) {
            Destroy(col);
        }

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }

    public void SetStartHealth(int startingMaxHealth) {
        maxHealth = startingMaxHealth;
        health = startingMaxHealth;
    }
}