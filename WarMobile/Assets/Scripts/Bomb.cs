using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private ParticleSystem particles;
    private void Update()
    {
        // Go downwards towards the floor.
        transform.Translate(Vector3.down * Time.deltaTime * speed, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        // On death, spawn particles.
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
