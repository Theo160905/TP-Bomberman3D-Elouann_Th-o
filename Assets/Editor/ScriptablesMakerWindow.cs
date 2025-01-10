using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ScriptablesMakerWindow : EditorWindow
{
    public string NewAssetName;
    public float WalkTopSpeed;
    public float WalkAcceleration;
    public float WalkDeceleration;
    public float WalkVelPower;
    public float FrictionAmount;

    [MenuItem("Window/ScriptableObjectsMaker")]
    public static void ShowWindow()
    {
        GetWindow<ScriptablesMakerWindow>("Scriptable Objects Maker");
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // New asset name input field
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);
        GUILayout.Label("New Asset Name");
        GUILayout.Space(45);
        NewAssetName = EditorGUILayout.TextField(NewAssetName);
        GUILayout.Space(5);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Separator line
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);
        GUILayout.Space(30);
        Rect rect = GUILayoutUtility.GetRect(375, 1, GUILayout.ExpandWidth(true));
        Handles.color = Color.gray;
        Handles.DrawLine(new Vector3(rect.xMin, rect.yMin), new Vector3(rect.xMax, rect.yMin));
        GUILayout.Space(30);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Top speed float
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);
        GUILayout.Label("Walk top speed");
        GUILayout.Space(55);
        WalkTopSpeed = EditorGUILayout.FloatField(WalkTopSpeed);
        GUILayout.Space(5);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Acceleration float
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);
        GUILayout.Label("Walk acceleration");
        GUILayout.Space(40);
        WalkAcceleration = EditorGUILayout.FloatField(WalkAcceleration);
        GUILayout.Space(5);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Deceleration float
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);
        GUILayout.Label("Walk deceleration");
        GUILayout.Space(40);
        WalkDeceleration = EditorGUILayout.FloatField(WalkDeceleration);
        GUILayout.Space(5);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Velocity power float
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);
        GUILayout.Label("Walk velocity power");
        GUILayout.Space(26);
        WalkVelPower = EditorGUILayout.FloatField(WalkVelPower);
        GUILayout.Space(5);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Friction float
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);
        GUILayout.Label("Friction amount");
        GUILayout.Space(54);
        FrictionAmount = EditorGUILayout.FloatField(FrictionAmount);
        GUILayout.Space(5);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Create asset button
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Create", GUILayout.Width(100), GUILayout.Height(25)))
        {
            if (!AssertAssetValidValues())
            {
                EditorGUILayout.EndHorizontal();
                return;
            }
            PlayerDatas newAsset = ScriptableObject.CreateInstance<PlayerDatas>();
            newAsset.WalkTopSpeed = WalkTopSpeed;
            newAsset.WalkAcceleration = WalkAcceleration;
            newAsset.WalkDeceleration = WalkDeceleration;
            newAsset.WalkVelPower = WalkVelPower;
            newAsset.FrictionAmount = FrictionAmount;
            AssetDatabase.CreateAsset(newAsset, $"Assets/Scripts/Player/ScriptableStats/{NewAssetName}.asset");
            AssetDatabase.SaveAssets();
            GUILayout.Width(5);
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    private bool AssertAssetValidValues()
    {
        bool assert = true;
        if (NewAssetName == "")
        {
            Debug.LogError("Can't create asset because the asset name is not valid.");
            assert = false;
        }

        if (WalkTopSpeed <= 0 | WalkAcceleration <= 0 | WalkDeceleration <= 0 | WalkVelPower <= 0)
        {
            Debug.LogWarning("One or more values are equal to 0 or lower, which may result in wrong or no movement. Please verify.");
        }
        return assert;
    }
}
