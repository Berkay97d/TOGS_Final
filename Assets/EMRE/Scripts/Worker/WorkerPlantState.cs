namespace EMRE.Scripts.Worker
{
    public class WorkerPlantState : WorkerBaseState
    {
        public override void OnEnter(WorkerBrain brain)
        {
            base.OnEnter(brain);
            brain.PlantAnimation();
        }

        public override void OnFixedUpdate(WorkerBrain brain)
        {
            base.OnFixedUpdate(brain);
        }
    }
}