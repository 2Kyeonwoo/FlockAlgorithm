using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlockAlgorithm.Behaviour {
    [CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
    public class CompositeBehaviour : FlockBehaviour {

        public FlockBehaviour[] behaviour;
        public float[] weights;

        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
            // Handle data mismatch
            if (weights.Length != behaviour.Length) {
                Debug.LogError("Data mismatch in " + name, this);
                return Vector2.zero;
            }

            // Setup move
            Vector2 move = Vector2.zero;

            for (int i = 0; i < behaviour.Length; i++) {
                Vector2 partialMove = behaviour[i].CalculateMove(agent, context, flock) * weights[i];

                if (partialMove != Vector2.zero) {
                    if (partialMove.sqrMagnitude > weights[i] * weights[i]) {
                        partialMove.Normalize();
                        partialMove *= weights[i];
                    }

                    move += partialMove;
                }
            }

            return move;
        }
    }
}