namespace EMRE.Scripts.Worker
{
    public class WorkerIdleState : WorkerBaseState
    {
        public override void OnEnter(WorkerBrain brain)
        {
            base.OnEnter(brain);
            brain.IdleAnimation();
        }
    }
}
