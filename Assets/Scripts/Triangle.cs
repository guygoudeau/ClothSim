using UnityEngine;
using System.Collections;

public class Triangle 
{
    public Particle P1, P2, P3; // points of the triangle
    public float p; // density of the air
    public float cd; // coefficient of drag for the triangle
    public float a; // cross sectional area of the triangle
    public float ao; // area of the triangle
    public Vector3 n; // normal of the surface
    public Vector3 v; // relative velocity
    public Vector3 vSurface; // velocity of the triangle
    public Vector3 vAir; // velocity of the air
    public Vector3 fAero; // aerodynamic force

    public Triangle(Particle p1, Particle p2, Particle p3)
    {
        P1 = p1;
        P2 = p2;
        P3 = p3;
    }

    public void AeroForce(float density, float drag, Vector3 airVelocity)
    {
        p = density;
        cd = drag;
        vAir = airVelocity;

        vSurface = (P1.velocity + P2.velocity + P3.velocity) / 3;
        v = vSurface - vAir;
        n = Vector3.Cross(P2.transform.position - P1.transform.position, P3.transform.position - P1.transform.position) /
            Vector3.Cross(P2.transform.position - P1.transform.position, P3.transform.position - P1.transform.position).magnitude;
        ao = 0.5f * Vector3.Cross(P2.transform.position - P1.transform.position, P3.transform.position - P1.transform.position).magnitude;
        a = ao * (Vector3.Dot(v, n) / v.magnitude);

        fAero = -0.5f * p * (v.magnitude * v.magnitude) * cd * a * n;

        P1.addForce(fAero / 3);
        P2.addForce(fAero / 3);
        P3.addForce(fAero / 3);
    }
}
