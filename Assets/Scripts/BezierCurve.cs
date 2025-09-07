using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{
    public Transform p0; // Point de départ
    public Transform p1; // Point de contrôle 1
    public Transform p2; // Point de contrôle 2 (quadratique & cubique)
    public Transform p3; // Point de contrôle 3 (seulement pour cubique)

    public int resolution = 30; // nombre de segments de la courbe
    public bool isCubic = false; // si false → quadratique, si true → cubique

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        DrawCurve();
    }

    void DrawCurve()
    {
        lineRenderer.positionCount = resolution + 1;

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector3 position;

            if (isCubic)
                position = GetCubicPoint(t);
            else
                position = GetQuadraticPoint(t);

            lineRenderer.SetPosition(i, position);
        }
    }

    // Bézier quadratique (3 points)
    Vector3 GetQuadraticPoint(float t)
    {
        return Mathf.Pow(1 - t, 2) * p0.position +
               2 * (1 - t) * t * p1.position +
               Mathf.Pow(t, 2) * p2.position;
    }

    // Bézier cubique (4 points)
    Vector3 GetCubicPoint(float t)
    {
        return Mathf.Pow(1 - t, 3) * p0.position +
               3 * Mathf.Pow(1 - t, 2) * t * p1.position +
               3 * (1 - t) * Mathf.Pow(t, 2) * p2.position +
               Mathf.Pow(t, 3) * p3.position;
    }
}
