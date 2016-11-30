using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class clothSim : MonoBehaviour
{
    List<Particle> particleList;
    List<springDamper> springDamperList;
    List<springDamper> tempSpringDamperList;
    List<Triangle> triangleList;
    List<Triangle> tempTriangleList;
    List<GameObject> lineList;
    List<GameObject> tempLineList;
    public GameObject particlePrefab;
    public GameObject linePrefab;
    public Material lineMaterial;
    public int width;
    public int height;
    [Range(0, 100)]
    public float springConstant;
    [Range(0, 10)]
    public float dampingFactor;
    [Range(0, 5)]
    public float restLength;
    [Range(-10, 10)]
    public float gravity;
    [Range(-5, 5)]
    public float airVelocity;
    public float airDensity = 1;
    public float dragCoefficient = 1;

    void Start()
    {
        particleList = new List<Particle>();
        springDamperList = new List<springDamper>();
        tempSpringDamperList = new List<springDamper>();
        triangleList = new List<Triangle>();
        tempTriangleList = new List<Triangle>();
        lineList = new List<GameObject>();
        tempLineList = new List<GameObject>();

        spawnParticles();
        setNeighborsAndDampers();
        setTriangles();

        foreach (Particle p in particleList) // set starting position values for every particle
        {
            p.position = p.transform.position;
        }
    }

    void Update()
    {
        updateForces();
    }    

    public void spawnParticles() // creates grid of particles
    {
        float xPos = 0f; // for determining position of particles on x axis 
        float yPos = 0f; // for determining position of particles on y axis 

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject particle = Instantiate(particlePrefab, new Vector3(xPos, yPos, 0), new Quaternion()) as GameObject; // spawn particle prefab
                particleList.Add(particle.GetComponent<Particle>()); // add that particle to the particle list
                xPos += 2; // x margin of 2 between particles
            }
            yPos += 2; // y margin of 2 between particles
            xPos = 0; // reset x for next loop
        }

        particleList[particleList.Count - 1].anchor = true; // set top right anchor
        particleList[particleList.Count - width].anchor = true; // set top left anchor
    }

    public void updateForces()
    {
        tempSpringDamperList = new List<springDamper>(); // reset the temporary line list
        foreach (springDamper sd in springDamperList) // add every spring damper in the spring damper list to temporary spring damper list
        {
            tempSpringDamperList.Add(sd);
        }

        tempTriangleList = new List<Triangle>(); // reset the temporary line list
        foreach (Triangle t in triangleList) // add every triangle in the triangle list to temporary triangle list
        {
            tempTriangleList.Add(t);
        }

        tempLineList = new List<GameObject>(); // reset the temporary line list
        foreach (GameObject l in lineList) // add every line in the line list to temporary line list
        {
            tempLineList.Add(l);
        }

        foreach (Particle p in particleList) // apply gravity to every particle
        {
            p.force = (gravity * Vector3.down) * p.mass;
        }

        foreach (springDamper sd in springDamperList) // compute and apply forces to every spring damper
        {
            sd.dampingFactor = dampingFactor;
            sd.springConstant = springConstant;
            sd.restLength = restLength;
            sd.computeForce();
        }

        foreach (Triangle t in triangleList) // compute and apply aerodynamic force to every triangle
        {
            if (airVelocity != 0)
            {
                t.AeroForce(airDensity, dragCoefficient, airVelocity * Vector3.forward);
            }
        }

        foreach (Particle p in particleList) // update the position of every particle that isn't an anchor
        {
            if (p.anchor == false)
            {
                p.transform.position = p.updateParticle();
            }
            else
            {
                p.position = p.transform.position;
            }
        }

        for (int i = 0; i < tempLineList.Count; i++) // set line positions, material and width
        {
            LineRenderer line = tempLineList[i].GetComponent<LineRenderer>();
            line.SetPosition(0, springDamperList[i].P1.position);
            line.SetPosition(1, springDamperList[i].P2.position);
            line.material = lineMaterial;
            line.SetWidth(.2f, .2f);
        }
    }

    public void setNeighborsAndDampers()
    {
        foreach (Particle p in particleList) 
        {
            p.neighbors = new List<Particle>();
            int particleNum = 0;

            for (int i = 0; i < particleList.Count; i++) // go through however many particles are in the list
            {
                if (particleList[i] == p) // if the index of a particle in the list equals the current particle
                {
                    particleNum = i; // set that particle's number to be the same as that index
                }
            } 

            // Note: how many heights you have is how many times you iterate through widths. create spring dampers for every neighbor
            if (particleNum + width < particleList.Count) // above neighbor
            {
                p.neighbors.Add(particleList[particleNum + width]); // add that particle to the neighbor list
                springDamper sd = new springDamper(p, particleList[particleNum + width], springConstant, dampingFactor, restLength); // create a spring damper
                springDamperList.Add(sd); // add that spring damper to the spring damper list
                lineList.Add(createLineRenderers(sd)); // create a line renderer here and add it to the line list
            }

            if ((particleNum + 1) % width > particleNum % width) // immediate right neighbor
            {
                p.neighbors.Add(particleList[particleNum + 1]); // add that particle to the neighbor list
                springDamper sd = new springDamper(p, particleList[particleNum + 1], springConstant, dampingFactor, restLength); // create a spring damper
                springDamperList.Add(sd); // add that spring damper to the spring damper list
                lineList.Add(createLineRenderers(sd)); // create a line renderer here and add it to the line list
            }

            if (particleNum + width + 1 < particleList.Count && (particleNum + 1) % width > particleNum % width) // top right neighbor
            {
                p.neighbors.Add(particleList[particleNum + width + 1]); // add that particle to the neighbor list
                springDamper sd = new springDamper(p, particleList[particleNum + width + 1], springConstant, dampingFactor, restLength); // create a spring damper
                springDamperList.Add(sd); // add that spring damper to the spring damper list
                lineList.Add(createLineRenderers(sd)); // create a line renderer here and add it to the line list
            }

            if (particleNum + width - 1 < particleList.Count && (particleNum + width - 1) % width < particleNum % width) // top left neighbor
            {
                p.neighbors.Add(particleList[particleNum + width - 1]); // add that particle to the neighbor list
                springDamper sd = new springDamper(p, particleList[particleNum + width - 1], springConstant, dampingFactor, restLength); // create a spring damper
                springDamperList.Add(sd); // add that spring damper to the spring damper list
                lineList.Add(createLineRenderers(sd)); // create a line renderer here and add it to the line list
            }
        }
    }

    public void setTriangles()
    {
        // i = this particle
        // i + 1 = right
        // i + width = top
        // i + width - 1 = top left

        for (int i = 0; i < particleList.Count; i++)
        {
            if (i % width != width - 1 && i < (width * height) - width) // bottom left triangle
            {
                Triangle tri = new Triangle(particleList[i], particleList[i + 1], particleList[i + width]); // (bottom, right, top)
                triangleList.Add(tri);
            }
            if (i % width != 0 && i < (width * height) - width) // top right triangle
            {
                Triangle tri = new Triangle(particleList[i], particleList[i + width - 1], particleList[i + width]); // (bottom, top left, top)
                triangleList.Add(tri);
            }
        }
    }

    public GameObject createLineRenderers(springDamper sd)
    {
        GameObject lineGO = Instantiate(linePrefab, (sd.P1.position + sd.P2.position) / 2, new Quaternion()) as GameObject; // use line prefab to draw from first particle to second particle
        return lineGO;
    }
}
