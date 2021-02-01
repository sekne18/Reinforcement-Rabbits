using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;
using TMPro;
using Random = UnityEngine.Random;

public class RabbitAgent : Agent
{
    private RabbitArea rabbitArea;
    private Animator animator;
    private RayPerception3D rayPerception;
    public GameObject egg;
    [HideInInspector]
    public int Stevec_jajc;

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Convert actions to axis values
        float forward = vectorAction[0];
        float leftOrRight = 0f;
        if (vectorAction[1] == 1f)
        {
            leftOrRight = -1f;
        }
        else if (vectorAction[1] == 2f)
        {
            leftOrRight = 1f;
        }

        // Set animator parameters
        animator.SetFloat("Vertical", forward);
        animator.SetFloat("Horizontal", leftOrRight);

        // Tiny negative reward every step
        AddReward(-1f / agentParameters.maxStep);
    }

    public override void AgentReset()
    {
        Stevec_jajc = 0;
        rabbitArea.ResetArea();
    }

    public override void CollectObservations()
    {
        // Distance to the egg
        //AddVectorObs(Vector3.Distance(egg.transform.position, transform.position));

        // Direction to egg
        //AddVectorObs((egg.transform.position - transform.position).normalized);

        // Direction rabbit is facing
        //AddVectorObs(transform.position);

        // RayPerception (sight)
        // ========================
        // rayDistance: How far to raycast
        // rayAngles: Angles to raycast (0 is right, 90 is forward, 180 is left)
        // detectableObjects: List of tags which correspond to object types agent can see
        // startOffset: Starting height offset of ray from center of agent
        // endOffset: Ending height offset of ray from center of agent
        const float rayDistance = 10f;
        float[] rayAngles = { 20f, 40f, 60f, 80f, 100f, 120f, 140f};

        string[] detectableObjects = { "egg", "wall" };
        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
    }

    private void Start()    
    {
        rabbitArea = GetComponentInParent<RabbitArea>();
        egg = rabbitArea.egg;
        animator = GetComponent<Animator>();
        rayPerception = GetComponent<RayPerception3D>();
    }

    //Collisioni - Ob trku izvedi ...
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("egg"))
        {
            // Was found
            Stevec_jajc++;
            FoundEgg(collision.gameObject);
        }
        else if (collision.transform.CompareTag("wall"))
        {
            SetReward(-1f);
            Done();
        }   
    }

    private void FoundEgg(GameObject eggObject)
    {
        rabbitArea.RemoveSpecificEgg(eggObject);  
    }
}
