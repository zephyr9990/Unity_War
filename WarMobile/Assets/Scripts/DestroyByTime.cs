using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField] private float time = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);   
    }
}
