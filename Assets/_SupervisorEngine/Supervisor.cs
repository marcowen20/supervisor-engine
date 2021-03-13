using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supervisor : MonoBehaviour
{
    public GameObject workflowGameObject;
    Workflow workflow;
    // Start is called before the first frame update
    void Start()
    {
        workflow = workflowGameObject.GetComponent<IWorkflow>().LoadWorkflow();
    }

    public interface IWorkflow
    {
        Workflow LoadWorkflow();
    }
    
    public class InputField
    {
        public string dataType;
        public string value = "";

        public InputField(string dataType)
        {
            this.dataType = dataType;
        }

        private bool ValidateData(string value)
        {
            if (dataType == "string")
            {
                return true;
            }
            else if (dataType == "int")
            {
                int n;
                return int.TryParse(value, out n);
            }
            else if (dataType == "bool")
            {
                return value == "true" || value == "false";
            } else
            {
                return false;
            }
        }

        public bool SetValue(string value)
        {
            if (ValidateData(value))
            {
                this.value = value;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Action
    {
        public List<Supervisor.InputField> inputFields = new List<Supervisor.InputField>() { };
        public List<Supervisor.Action> nextActions = new List<Supervisor.Action>() { };

        public void AddNextAction(Supervisor.Action nextAction)
        {
            nextActions.Add(nextAction);
        }

        public void RemoveNextAction(Supervisor.Action targetAction)
        {
            nextActions.Remove(targetAction);
        }

        public void addInputField (Supervisor.InputField inputField)
        {
            inputFields.Add(inputField);
        }

        public void removeInputField (Supervisor.InputField inputField)
        {
            inputFields.Remove(inputField);
        }
    }

    public class Workflow
    {
        Supervisor.Action startingAction;
        Supervisor.Action currentAction;
        Supervisor.Action previousAction;
        List<Supervisor.Action> nextActions;

        public Workflow(Supervisor.Action action)
        {
            this.startingAction = action;
            this.currentAction = action;
            this.nextActions = action.nextActions;
        }

        public Supervisor.Action GetCurrentAction()
        {
            return this.currentAction;
        }

        public Supervisor.Action GetPreviousAction()
        {
            return this.previousAction;
        }

        public List<Supervisor.Action> GetNextActions()
        {
            return this.nextActions;
        }

        public void Forward(Supervisor.Action action = null)
        {
            this.previousAction = this.currentAction;

            if (action != null)
            {
                this.currentAction = action;
                this.nextActions = action.nextActions;
            } else
            {
                this.currentAction = nextActions[0];
                this.nextActions = currentAction.nextActions;
            }

        }
    }
}