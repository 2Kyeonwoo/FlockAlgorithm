using UnityEngine;

namespace FlockAlgorithm {
    [RequireComponent(typeof(Collider2D))]
    public class FlockAgent : MonoBehaviour {
        Collider2D _agentCollider;

        public Collider2D AgentCollider { get { return _agentCollider; } }

        private void Start() {
            _agentCollider = GetComponent<Collider2D>();
        }

        public void Move(Vector2 velocity) {
            transform.up = velocity;
            transform.position += (Vector3)velocity * Time.deltaTime;
        }
    }
}