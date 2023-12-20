using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePrefabsLine : MonoBehaviour
{
    public Transform myPrefab;
    private int numberOfInstances = 1000; // Number of instances to create

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPosition = transform.position; // Start position for the first instance
        Vector3 direction = transform.forward; // Direction the GameObject is facing

        for (int i = 0; i < numberOfInstances; i++)
        {
            // Calculate position for each instance along the direction the GameObject is facing
            Vector3 spawnPosition = startPosition + direction * (i * 40); // 40 units apart

            // Instantiate prefab at the calculated position
            Instantiate(myPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
