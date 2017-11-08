using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryUI))]
public class InventoryEditor : Editor {
	private bool[] showItemSlots = new bool[InventoryUI.numItemSlots];
	private SerializedProperty itemImagesProperty;
	private SerializedProperty itemsProperty;
	private SerializedProperty itemCountersProperty;
	private const string inventoryPropItemImagesName = "itemImages";
	private const string inventoryPropItemsName = "items";
	private const string inventoryPropItemCounters = "itemCounters";
	private void OnEnable ()
	{
		itemImagesProperty = serializedObject.FindProperty (inventoryPropItemImagesName);
		itemsProperty = serializedObject.FindProperty (inventoryPropItemsName);
		itemCountersProperty = serializedObject.FindProperty (inventoryPropItemCounters);
	}
	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();
		for (int i = 0; i < InventoryUI.numItemSlots; i++)
		{
			ItemSlotGUI (i);
		}
		serializedObject.ApplyModifiedProperties ();
	}
	private void ItemSlotGUI (int index)
	{
		EditorGUILayout.BeginVertical (GUI.skin.box);
		EditorGUI.indentLevel++;

		showItemSlots[index] = EditorGUILayout.Foldout (showItemSlots[index], "Item slot " + index);
		if (showItemSlots[index])
		{
			EditorGUILayout.PropertyField (itemImagesProperty.GetArrayElementAtIndex (index));
			EditorGUILayout.PropertyField (itemsProperty.GetArrayElementAtIndex (index));
			EditorGUILayout.PropertyField (itemCountersProperty.GetArrayElementAtIndex (index));
		}
		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical ();
	}
}
