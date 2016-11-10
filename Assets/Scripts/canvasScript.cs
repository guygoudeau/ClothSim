﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class canvasScript : MonoBehaviour {

    public Slider springC;
    public Slider dampF;
    public Slider restL;
    public Slider grav;
    public clothSim clothSim;
	
    void Start ()
    {
        springC.value = clothSim.springConstant;
        dampF.value = clothSim.dampingFactor;
        restL.value = clothSim.restLength;
        grav.value = clothSim.gravity;
    }

	void Update ()
    {
        clothSim.springConstant = springC.value;
        clothSim.dampingFactor = dampF.value;
        clothSim.restLength = restL.value;
        clothSim.gravity = grav.value;
    }

    public void Reload(string name)
    {
        SceneManager.LoadScene(name);
    }
}