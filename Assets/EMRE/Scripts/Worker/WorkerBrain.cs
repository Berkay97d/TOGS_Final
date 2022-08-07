using System;
using NaughtyAttributes;
using UnityEngine;

namespace EMRE.Scripts.Worker
{
    public class WorkerBrain : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator animator;


        public readonly WorkerIdleState Idle = new WorkerIdleState();
        public readonly WorkerRunState Run = new WorkerRunState();
        public readonly WorkerHarvestState Harvest = new WorkerHarvestState();
        public readonly WorkerPlantState Plant = new WorkerPlantState();
        

        private WorkerBaseState m_CurrentState;


        private void Start()
        {
            SetSuperStates();
            SwitchState(Idle);
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private void IdleTest()
        {
            SwitchState(Idle);
        }
        
        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private void RunTest()
        {
            SwitchState(Run);
        }
        
        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private void PlantTest()
        {
            SwitchState(Plant);
        }
        
        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private void HarvestTest()
        {
            SwitchState(Harvest);
        }

        private void SetSuperStates()
        {
            Harvest.SetSuperState(Run);
            Plant.SetSuperState(Run);
        }


        public void IdleAnimation()
        {
            animator.IdleAnimation();
        }
        
        public void RunAnimation()
        {
            animator.RunAnimation();
        }

        public void HarvestAnimation()
        {
            animator.HarvestAnimation();
        }

        public void PlantAnimation()
        {
            animator.PlantAnimation();
        }

        public void SwitchState(WorkerBaseState state)
        {
            m_CurrentState?.OnExit(this);
            m_CurrentState = state;
            state?.OnEnter(this);
        }
    }
}