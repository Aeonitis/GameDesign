using UnityEngine;
using Hiraeth.StateMachine;
using Hiraeth.StateMachine.ScriptableObjects;


[CreateAssetMenu(fileName = "PassAction", menuName = "State Machines/Actions/Pass")]
public class PassActionSO : StateActionSO<PassAction>
{
    [Tooltip("Passing strength.")]
    public float PassStrength = 10f;
}

public class PassAction : StateAction
{
    //Component references
    private Protagonist _protagonistScript;
    private PassActionSO _originSO => (PassActionSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _protagonistScript = stateMachine.GetComponent<Protagonist>();
        Debug.Log("Pass: Pass AWAKE!");
    }

    public override void OnUpdate()
    {
        // Vector3 hitNormal = _protagonistScript.lastHit.normal;
        // Debug.Log("hitNormal" + hitNormal);
        Debug.Log("Pass: ...");
    }

    public override void OnStateEnter()
    {
        Debug.Log("Pass: OnStateEnter");
    }

    public override void OnStateExit()
    {
        Debug.Log("Pass: OnStateExit");
    }
}