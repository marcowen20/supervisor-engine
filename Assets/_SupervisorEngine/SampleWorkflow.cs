using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleWorkflow : MonoBehaviour, Supervisor.IWorkflow
{
    Supervisor.Workflow workflow;

    public Supervisor.Workflow LoadWorkflow()
    {
        // Add actions to the workflow
        return workflow;
    }
}
