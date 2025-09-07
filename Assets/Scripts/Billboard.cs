using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main; // récupère la caméra principale
    }

    void LateUpdate()
    {
        if (cam != null)
        {
            // Le canvas regarde toujours dans la direction de la caméra
            transform.LookAt(transform.position + cam.transform.forward);
        }
    }
}
