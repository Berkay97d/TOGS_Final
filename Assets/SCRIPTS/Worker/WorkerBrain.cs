using System;
using NaughtyAttributes;
using UnityEngine;

namespace EMRE.Scripts.Worker
{
    public class WorkerBrain : MonoBehaviour
    {
        [SerializeField] private Worker worker;
        [SerializeField] private PlayerAnimator animator;


        public readonly WorkerIdleState Idle = new WorkerIdleState();
        public readonly WorkerRunState Run = new WorkerRunState();
        public readonly WorkerHarvestState Harvest = new WorkerHarvestState();
        public readonly WorkerPlantState Plant = new WorkerPlantState();
        

        private WorkerBaseState m_CurrentState;
        private FarmTile m_TargetTile;


        private void Start()
        {
            SetSuperStates();
            SwitchState(Idle);
        }

        private void FixedUpdate()
        {
            //m_CurrentState?.OnFixedUpdate(this);
            HandleMovement();
        }

        private void Update()
        {
            CalculateState();
            CalculateTargetTile();
        }
        

        private void SetSuperStates()
        {
            Harvest.SetSuperState(Run);
            Plant.SetSuperState(Run);
        }


        public void HandleMovement()
        {
            worker.HandleMovement(m_TargetTile);
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


        private void CalculateState()
        {
            switch (worker.FarmLandState)
            {
                case FarmLandState.Growing:
                    SwitchState(Idle);
                    break;
                
                case FarmLandState.Seeding:
                    SwitchState(Plant);
                    break;
                
                case FarmLandState.Harvestable:
                    SwitchState(Harvest);
                    break;
            }
        }

        private void CalculateTargetTile()
        {
            if (m_CurrentState is WorkerHarvestState)
            {
                m_TargetTile = worker.GetTargetTile(FarmTileState.Grown);
            }
            else if (m_CurrentState is WorkerPlantState)
            {
                m_TargetTile = worker.GetTargetTile(FarmTileState.Empty);
            }
            else
            {
                m_TargetTile = null;
            }
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
    }
}