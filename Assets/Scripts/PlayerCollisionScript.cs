using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisionScript : MonoBehaviour
{
    private int collectableCount = 0; // Counter for collected collectables
    private Text collectableCountDisplay;

    [SerializeField]
    private ParticleSystem smoke;

    // Start is called before the first frame update
    void Start()
    {
        GameObject textObject = GameObject.Find("CollectableText");
        if (textObject != null)
        {
            collectableCountDisplay = textObject.GetComponent<Text>();
            if (collectableCountDisplay != null)
            {
                collectableCountDisplay.text = "Collectables: 0";
            }
        }
    }

    // OnTriggerEnter is called when the player collides with an object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the tag "toDestroy"
        if (other.gameObject.CompareTag("toDestroy"))
        {
            Destroy(other.gameObject); // Destroy the colliding object
            if (collectableCountDisplay != null)
            {
                collectableCountDisplay.text = "Collectables: " + ++collectableCount;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        smoke.Play();
        Debug.Log("Collided.");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
