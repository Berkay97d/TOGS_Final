namespace EMRE.Scripts.Worker
{
    public class WorkerHarvestState : WorkerBaseState
    {
        public override void OnEnter(WorkerBrain brain)
        {
            base.OnEnter(brain);
            brain.HarvestAnimation();
        }
    }
}