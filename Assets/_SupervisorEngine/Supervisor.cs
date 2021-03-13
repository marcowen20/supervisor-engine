using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supervisor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public class InputField
    {
        public string dataType;
        public string value;

        public void InputFIeld(string dataType)
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
        public List<Supervisor.InputField> inputFields;
        public List<Supervisor.Action> nextActions;

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
    }
}