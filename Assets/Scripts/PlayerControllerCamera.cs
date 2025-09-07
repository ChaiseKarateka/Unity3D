using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerCamera : MonoBehaviour
{
    public float moveSpeed = 5f;      // vitesse de déplacement
    public float jumpForce = 5f;      // force de saut
    public Transform cameraTransform; // référence à la caméra

    private Rigidbody rb;
    private bool isGrounded;
    private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>(); // récupère l'Animator du modèle enfant

    }

    void Update()
    {
        // --- Mouvement ---
        float horizontal = Input.GetAxis("Horizontal"); // A/D ou ← →
        float vertical = Input.GetAxis("Vertical");     // W/S ou ↑ ↓

        // vecteur relatif à la caméra
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // On ignore l’axe Y de la caméra (sinon le joueur flotte si on regarde vers le haut)
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

        // Déplacer le joueur
        Vector3 velocity = moveDirection * moveSpeed;
        Vector3 currentVelocity = rb.linearVelocity;
        rb.linearVelocity = new Vector3(velocity.x, currentVelocity.y, velocity.z);
        rb.angularVelocity = Vector3.zero;

        // Tourner le joueur vers sa direction de déplacement
        if (moveDirection.magnitude > 0.1f)
        {
            transform.forward = moveDirection;
        }

        // 🎬 Animation : Idle ↔ Run
        animator.SetFloat("Speed", moveDirection.magnitude);

        // 🎬 Animation : Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("Ground", false);
            animator.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Vérifie si on touche le sol
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("Ground", true);
        }
    }
}
