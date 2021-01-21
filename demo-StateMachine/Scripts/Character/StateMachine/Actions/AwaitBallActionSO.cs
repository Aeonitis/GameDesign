using UnityEngine;
using Hiraeth.StateMachine;
using Hiraeth.StateMachine.ScriptableObjects;


[CreateAssetMenu(fileName = "AwaitBallAction", menuName = "State Machines/Actions/Await Ball")]
public class AwaitBallActionSO : StateActionSO<AwaitBallAction>
{
}

public class AwaitBallAction : StateAction
{
    //Component references
    private Protagonist _protagonistScript;
    private AwaitBallActionSO _originSO => (AwaitBallActionSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _protagonistScript = stateMachine.GetComponent<Protagonist>();
        Debug.Log("Await Ball: Await Ball AWAKE!");
    }

    public override void OnUpdate()
    {
        Debug.Log("Await Ball: ...");
    }

    public override void OnStateEnter()
    {
        Debug.Log("Await Ball: OnStateEnter");
    }

    public override void OnStateExit()
    {
        Debug.Log("Await Ball: OnStateExit");
    }
}