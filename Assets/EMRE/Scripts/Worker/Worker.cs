using UnityEngine;

namespace EMRE.Scripts.Worker
{
    public class Worker : MonoBehaviour
    {
        [SerializeField] private WorkerRandomer randomer;
        [SerializeField] private FarmLand farmLand;
        [SerializeField] private Rigidbody body;
        [SerializeField, Min(0f)] private float maxSpeed;
        

        public FarmLandState FarmLandState => farmLand.State;

        
        public bool IsActive { get; private set; }
        

        public void Enable()
        {
            randomer.Randomize();
            gameObject.SetActive(true);
            IsActive = true;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            IsActive = false;
        }
        
        public FarmTile GetTargetTile(FarmTileState state)
        {
            return farmLand.FindTile(state);
        }

        public void HandleMovement(FarmTile tile)
        {
            if (!tile)
            {
                body.velocity = Vector3.zero;
                return;
            }
            
            var currentPosition = transform.position;
            var targetPosition = tile.Position;
            targetPosition.y = currentPosition.y;
            transform.LookAt(targetPosition);
            var direction = targetPosition - currentPosition;
            direction.y = 0;
            var distance = direction.magnitude;

            direction = direction.normalized * Mathf.Min(distance, maxSpeed);
            
            body.velocity = direction;
        }
    }
}