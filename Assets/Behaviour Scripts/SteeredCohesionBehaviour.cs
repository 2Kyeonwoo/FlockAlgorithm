﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlockAlgorithm.Behaviour {
    [CreateAssetMenu(menuName ="Flock/Behaviour/Steered Cohesion")]
    public class SteeredCohesionBehaviour : FlockBehaviour {

        Vector2 currentVelocity;
        public float agentSmoothTime = .5f;

        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
            // if no neighbors, return no adjustment
            if (context.Count == 0) { return Vector2.zero; }

            // add all points together and average
            Vector2 cohesionMove = Vector2.zero;
            foreach (Transform item in context) {
                cohesionMove += (Vector2)item.position;
            }
            cohesionMove /= context.Count;

            // create offset from agent position
            cohesionMove -= (Vector2)agent.transform.position;
            cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);

            return cohesionMove;
        }
    }
}