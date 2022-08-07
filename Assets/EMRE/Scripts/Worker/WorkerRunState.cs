namespace EMRE.Scripts.Worker
{
    public class WorkerRunState : WorkerBaseState
    {
        public override void OnEnter(WorkerBrain brain)
        {
            base.OnEnter(brain);
            brain.RunAnimation();
        }
    }
}