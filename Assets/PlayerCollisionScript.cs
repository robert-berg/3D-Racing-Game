using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisionScript : MonoBehaviour
{

    private int collectableCount = 0; // Zähler für gesammelte Collectables
    public Text collectableCountDisplay;

    // Start is called before the first frame update
    void Start()
    {
        collectableCountDisplay.text = "Collectables: 0";
    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnTriggerEnter wird aufgerufen, wenn der Spieler mit einem Objekt kollidiert
    private void OnTriggerEnter(Collider other)
    {

        // Überprüft, ob das kollidierende Objekt den Tag "toDestroy" hat
        if (other.gameObject.CompareTag("toDestroy"))
        {
            Destroy(other.gameObject); // Zerstört das kollidierende Objekt
            if (collectableCountDisplay != null)
            {
                collectableCountDisplay.text = "Collectables: " +  ++collectableCount;
            }
        }
    }
}