using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supervisor : MonoBehaviour
{
    public GameObject workflowGameObject;
    public GameObject assistantGameObject;
    Workflow workflow;
    // Start is called before the first frame update
    void Start()
    {
        workflow = workflowGameObject.GetComponent<IWorkflow>().LoadWorkflow();
    }

    public interface IAssistant
    {
        void DoAction(Action actionTODO);
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
        public Dictionary<string, Supervisor.InputField> inputFields = new Dictionary<string, InputField>();
        public List<Supervisor.Action> nextActions = new List<Supervisor.Action>() { };
        public bool isPassive;

        public Action(bool isPassive = false)
        {
            this.isPassive = isPassive;
        }
        
        public void AddNextAction(Supervisor.Action nextAction)
        {
            nextActions.Add(nextAction);
        }

        public void RemoveNextAction(Supervisor.Action targetAction)
        {
            nextActions.Remove(targetAction);
        }

        public void addInputField(string fieldName, Supervisor.InputField inputField)
        {
            inputFields.Add(fieldName, inputField);
        }

        public void removeInputField(string fieldName)
        {
            inputFields.Remove(fieldName);
        }
    }

    public class Workflow
    {
        Supervisor.Action previousAction;
        List<Supervisor.Action> startingActions;
        List<Supervisor.Action> currentActions;
        //List<Supervisor.Action> nextActions;

        public Workflow(List<Supervisor.Action> actions)
        {
            this.startingActions = actions;
            this.currentActions = actions;
        }

        public List<Supervisor.Action> GetCurrentActions()
        {
            return this.currentActions;
        }

        public Supervisor.Action GetPreviousAction()
        {
            return this.previousAction;
        }

        public List<Supervisor.Action> GetNextActions()
        {
            return new List<Supervisor.Action> { };
        }

        public void Forward(Supervisor.Action action = null)
        {
            if (action == null)
            {
                action = this.currentActions[0];
            }

            // Search for action in currentActions
            int actionIndex = 0;
            int counter = 0;
            foreach (Supervisor.Action currentAction in GetCurrentActions()){
                if(currentAction.inputFields["action"].value == action.inputFields["action"].value &&
                    currentAction.inputFields["target"].value == action.inputFields["target"].value &&
                    currentAction.inputFields["parameter"].value == action.inputFields["parameter"].value)
                {
                    actionIndex = counter;
                }
                counter++;
            }

            this.previousAction = GetCurrentActions()[actionIndex];
            this.currentActions = GetCurrentActions()[actionIndex].nextActions;
        }
    }

    public void SendIntention(Supervisor.Action intention)
    {
        IAssistant assistant = assistantGameObject.GetComponent<IAssistant>();

        // Check if intention is in current actions
        bool intentionIsValid = true;

        foreach (Supervisor.Action workflowAction in workflow.GetCurrentActions())
        {
            foreach (string workflowInputField in workflowAction.inputFields.Keys)
            {
                Debug.Log(workflowAction.inputFields[workflowInputField].value + intention.inputFields[workflowInputField].value);
                if (workflowAction.inputFields[workflowInputField].value != intention.inputFields[workflowInputField].value)
                {
                    intentionIsValid = false;
                }
            }
        }

        if (intentionIsValid)
        {
            assistant.DoAction(intention);
            workflow.Forward(intention);
        }
        
        if (workflow.GetCurrentActions()[0].isPassive)
        {
            SendIntention(workflow.GetCurrentActions()[0]);
        }
    }
}