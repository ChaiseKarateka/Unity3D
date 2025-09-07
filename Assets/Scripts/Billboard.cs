using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main; // r�cup�re la cam�ra principale
    }

    void LateUpdate()
    {
        if (cam != null)
        {
            // Le canvas regarde toujours dans la direction de la cam�ra
            transform.LookAt(transform.position + cam.transform.forward);
        }
    }
}
