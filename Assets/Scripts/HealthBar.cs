using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image barreVie; // assigner l'image rouge
    private float maxHealth;

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        barreVie.fillAmount = 1f;
    }

    public void SetHealth(float health)
    {
        barreVie.fillAmount = health / maxHealth;
    }
}
