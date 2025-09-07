using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform player;        // Cible (le Player)
    public Transform cameraTransform; // Référence à la caméra (child)

    public float distance = 5f;     // Distance par défaut
    public float minDistance = 2f;  // Zoom minimum
    public float maxDistance = 10f; // Zoom maximum

    public float rotationSpeed = 3f;  // Sensibilité de la souris
    public float zoomSpeed = 2f;

    public float minY = -20f; // Limite basse (ne pas passer sous le sol)
    public float maxY = 60f;  // Limite haute (ne pas voir au-dessus de la tête)

    private float currentX = 0f; // Rotation horizontale
    private float currentY = 20f; // Rotation verticale

    void LateUpdate()
    {
        // --- Zoom avec molette ---
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // --- Rotation avec souris ---
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) // clic gauche ou droit
        {
            currentX += Input.GetAxis("Mouse X") * rotationSpeed;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            currentY = Mathf.Clamp(currentY, minY, maxY);
        }

        // --- Position de la caméra ---
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 dir = new Vector3(0, 0, -distance);
        cameraTransform.position = player.position + rotation * dir;
        cameraTransform.LookAt(player.position + Vector3.up * 1.0f); // focus sur le centre du Player

        // --- Si clic droit : orienter aussi le Player ---
        if (Input.GetMouseButton(1))
        {
            Vector3 forward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
            if (forward.sqrMagnitude > 0.01f)
            {
                player.forward = forward; // le Player tourne avec la caméra
            }
        }
    }
}
