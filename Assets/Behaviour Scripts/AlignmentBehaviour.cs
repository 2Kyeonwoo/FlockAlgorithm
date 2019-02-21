using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlockAlgorithm.Behaviour {
    [CreateAssetMenu(menuName = "Flock/Behaviour/AlignmentBehaviour")]
    public class AlignmentBehaviour : FlockBehaviour {

        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
            // if have no neighbor, miantain current alingment
            if (context.Count == 0) {
                return agent.transform.up;
            }

            // add all points together and average
            Vector2 alignmentMove = Vector2.zero;

            foreach (Transform item in context)
            {
                alignmentMove += (Vector2)item.transform.up;
            }
            alignmentMove /= context.Count;

            return alignmentMove; 
        }
    }
}