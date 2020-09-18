using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Reticle : MonoBehaviour
{
    [SerializeField] private float AnimTime = .5f;

    private float timeElapsed = 0.0f;
    private Vector3 startScale;

    private void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Pulsate();
    }

    /// <summary>
    /// Exhibit a pulsating behavior.
    /// </summary>
    private void Pulsate()
    {
        timeElapsed += Time.deltaTime;

        // Slowly lower scale to 80% of it's original scale. 
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            new Vector3(transform.localScale.x * .8f, transform.localScale.y * .8f, transform.localScale.z * .8f),
            Time.deltaTime / AnimTime);

        if (timeElapsed >= AnimTime)
        {
            // Return/snap back to original size.
            timeElapsed = 0;
            transform.localScale = startScale;
        }
    }
}
