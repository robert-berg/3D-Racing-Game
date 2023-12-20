using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePrefabsRandom : MonoBehaviour
{
    public Transform myPrefab;
    private int numberOfInstances = 10; // Anzahl der zu erzeugenden Instanzen

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfInstances; i++)
        {
            Vector3 spawnPosition = GetNonCollidingPosition();

            if (spawnPosition != Vector3.zero) // Überprüfe, ob eine geeignete Position gefunden wurde
            {
                // Instanz erzeugen
                Instantiate(myPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetNonCollidingPosition()
    {
        int maxAttempts = 100; // Maximalanzahl der Versuche, um Überlappungen zu vermeiden
        int currentAttempt = 0;

        while (currentAttempt < maxAttempts)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-25, 25), // X-Position
                2, // Y-Position
                Random.Range(-25, 25)  // Z-Position
            );

            if (!Physics.CheckSphere(spawnPosition, 0.5f)) // 0.5f ist der Radius der Prüfung
            {
                return spawnPosition; // Keine Kollision gefunden, gib diese Position zurück
            }

            currentAttempt++;
        }

        // Falls alle Versuche fehlschlagen, gib einen Standardwert zurück
        return Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }
}