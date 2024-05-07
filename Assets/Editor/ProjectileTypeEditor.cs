using UnityEditor;

namespace Tower.Projectile
{
    [CustomEditor(typeof(ProjectileType))]
    public class ProjectileTypeEditor : Editor
    {
        private SerializedProperty _damageTypeProp;
        private SerializedProperty _explosionRadiusProp;
        private SerializedProperty _dotDamageProp;
        private SerializedProperty _dotTickRateProp;
        private SerializedProperty _dotTickAmountProp;
        private SerializedProperty _layersToPunctureProp;

        private void OnEnable()
        {
            _damageTypeProp = serializedObject.FindProperty("_damageType");
            _explosionRadiusProp = serializedObject.FindProperty("_explosionRadius");
            _dotDamageProp = serializedObject.FindProperty("_dotDamage");
            _dotTickRateProp = serializedObject.FindProperty("_dotTickRate");
            _dotTickAmountProp = serializedObject.FindProperty("_dotAmountOfTicks");
            _layersToPunctureProp = serializedObject.FindProperty("_layersToPuncture");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Projectile", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_sprite"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_damage"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_moveSpeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_lifetime"));

            EditorGUILayout.PropertyField(_damageTypeProp);
            DamageType damageType = (DamageType)_damageTypeProp.intValue;

            _explosionRadiusProp.isExpanded = false;
            _dotDamageProp.isExpanded = false;
            _layersToPunctureProp.isExpanded = false;

            if ((damageType & DamageType.Explosive) != 0)
            {
                EditorGUILayout.PropertyField(_explosionRadiusProp);
            }
            if ((damageType & DamageType.Corrosive) != 0)
            {
                EditorGUILayout.PropertyField(_dotDamageProp);
                EditorGUILayout.PropertyField(_dotTickRateProp);
                EditorGUILayout.PropertyField(_dotTickAmountProp);
            }
            if ((damageType & DamageType.Puncture) != 0)
            {
                EditorGUILayout.PropertyField(_layersToPunctureProp);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

