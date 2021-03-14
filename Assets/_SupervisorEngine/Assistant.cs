using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assistant : MonoBehaviour, Supervisor.IAssistant
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DoAction(Supervisor.Action actionTODO)
    {
        // Do action checking here.
        if (actionTODO.inputFields["target"].value == "Flask")
        {
            Debug.Log("Flask!!!!");
        }

    }
}
