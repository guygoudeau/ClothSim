using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class clothSim : MonoBehaviour
{
    List<Particle> particleList;
    List<springDamper> springDamperList;
    public GameObject particlePrefab;
    public float gravity;
    public int width;
    public int height;

    void Start()
    {
        particleList = new List<Particle>();
        springDamperList = new List<springDamper>();
        spawnParticles();
        setNeighbors();

        //GameObject newGameObject = new GameObject();
        //springDamper springDam = newGameObject.AddComponent<springDamper>();
    }

    void Update()
    {
        
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
                xPos += 2;
            }
            yPos += 2;
            xPos = 0;
        }
    }

    public void setNeighbors()
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

            // remember how many heights you have is how many times you iterate through widths
            if (particleNum + width < particleList.Count) // this is the ups
            {
                p.neighbors.Add(particleList[particleNum + width]);
            }

            if ((particleNum + 1) % width > particleNum % width) // this is the rights
            {
                p.neighbors.Add(particleList[particleNum + 1]);
            }

            if (particleNum + width + 1 < particleList.Count && (particleNum + 1) % width > particleNum % width) // this is the top rights
            {
                p.neighbors.Add(particleList[particleNum + width + 1]);
            }

            if (particleNum + width - 1 < particleList.Count && (particleNum + width - 1) % width < particleNum % width) // this is the top lefts
            {
                p.neighbors.Add(particleList[particleNum + width - 1]);
            }

        }
    }
}
