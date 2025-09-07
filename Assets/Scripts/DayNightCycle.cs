using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Sun Settings")]
    public Light sun;                // Directional Light (le Soleil)
    public float dayLength = 120f;   // Durée d’un cycle jour/nuit en secondes (ici 2 minutes)
    public float maxIntensity = 1f;  // Intensité maximale du soleil
    public float minIntensity = 0f;  // Intensité minimale (nuit)

    private float timeOfDay = 0f;    // 0 -> minuit, 12 -> midi, 24 -> fin du cycle
    private float sunInitialRotationX;

    void Start()
    {
        if (sun == null)
        {
            Debug.LogError("⚠️ Assigne le Directional Light (Soleil) dans l'inspecteur !");
            enabled = false;
            return;
        }

        // Sauvegarde l’angle initial pour éviter des décalages
        sunInitialRotationX = sun.transform.rotation.eulerAngles.x;
    }

    void Update()
    {
        // Temps qui avance
        timeOfDay += (24f / dayLength) * Time.deltaTime;
        if (timeOfDay >= 24f) timeOfDay = 0f; // remet à 0 après 24h virtuelles

        // Rotation du Soleil (cycle sur 360°)
        float sunAngle = (timeOfDay / 24f) * 360f;
        sun.transform.rotation = Quaternion.Euler(sunAngle + sunInitialRotationX, 170f, 0f);

        // Intensité de la lumière (sinus pour transition douce)
        float intensityMultiplier = Mathf.Clamp01(Mathf.Sin(timeOfDay / 24f * Mathf.PI));
        sun.intensity = Mathf.Lerp(minIntensity, maxIntensity, intensityMultiplier);
    }
}
