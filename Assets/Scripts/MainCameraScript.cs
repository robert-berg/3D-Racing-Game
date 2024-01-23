using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public Transform target;
    private Vector3 initialOffset;
    public float rotationSpeed = 2.0f; // Geschwindigkeit der Kamera-Rotation

    // Start is called before the first frame update
    void Start()
    {
        initialOffset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotiere den Offset basierend auf der aktuellen Rotation des Spielers
        Vector3 offsetRotated = target.rotation * initialOffset;

        // Aktualisiere die Kameraposition
        transform.position = target.position + offsetRotated;

        // Schau auf den Spieler
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);

        // Zur Zielrotation interpolieren
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}