using UnityEngine;

public class springDamper
{
    public float springConstant; // Ks, force that brings it back
    public float dampingFactor; // Kd, force that limits how hard it comes back
    public float restLength; // l0, default starting length while at rest
    public Particle P1, P2;

    public void computeForce()
    {
        Vector3 Dist = P2.position - P1.position; // e*
        Vector3 normDist = Dist / Dist.magnitude; // e, where l = Dist.magnitude

        float v1 = Vector3.Dot(normDist, P1.velocity);
        float v2 = Vector3.Dot(normDist, P2.velocity);

        float springDamperForce = (-springConstant * (restLength - Dist.magnitude)) - (dampingFactor * (v1 - v2));

        Vector3 force1 = springDamperForce * normDist;
        Vector3 force2 = -force1;

        P1.addForce(force1);
        P2.addForce(force2);
    }

    public springDamper(Particle p1, Particle p2, float sC, float dF, float rL)
    {
        P1 = p1;
        P2 = p2;
        springConstant = sC;
        dampingFactor = dF;
        restLength = rL;
    }
}