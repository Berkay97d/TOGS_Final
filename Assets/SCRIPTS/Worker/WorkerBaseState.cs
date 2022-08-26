namespace EMRE.Scripts.Worker
{
    public abstract class WorkerBaseState
    {
        private WorkerBaseState m_SuperState;
    
    
        public void SetSuperState(WorkerBaseState superState)
        {
            m_SuperState = superState;
        }


        public virtual void OnEnter(WorkerBrain brain)
        {
            m_SuperState?.OnEnter(brain);
        }
    
        public virtual void OnExit(WorkerBrain brain)
        {
            m_SuperState?.OnExit(brain);
        }
        
        public virtual void OnFixedUpdate(WorkerBrain brain)
        {
            m_SuperState?.OnExit(brain);
        }
    }
}
