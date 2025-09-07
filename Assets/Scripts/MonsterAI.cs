using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class MonsterAI : MonoBehaviour
{
    public int vie = 100;
    public int forceAttaque = 10;
    public Animator animator;
    private Rigidbody rb;
    public Transform playerTransform;
    public float tempsEntreAttaques = 1.5f;
    private float prochainAttaque = 0f;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (playerTransform == null) return;

        // Calcule la direction vers le joueur
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0f; // on ignore la hauteur pour éviter de pencher le monstre

        rb.angularVelocity = Vector3.zero;

        direction.y = 0f;

        direction.Normalize();

        if (direction.magnitude > 0.1f)
        {
            transform.forward = direction;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Attaquer si cooldown terminé
        if (other.gameObject.CompareTag("Player") && Time.time >= prochainAttaque)
        {
            animator.SetTrigger("Attack");

            PlayerStats playerStats = playerTransform.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.PrendreDegats(forceAttaque);
            }

            prochainAttaque = Time.time + tempsEntreAttaques;
        }
    }
}
