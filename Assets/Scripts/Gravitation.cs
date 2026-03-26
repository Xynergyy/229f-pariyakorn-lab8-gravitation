using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Gravitation : MonoBehaviour
{
    //Create a List objects in the galaxy to attract
    public static List<Gravitation> otherObj;
    private Rigidbody rb;
    const float G = 6.67f;

    //set speed fot orbiting
    [SerializeField] bool planet = false; //if not a planet -> orbit
    [SerializeField] int orbitSpeed = 1000000;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //Create a List for the first time
        rb = GetComponent<Rigidbody>();
        if (otherObj == null )
        {
            otherObj = new List<Gravitation>();
        }

        //Add object (with gravitation script) to attract to the list
        otherObj.Add( this );

        //orbiting
        if ( !planet )
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
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
