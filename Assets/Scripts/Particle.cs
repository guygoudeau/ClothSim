using UnityEngine;
using System.Collections.Generic;

public class Particle : MonoBehaviour
{
    public Vector3 position = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 force = Vector3.zero;
    public float mass;
    public bool anchor;
    public List<Particle> neighbors;

    public Vector3 updateParticle()
    {
        foreach(Particle p in neighbors)
        {
            Debug.DrawLine(transform.position, p.transform.position, Color.green);
        }

        acceleration = (1f / mass) * force;
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        return position;
    }

    public void addForce(Vector3 Force)
    {
        force += Force;
    }
}