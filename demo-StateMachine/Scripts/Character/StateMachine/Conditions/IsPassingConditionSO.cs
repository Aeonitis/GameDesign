using UnityEngine;
using Hiraeth.StateMachine;
using Hiraeth.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsPassing", menuName = "State Machines/Conditions/Is Passing")]
public class IsPassingConditionSO : StateConditionSO<IsPassingCondition>
{
}

public class IsPassingCondition : Condition
{
    private Protagonist _protagonistScript;

    public override void Awake(StateMachine stateMachine)
    {
        _protagonistScript = stateMachine.GetComponent<Protagonist>();
    }

    protected override bool Statement() => _protagonistScript.passInput;
}