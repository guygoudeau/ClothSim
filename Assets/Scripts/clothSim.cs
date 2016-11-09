using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class clothSim : MonoBehaviour
{
    List<Particle> particleList;
    List<springDamper> springDamperList;
    public GameObject particlePrefab;
    public int width;
    public int height;
    [Range(-15, 15)]
    public float gravity;
    [Range(0, 100)]
    public float springConstant;
    [Range(0, 10)]
    public float dampingFactor;
    [Range(0, 5)]
    public float restLength;

    void Start()
    {
        particleList = new List<Particle>();
        springDamperList = new List<springDamper>();

        spawnParticles();
        setNeighborsAndDampers();

        foreach (Particle p in particleList) // set starting position values for every particle
        {
            p.position = p.transform.position;
        }
    }

    void Update()
    {
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

        foreach (Particle p in particleList) // apply forward Euler integration to every particle that isn't an anchor
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
    }

    public void spawnParticles()
    {
        float xPos = 0f;
        float yPos = 0f;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject particle = Instantiate(particlePrefab, new Vector3(xPos, yPos, 0), new Quaternion()) as GameObject;
                particleList.Add(particle.GetComponent<Particle>());
                xPos += 2; // x margin of 2 between particles
            }
            yPos += 2; // y margin of 2 between particles
            xPos = 0;
        }

        particleList[particleList.Count - 1].anchor = true; // set top left anchor
        particleList[particleList.Count - width].anchor = true; // set top right anchor
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

            // remember how many heights you have is how many times you iterate through widths. create spring dampers for every neighbor
            if (particleNum + width < particleList.Count) // this is the ups
            {
                p.neighbors.Add(particleList[particleNum + width]);
                springDamper sd = new springDamper(p, particleList[particleNum + width], springConstant, dampingFactor, restLength);
                springDamperList.Add(sd);
            }

            if ((particleNum + 1) % width > particleNum % width) // this is the rights
            {
                p.neighbors.Add(particleList[particleNum + 1]);
                springDamper sd = new springDamper(p, particleList[particleNum + 1], springConstant, dampingFactor, restLength);
                springDamperList.Add(sd);
            }

            if (particleNum + width + 1 < particleList.Count && (particleNum + 1) % width > particleNum % width) // this is the top rights
            {
                p.neighbors.Add(particleList[particleNum + width + 1]);
                springDamper sd = new springDamper(p, particleList[particleNum + width + 1], springConstant, dampingFactor, restLength);
                springDamperList.Add(sd);
            }

            if (particleNum + width - 1 < particleList.Count && (particleNum + width - 1) % width < particleNum % width) // this is the top lefts
            {
                p.neighbors.Add(particleList[particleNum + width - 1]);
                springDamper sd = new springDamper(p, particleList[particleNum + width - 1], springConstant, dampingFactor, restLength);
                springDamperList.Add(sd);
            }
        }
    }
}
