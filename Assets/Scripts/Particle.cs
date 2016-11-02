using UnityEngine;
using System.Collections.Generic;

public class Particle : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 force;
    public Vector3 gravity = new Vector3(0, -10, 0);
    public Vector3 gravForce;
    public float mass = 3;
    public List<Particle> neighbors;

    void Update()
    {
        acceleration = (1f / mass) * force;
        velocity += acceleration * Time.fixedDeltaTime;
        position += velocity * Time.fixedDeltaTime;

        foreach(Particle p in neighbors)
        {
            Debug.DrawLine(transform.position, p.transform.position, Color.green);
        }
    }

    public void addGravity()
    {
        gravForce = gravity * mass;
        addForce(gravForce);
    }

    public void addForce(Vector3 Force)
    {
        force += Force;
    }
}