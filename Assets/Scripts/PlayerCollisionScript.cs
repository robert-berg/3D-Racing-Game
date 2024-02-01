using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
                StartCoroutine(AnimateScaleOverTime(0.5f, 1.2f));
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

    IEnumerator AnimateScaleOverTime(float duration, float scaleFactor)
    {
        Vector3 originalScale = Vector3.one;
        Vector3 targetScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        float halfDuration = duration / 2f;



        // First half: Scale up
        float currentTime = 0f;
        while (currentTime < halfDuration)
        {
            float t = currentTime / halfDuration; // Normalize time to [0, 1]
            collectableCountDisplay.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            currentTime += Time.deltaTime;
            yield return null;
        }


        // Second half: Scale down
        currentTime = 0f;
        while (currentTime < halfDuration)
        {
            float t = currentTime / halfDuration; // Normalize time to [0, 1]
            collectableCountDisplay.transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            currentTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the scale is set back to original (Vector3.one) after the animation
        collectableCountDisplay.transform.localScale = Vector3.one;
    }

}
