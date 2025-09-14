using UnityEngine;

public class CameraBezierPath : MonoBehaviour
{
    public Transform[] controlPoints; // 4 points pour une Bézier cubique
    public float duration = 20f; // durée du trajet
    private float t = 0f;

    void Update()
    {
        if (t < 1f)
        {
            t += Time.deltaTime / duration;
            Vector3 pos = CalculateCubicBezierPoint(t, controlPoints[0].position, controlPoints[1].position, controlPoints[2].position, controlPoints[3].position);
            transform.position = pos;

            // Faire regarder la caméra vers l'avant de la courbe
            Vector3 forward = CalculateCubicBezierPoint(t + 0.01f, controlPoints[0].position, controlPoints[1].position, controlPoints[2].position, controlPoints[3].position) - pos;
            transform.rotation = Quaternion.LookRotation(forward);
        }
    }

    // Formule Bézier cubique
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        return uuu * p0 +
               3 * uu * t * p1 +
               3 * u * tt * p2 +
               ttt * p3;
    }
}
