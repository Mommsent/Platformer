using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    [SerializeField] private string floatName;
    [SerializeField] private bool updateOnStateEnter, updateOnStateExit;
    [SerializeField] private bool updateOnStateMachineEnter, updateOnStateMachineExit;
    [SerializeField] private float valueOnEnter, valueOnExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(updateOnStateEnter)
            animator.SetFloat(floatName, valueOnEnter);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(updateOnStateExit)
            animator.SetFloat(floatName, valueOnExit);
    }

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineEnter)
            animator.SetFloat(floatName, valueOnEnter);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if(updateOnStateMachineExit)
            animator.SetFloat(floatName, valueOnExit);
    }
}
