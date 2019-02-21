using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FlockAlgorithm.Behaviour {
    [CreateAssetMenu(menuName = ("Flock/Behaviour/Avoidance"))]
    public class AvoidanceBehaviour : FlockBehaviour {
        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
            // if have no neighbor, return no adjustment
            if (context.Count == 0) {
                return Vector2.zero;
            }

            // add all points together and average
            Vector2 avoidanceMove = Vector2.zero;
            int avoid = 0;
            foreach (Transform item in context) {
                if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius) {
                    avoid++;
                    avoidanceMove += (Vector2)(agent.transform.position - item.position);
                }
            }

            if (avoid > 0) {
                avoidanceMove /= avoid;
            }

            return avoidanceMove;
        }
    }
}