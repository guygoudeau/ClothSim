using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class clothSim : MonoBehaviour
{
    List<Particle> particleList;
    List<springDamper> springDamperList;
    public GameObject particlePrefab;

    void Start()
    {
        particleList = new List<Particle>();
        springDamperList = new List<springDamper>();
        spawnParticles();
        setNeighbors();
    }

    void Update()
    {
        computeGravity();
    }

    void spawnParticles()
    {
        float xPos = 0f;
        float yPos = 0f;

        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                GameObject particle = Instantiate(particlePrefab, new Vector3(xPos, yPos, 0), new Quaternion()) as GameObject;
                particleList.Add(particle.GetComponent<Particle>());
                xPos += 2;
            }
            yPos += 2;
            xPos = 0;
        }
    }

    void computeGravity()
    {

    }

    void setNeighbors()
    {

    }


}
