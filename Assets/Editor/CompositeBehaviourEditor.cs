using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FlockAlgorithm.Editor {
    using Behaviour;

    [CustomEditor(typeof(CompositeBehaviour))]
    public class CompositeBehaviourEditor : UnityEditor.Editor {

        public override void OnInspectorGUI() {
            // Setup
            CompositeBehaviour compositeBehaviour = (CompositeBehaviour)target;

            Rect r = EditorGUILayout.BeginHorizontal();
            r.height = EditorGUIUtility.singleLineHeight;

            // Check for behaviours
            if (compositeBehaviour.behaviour == null || compositeBehaviour.behaviour.Length == 0) {
                EditorGUILayout.HelpBox("No behaviours in array", MessageType.Warning);
                EditorGUILayout.EndHorizontal();

                r = EditorGUILayout.BeginHorizontal();
                r.height = EditorGUIUtility.singleLineHeight;
            }
            else {
                r.x = 30f;
                r.width = EditorGUIUtility.currentViewWidth - 95f;
                EditorGUI.LabelField(r, "Behaviours");
                r.x = EditorGUIUtility.currentViewWidth - 65f;
                r.width = 60f;
                EditorGUI.LabelField(r, "Weights");
                r.y += EditorGUIUtility.singleLineHeight * 1.2f;

                EditorGUI.BeginChangeCheck();
                for (int i = 0; i < compositeBehaviour.behaviour.Length; i++) {
                    r.x = 5f;
                    r.width = 20f;
                    EditorGUI.LabelField(r, i.ToString());
                    r.x = 30f;
                    r.width = EditorGUIUtility.currentViewWidth - 95f;
                    compositeBehaviour.behaviour[i] = (FlockBehaviour)EditorGUI.ObjectField(r, compositeBehaviour.behaviour[i], typeof(FlockBehaviour), false);
                    r.x = EditorGUIUtility.currentViewWidth - 65f;
                    r.width = 60f;
                    compositeBehaviour.weights[i] = EditorGUI.FloatField(r, compositeBehaviour.weights[i]);
                    r.y += EditorGUIUtility.singleLineHeight * 1.1f;
                }
                if (EditorGUI.EndChangeCheck()) {
                    EditorUtility.SetDirty(compositeBehaviour);
                }

            }

            EditorGUILayout.EndHorizontal();
            r.x = 5f;
            r.width = EditorGUIUtility.currentViewWidth - 10f;
            r.y += EditorGUIUtility.singleLineHeight * .5f;

            if (GUI.Button(r, "Add Behaviour")) {
                AddBehaviour(compositeBehaviour);
                EditorUtility.SetDirty(compositeBehaviour);
            }

            r.y += EditorGUIUtility.singleLineHeight * 1.5f;

            if (compositeBehaviour.behaviour != null && compositeBehaviour.behaviour.Length > 0) {
                if (GUI.Button(r, "Remove Behaviour")) {
                    RemoveBehaviour(compositeBehaviour);
                    EditorUtility.SetDirty(compositeBehaviour);
                }
            }
        }

        void AddBehaviour(CompositeBehaviour compositeBehaviour) {
            int oldCount = (compositeBehaviour.behaviour != null) ? compositeBehaviour.behaviour.Length : 0;
            FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount + 1];
            float[] newWeights = new float[oldCount + 1];

            for (int i = 0; i < oldCount; i++) {
                newBehaviours[i] = compositeBehaviour.behaviour[i];
                newWeights[i] = compositeBehaviour.weights[i];
            }

            newWeights[oldCount] = 1f;
            compositeBehaviour.behaviour = newBehaviours;
            compositeBehaviour.weights = newWeights;
        }

        void RemoveBehaviour(CompositeBehaviour compositeBehaviour) {
            int oldCount = compositeBehaviour.behaviour.Length;
            if (oldCount == 1) {
                compositeBehaviour.behaviour = null;
                compositeBehaviour.weights = null;
                return;
            }

            FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount - 1];
            float[] newWeights = new float[oldCount - 1];

            for (int i = 0; i < oldCount - 1; i++) {
                newBehaviours[i] = compositeBehaviour.behaviour[i];
                newWeights[i] = compositeBehaviour.weights[i];
            }
            
            compositeBehaviour.behaviour = newBehaviours;
            compositeBehaviour.weights = newWeights;
        }
    }
}
