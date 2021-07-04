using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR_WIN

//Editor
// [CustomEditor(typeof(ListRequirements))]
// public class RequirementEditor : Editor {
// 	ListRequirements requirementScript;
//     public override void OnInspectorGUI(){
//         if(requirementScript == null) requirementScript = (ListRequirements) target;
// 		EditorGUILayout.LabelField("Requirements");
//         ShowListRequirement(serializedObject.FindProperty("listRequirement"));
//         serializedObject.ApplyModifiedProperties();
//     }

//     private void ShowListRequirement(SerializedProperty list){
//         EditorGUILayout.PropertyField(list, false);
//         EditorGUI.indentLevel += 1;
//         if (list.isExpanded) {
//             EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
//             for (int i = 0; i < list.arraySize; i++) {
//                 EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), false);
//                 if (list.GetArrayElementAtIndex(i).isExpanded) {
//                     EditorGUI.indentLevel += 1;
//                         requirementScript.listRequirement[i].type = (TypeRequirement) EditorGUILayout.EnumPopup("Type:", requirementScript.listRequirement[i].type);
//                         switch(requirementScript.listRequirement[i].type){
//                             case TypeRequirement.GetLevel:
//                             case TypeRequirement.DoneChapter:
//                             case TypeRequirement.DoneMission:
//                             case TypeRequirement.SimpleSpin:
//                             case TypeRequirement.SpecialSpin:
//                             case TypeRequirement.BuyItemCount:
//                             case TypeRequirement.DestroyHeroCount:
//                             case TypeRequirement.CountWin:
//                             case TypeRequirement.CountDefeat:
//                             case TypeRequirement.CountPointsOnSimpleArena:
//                             case TypeRequirement.CountPointsOnTournament:
//                             case TypeRequirement.CountSpecialHaring:
//                                 requirementScript.listRequirement[i].requireInt = EditorGUILayout.IntField("Count:", requirementScript.listRequirement[i].requireInt);
//                                 break;  
//                             // case TypeRequirement.GetHeroes:
//                             //     requirementScript.listRequirement[i].requireInt = EditorGUILayout.IntField("Count:", requirementScript.listRequirement[i].requireInt);
//                             //     break;
//                             // case TypeRequirement.GetHeroesWithLevel:
//                             //     requirementScript.listRequirement[i].requireInt = EditorGUILayout.IntField("Count:", requirementScript.listRequirement[i].requireInt);
//                             //     break;
//                             // case TypeRequirement.GetHeroesWithRating:
//                             //     requirementScript.listRequirement[i].requireInt = EditorGUILayout.IntField("Count:", requirementScript.listRequirement[i].requireInt);
//                             //     break;
//                             // case TypeRequirement.GetHeroesCount:
//                             //     requirementScript.listRequirement[i].requireInt = EditorGUILayout.IntField("Count:", requirementScript.listRequirement[i].requireInt);
//                             //     break;
//                             // case TypeRequirement.SynthesCount:
//                             //     requirementScript.listRequirement[i].requireInt = EditorGUILayout.IntField("Count:", requirementScript.listRequirement[i].requireInt);
//                             //     break;
//                             // case TypeRequirement.SynthesItem:
//                             //     requirementScript.listRequirement[i].requireInt = EditorGUILayout.IntField("Count:", requirementScript.listRequirement[i].requireInt);
//                             //     break;  
//                             // case TypeRequirement.BuyItem:
//                             //     requirementScript.listRequirement[i].requireObject = (ScriptableObject) EditorGUILayout.ObjectField ("Object:", requirementScript.listRequirement[i].requireObject, typeof (ScriptableObject), false);
//                             //     break;
//                             // case TypeRequirement.SpendResource:
//                             //     requirementScript.requireRes = (Resource) EditorGUILayout.ObjectField ("Resource:", requirementScript.requireRes, typeof(Resource), true);
//                             //  break;  
//                         }
//                     EditorGUI.indentLevel -= 1;
//                 }
//             }
//         }
//         EditorGUI.indentLevel -= 1;
//     }
// }
#endif