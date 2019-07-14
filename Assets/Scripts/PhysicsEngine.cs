﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    //DrawForces variables
    private LineRenderer lineRenderer;
    private int numberOfForces;
    public bool showTrails = true;

    //the other physics engine variables
    public Vector3 velocityVector;  //average velocity this FixedUpdate();
    public Vector3 netForceVector;
    public float mass;

    private List<Vector3> forceVectorList = new List<Vector3>();


    void Start()
    {
        SetupThrustTrails();    
    }

    void FixedUpdate() {
        RenderTrails();
        UpdatePosition();      
    }

    public void AddForce(Vector3 forceVector) {
        forceVectorList.Add(forceVector);
    }

    void UpdatePosition() {
        //Sum the forces and clear the list
       netForceVector = Vector3.zero;
        foreach (Vector3 forceVector in forceVectorList) {
            netForceVector = netForceVector + forceVector;
        }
        forceVectorList = new List<Vector3>();

        //Calculate position change due to net force
        Vector3 accelerationVector = netForceVector / mass;
        velocityVector += accelerationVector * Time.deltaTime;
        transform.position += velocityVector * Time.deltaTime;
    }

    void SetupThrustTrails()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetColors(Color.yellow, Color.yellow);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.useWorldSpace = false;
    }

    void RenderTrails()
    { //only for updating the trails
        if (showTrails)
        {
            lineRenderer.enabled = true;
            numberOfForces = forceVectorList.Count;
            lineRenderer.SetVertexCount(numberOfForces * 2);
            int i = 0;
            foreach (Vector3 forceVector in forceVectorList)
            {
                lineRenderer.SetPosition(i, Vector3.zero);
                lineRenderer.SetPosition(i + 1, -forceVector);
                i = i + 2;
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
