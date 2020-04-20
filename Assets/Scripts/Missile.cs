using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float rotation;
    [SerializeField] private float throwForce;

    private float bottomLine;

    void Start()
    {
        rb.AddTorque(25f);
        bottomLine = GameObject.Find("BottomLine").transform.position.y;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < bottomLine)
        {
            Destroy(gameObject);
        }
    }

}
