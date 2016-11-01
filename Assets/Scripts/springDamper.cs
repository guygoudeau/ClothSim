using UnityEngine;

public class springDamper : MonoBehaviour
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
    }

    public void OnRenderObject()
    {
        GL.Begin(GL.LINES);
        {
            GL.Vertex3(P1.transform.position.x, P1.transform.position.y, P1.transform.position.z);
            GL.Vertex3(P2.transform.position.x, P2.transform.position.y, P2.transform.position.z);
            GL.Color(new Color(0, 1, 0, 1));
        }
        GL.End();
    }
}