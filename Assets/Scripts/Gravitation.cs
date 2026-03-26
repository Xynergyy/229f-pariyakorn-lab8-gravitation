using UnityEngine;
using System.Collections.Generic;

public class Gravitation : MonoBehaviour
{

    public static List<Gravitation> otherObj;
    private Rigidbody rb;
    const float G = 6.67f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObj == null )
        {
            otherObj = new List<Gravitation>();
        }
        otherObj.Add( this );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Gravitation obj in otherObj)
        {
            if (obj != this) // Prevent the object from attracting itself
            {
                Attract(obj);
            }
            
        }
    }
    void Attract(Gravitation other)
    {
        Rigidbody otherRb = other.rb; // Get mass m
        Vector3 direction = rb.position - otherRb.position; // Direction from object M to m

        float distance = direction.magnitude; // Find the distance r
        if ( distance == 0f) // Prevent gravity from occurring (at zero distance)
            return;

        // F = G(M1 * M2) / r^2
        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);
        Vector3 gravitationForce = forceMagnitude * direction.normalized; // Add direction to forceMagnitude 
        otherRb.AddForce(gravitationForce); // Add Gravity to Object
    }
}
