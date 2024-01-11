// #if UNITY_EDITOR
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.Serialization;
// using VRC.SDKBase;
//
// namespace ANGELWARE.AW_AAC.Editor
// {
//     [CustomEditor(typeof(AW_OCS))]
//     // ReSharper disable once InconsistentNaming
//     public class AW_OCSEditor : AW_BaseEditor
//     {
//         private Texture2D _bannerImage;
//         [FormerlySerializedAs("RootTransform")] public SerializedProperty rootTransform;
//         
//         protected override void OnEnable()
//         {
//             base.OnEnable();
//             rootTransform = serializedObject.FindProperty("rootTransform");
//         }
//
//         protected override void DrawInspector()
//         {
//             serializedObject.Update();
//             var rootField = EditorGUILayout.PropertyField(rootTransform);
//
//             var btn = GUILayout.Button("Find Spine on Avatar");
//             if (btn) GetDefaultSpineTransform();
//             
//             serializedObject.ApplyModifiedProperties();
//             
//             EditorGUILayout.HelpBox("This component facilitates the setup of OCS. It contains a basic set of colliders " +
//                                     "that can be used on any avatar, and has the option to change the location of some components.",
//                 MessageType.Info, true);
//         }
//
//         private void GetDefaultSpineTransform()
//         {
//             var ocsComponent = (AW_OCS)target;
//             if (ocsComponent == null) return;
//             
//             var componentTransform = ocsComponent.transform;
//             var spine = componentTransform.transform.Find("Armature/Hips/Spine");
//
//             if (spine == null) return;
//             rootTransform.objectReferenceValue = spine;
//         }
//     }
// }
//
// #endif