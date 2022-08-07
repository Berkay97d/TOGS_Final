using DG.Tweening;
using UnityEngine;

namespace EMRE.Scripts.Worker
{
    public class Worker : MonoBehaviour
    {
        [SerializeField] private FarmLand farmLand;
        [SerializeField] private Rigidbody body;
        [SerializeField, Min(0f)] private float maxSpeed;
        

        public FarmLandState FarmLandState => farmLand.State;


        public FarmTile GetTargetTile(FarmTileState state)
        {
            return farmLand.FindTile(state);
        }

        public void HandleMovement(FarmTile tile)
        {
            if (!tile)
            {
                body.velocity = Vector3.zero;
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