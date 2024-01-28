using UnityEngine;

public class InitializePrefabsRandom : MonoBehaviour
{
    public Transform myPrefab;
    private int numberOfInstances = 8; // Number of instances to create

    void Start()
    {
        for (int i = 0; i < numberOfInstances; i++)
        {
            Vector3 spawnPosition = GetNonCollidingPosition();

            if (spawnPosition != Vector3.zero) // Check if a suitable position was found
            {
                Instantiate(myPrefab, spawnPosition, Quaternion.Euler(90, 0, 0));
            }
        }
    }

    Vector3 GetNonCollidingPosition()
    {
        int maxAttempts = 100; // Maximum number of attempts to avoid overlaps
        int currentAttempt = 0;

        while (currentAttempt < maxAttempts)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-15, 15), // X offset
                3f,                   // Y offset
                Random.Range(0, 450)     // Z offset
            );
            Vector3 spawnPosition = transform.position + randomOffset;

            if (!Physics.CheckSphere(spawnPosition, 0.5f)) // 0.5f is the radius of the check
            {
                return spawnPosition; // No collision found, return this position
            }

            currentAttempt++;
        }

        // If all attempts fail, return a default value
        return Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
