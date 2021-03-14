using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intender : MonoBehaviour
{
    public GameObject supervisorGameObject;
    Supervisor supervisor;
    bool doneFirstIntention;
    Supervisor.Action firstIntention;

    public class GenericInputFields
    {
        Supervisor.InputField actionField = new Supervisor.InputField("string");
        Supervisor.InputField targetField = new Supervisor.InputField("string");
        Supervisor.InputField parameterField = new Supervisor.InputField("string");

        public GenericInputFields(string action, string target, string parameter = "")
        {
            this.actionField.SetValue(action);
            this.targetField.SetValue(target);
            this.parameterField.SetValue(parameter);
        }

        public Dictionary<string, Supervisor.InputField> GetDict()
        {
            Dictionary<string, Supervisor.InputField> genericInputFields = new Dictionary<string, Supervisor.InputField>();
            genericInputFields.Add("action", actionField);
            genericInputFields.Add("target", targetField);
            genericInputFields.Add("parameter", parameterField);

            return genericInputFields;
        }
    }

    void Start()
    {
        doneFirstIntention = false;
        supervisor = supervisorGameObject.GetComponent<Supervisor>();
        firstIntention = new Supervisor.Action();

        firstIntention.inputFields = new GenericInputFields("grab", "Flask").GetDict();

    }

    // Update is called once per frame
    void Update()
    {
        if (doneFirstIntention == false)
        {
            supervisor.SendIntention(firstIntention);
            doneFirstIntention = true;
        }
    }
}
