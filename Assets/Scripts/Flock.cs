using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlockAlgorithm {
    public class Flock : MonoBehaviour {

        const float AgentDensity = .08f;

        public FlockAgent prefab;
        public FlockBehaviour behaviour;

        [Range(10, 500)] public int startingCount = 250;
        [Range(1f, 100f)] public float driveFactor = 10f;
        [Range(1f, 100f)] public float maxSpeed = 5f;
        [Range(1f, 10f)] public float neighborRadius = 1.5f;
        [Range(0f, 1f)] public float avoidanceRadiusMultiplier = .5f;

        private float squareMaxSpeed;
        private float squareNeighborRadius;
        private float squareAvoidanceRadius;

        public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

        List<FlockAgent> agents = new List<FlockAgent>();

        private void Start() {
            squareMaxSpeed = maxSpeed * maxSpeed;
            squareNeighborRadius = neighborRadius * neighborRadius;
            squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

            for (int i = 0; i < startingCount; i++) {
                FlockAgent newAgent = Instantiate(prefab, Random.insideUnitCircle * startingCount * AgentDensity, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform);
                newAgent.name = "Agent " + i;
                agents.Add(newAgent);
            }
        }

        private void Update() {
            foreach (FlockAgent agent in agents) {
                List<Transform> context = GetNearbyObjects(agent);
                //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

                Vector2 move = behaviour.CalculateMove(agent, context, this);
                move *= driveFactor;

                if (move.sqrMagnitude > squareMaxSpeed) {
                    move = move.normalized * maxSpeed;
                }

                agent.Move(move);
            }
        }

        List<Transform> GetNearbyObjects(FlockAgent agent) {
            List<Transform> context = new List<Transform>();
            Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);

            foreach (Collider2D c in contextColliders) {
                if (c != agent.AgentCollider) {
                    context.Add(c.transform);
                }
            }

            return context;
        }
    }
}
