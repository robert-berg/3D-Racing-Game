using UnityEngine;

public class MagnetismEffect : MonoBehaviour
{
    public string targetTag = "magnetic";
    public float pullRadius = 5f;
    public float pullForce = 100f;

    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pullRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag(targetTag))
            {
                Vector3 pullDirection = (collider.transform.position - transform.position).normalized;
                float distance = Vector3.Distance(collider.transform.position, transform.position);
                float newPullForce = Mathf.Lerp(pullForce, 0, distance / pullRadius);
                GetComponent<Rigidbody>().AddForce(pullDirection * newPullForce);
            }
        }
    }
}
