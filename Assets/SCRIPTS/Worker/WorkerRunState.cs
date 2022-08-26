namespace EMRE.Scripts.Worker
{
    public class WorkerRunState : WorkerBaseState
    {
        public override void OnEnter(WorkerBrain brain)
        {
            base.OnEnter(brain);
            brain.RunAnimation();
        }

        public override void OnFixedUpdate(WorkerBrain brain)
        {
            base.OnFixedUpdate(brain);
            brain.HandleMovement();
        }
    }
}