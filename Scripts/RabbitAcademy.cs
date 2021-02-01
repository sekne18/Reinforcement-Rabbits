using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RabbitAcademy : Academy
{
    private RabbitArea[] rabbitAreas;

    public override void AcademyReset()
    {
        // Get the rabbit areas
        if (rabbitAreas == null)
        {
            rabbitAreas = FindObjectsOfType<RabbitArea>();
        }

        // Set up areas
        foreach (RabbitArea RabbitArea in rabbitAreas)
        {
            RabbitArea.egg_Number = resetParameters["egg_Number"];
            RabbitArea.obsticales = resetParameters["obsticales"];
            RabbitArea.ResetArea();
        }


    }
}
