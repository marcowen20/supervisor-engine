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

        public List<Supervisor.InputField> GetList()
        {
            Supervisor.InputField[] genericInputFields = new Supervisor.InputField[] { this.actionField, this.targetField, this.parameterField };
            return new List<Supervisor.InputField>(genericInputFields);
        }
    }

    public Supervisor.Workflow LoadWorkflow()
    {
        Supervisor.InputField actionField = new Supervisor.InputField("string");
        Supervisor.InputField targetField = new Supervisor.InputField("string");
        Supervisor.InputField parameterField = new Supervisor.InputField("string");

        Supervisor.InputField[] genericInputField = new Supervisor.InputField[] { actionField, targetField, parameterField };

        Supervisor.Action actionOne = new Supervisor.Action();
        actionOne.inputFields = new GenericInputFields("grab", "Flask").GetList();

        Supervisor.Action actionTwo = new Supervisor.Action();
        actionTwo.inputFields = new GenericInputFields("place", "Flask Place Target").GetList();

        Supervisor.Action actionThree = new Supervisor.Action();
        actionThree.inputFields = new GenericInputFields("turn on", "Bunsen Burner").GetList();

        Supervisor.Action actionFour = new Supervisor.Action();
        actionFour.inputFields = new GenericInputFields("turn off", "Bunsen Burner").GetList();

        actionOne.AddNextAction(actionTwo);
        actionTwo.AddNextAction(actionThree);
        actionThree.AddNextAction(actionFour);

        workflow = new Supervisor.Workflow(actionOne);
        return workflow;
    }
}
