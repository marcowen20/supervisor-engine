using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleWorkflow : MonoBehaviour, Supervisor.IWorkflow
{
    Supervisor.Workflow workflow;

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

    public Supervisor.Workflow LoadWorkflow()
    {
        Supervisor.InputField actionField = new Supervisor.InputField("string");
        Supervisor.InputField targetField = new Supervisor.InputField("string");
        Supervisor.InputField parameterField = new Supervisor.InputField("string");

        Supervisor.InputField[] genericInputField = new Supervisor.InputField[] { actionField, targetField, parameterField };

        Supervisor.Action actionOne = new Supervisor.Action();
        actionOne.inputFields = new GenericInputFields("grab", "Flask").GetDict();

        Supervisor.Action actionTwo = new Supervisor.Action();
        actionTwo.inputFields = new GenericInputFields("place", "Flask Place Target").GetDict();

        Supervisor.Action actionThree = new Supervisor.Action();
        actionThree.inputFields = new GenericInputFields("turn on", "Bunsen Burner").GetDict();

        Supervisor.Action actionFour = new Supervisor.Action();
        actionFour.inputFields = new GenericInputFields("turn off", "Bunsen Burner").GetDict();

        actionOne.AddNextAction(actionTwo);
        actionTwo.AddNextAction(actionThree);
        actionThree.AddNextAction(actionFour);

        List<Supervisor.Action> startingList = new List<Supervisor.Action>() { actionOne };
        workflow = new Supervisor.Workflow(startingList);
        return workflow;
    }
}
