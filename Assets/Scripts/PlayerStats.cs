using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float vie = 100f;
    public HealthBar healthBar; // assigné dans l’inspecteur
    public Animator animator;

    void Start()
    {
        healthBar.SetMaxHealth(vie);
    }

    public void PrendreDegats(float degats)
    {
        vie -= degats;
        healthBar.SetHealth(vie);

        if (vie <= 0)
        {
            Debug.Log("Player mort !");
            animator.SetTrigger("death");
        }
        else
        {
            animator.SetTrigger("damage");
        }
    }
}
