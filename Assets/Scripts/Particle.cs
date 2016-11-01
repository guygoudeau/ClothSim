using UnityEngine;
using System.Collections.Generic;

public class Particle : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 Force;
    public float mass = 3;
    public List<Particle> neighbors;

    void Update()
    {
        acceleration = (1f / mass) * Force;
        velocity += acceleration * Time.fixedDeltaTime;
        position += velocity * Time.fixedDeltaTime;

        foreach(Particle p in neighbors)
        {
            Debug.DrawLine(transform.position, p.transform.position, Color.green);
        }
    }

    public void addForce(Vector3 force)
    {
        Force += force;
    }
}