using UnityEditor;
using UnityEngine;

namespace Tower.Projectile
{
    [CustomEditor(typeof(ProjectileType))]
    public class ProjectileTypeEditor : Editor
    {
        private SerializedProperty _isExplosiveProp;
        private SerializedProperty _isCorrosiveProp;
        private SerializedProperty _isPunctureProp;

        private SerializedProperty _explosionRadiusProp;
        private SerializedProperty _dotDamageProp;
        private SerializedProperty _dotTickRateProp;
        private SerializedProperty _layersToPunctureProp;

        private void OnEnable()
        {
            _isExplosiveProp = serializedObject.FindProperty("_isExplosive");
            _isCorrosiveProp = serializedObject.FindProperty("_isCorrosive");
            _isPunctureProp = serializedObject.FindProperty("_isPuncture");

            _explosionRadiusProp = serializedObject.FindProperty("_explosionRadius");
            _dotDamageProp = serializedObject.FindProperty("_dotDamage");
            _dotTickRateProp = serializedObject.FindProperty("_dotTickRate");
            _layersToPunctureProp = serializedObject.FindProperty("_layersToPuncture");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Tower", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_sprite"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_damage"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_moveSpeed"));

            EditorGUILayout.PropertyField(_isExplosiveProp);
            if (_isExplosiveProp.boolValue)
            {
                EditorGUILayout.PropertyField(_explosionRadiusProp);
            }

            EditorGUILayout.PropertyField(_isCorrosiveProp);
            if (_isCorrosiveProp.boolValue)
            {
                EditorGUILayout.PropertyField(_dotDamageProp);
                EditorGUILayout.PropertyField(_dotTickRateProp);
            }

            EditorGUILayout.PropertyField(_isPunctureProp);
            if (_isPunctureProp.boolValue)
            {
                EditorGUILayout.PropertyField(_layersToPunctureProp);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
