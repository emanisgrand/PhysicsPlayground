using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PhysicsEngine))]
public class ApplyForce : MonoBehaviour
{
    public Vector3 forceVector;
    private PhysicsEngine physicsEngine;

    private void Start()
    {
        physicsEngine = GetComponent<PhysicsEngine>();
    }

    private void FixedUpdate()
    {
        physicsEngine.AddForce(forceVector);
    }

}
