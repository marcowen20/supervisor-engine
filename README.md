# 👨‍🏫Supervisor👩‍🏫 - the Workflow Engine for Unity
Supervisor provides a framework to help define a Workflow of Actions that the user has to strictly follow. It is perfect for creating guided experiences in Unity.

## Concepts

### Action

`Supervisor.Action`

Each action may have multiple Input Fields (`Supervisor.InputField`) that are used to store information about the action. For example, an action might contain an Input Field for the *subject*, another for *object*, another for *verb*.

Actions can be passive or active by changing `isPassive` accordingly. Active actions require users to perform them while passive actions are done automatically.

An action may point to multiple actions through `nextActions`. After an action is done, Supervisor moves on to the next set of actions.	

![Action Graph](https://i.imgur.com/BvpQjJI.png)

### Workflow

As shown in the image above, the sets of actions can be seen as a [directed acyclic graph](https://en.wikipedia.org/wiki/Directed_acyclic_graph). The workflow keeps track of previous action and current allowed Actions.

### Intent (also Action)

Only actions defined by the current allowed actions in the Workflow are allowed to occur. When users want to do an Action, an Intent is first sent to the Supervisor.

The Supervisor then compares the Intent with the list of current allowed Actions to find a matching Action. If a matching action is found, the Intent is allowed to be executed. 



## Components

### Supervisor

Receives Intents. If an Intent matches with the current list of allowed Actions, the Intent is sent to the Assistant to be executed.

### Assistant

Implements the interface `Supervisor.IAssistant` which has the method `void DoAction(Supervisor.Action actionTODO)`. How each actions are handled must be defined here.

### Intender(s)

Sends Intents to the Supervisor.

