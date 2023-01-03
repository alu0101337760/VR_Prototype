using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace VR_Prototype
{
    [CustomEditor(typeof(EnemyVisuals))]
    [CanEditMultipleObjects] // only if you handle it properly
    public class EnemyVisualsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector ();
            if (GUILayout.Button("Attack", EditorStyles.miniButton))
            {
                ((EnemyVisuals)this.target).Attack();
            }
            if (GUILayout.Button("Die", EditorStyles.miniButton))
            {
                ((EnemyVisuals)this.target).Die();
            }
            if (GUILayout.Button("Walking", EditorStyles.miniButton))
            {
                if (((EnemyVisuals)this.target).isWalking)
                {
                    ((EnemyVisuals)this.target).Stop();
                }
                else
                {
                    ((EnemyVisuals)this.target).Walk();
                }
            }
            if (GUILayout.Button("Reset", EditorStyles.miniButton))
            {
                ((EnemyVisuals)this.target).Reset();
            }
        }
    }
}