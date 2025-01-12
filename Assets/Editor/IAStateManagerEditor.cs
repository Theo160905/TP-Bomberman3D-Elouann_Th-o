using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IAStateManager))]
public class IAStateManagerEditor : Editor
{
    private Color _selectedColor;
    private Color _unselectedColor;
    IAStateManager stateManager;

    private void OnEnable()
    {
        stateManager = (IAStateManager)target;
        _selectedColor = stateManager.SelectedColor;
        _unselectedColor = stateManager.UnselectedColor;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        switch (stateManager.CurrentStateDisplayed)
        {
            case StateEnum.Run:
                GUILayout.Space(10);

                EditorGUILayout.BeginHorizontal();

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = Color.white;
                GUI.backgroundColor = _selectedColor;
                if (GUILayout.Button("Run", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IARunState);
                }

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = _unselectedColor;
                if (GUILayout.Button("Seek Bomb", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IASeekBombState);
                }

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = _unselectedColor;
                if (GUILayout.Button("Hunt", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IAHuntState);
                }

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = _unselectedColor;
                if (GUILayout.Button("Death", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IADeathState);
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                break;

            case StateEnum.SeekBomb:
                GUILayout.Space(10);

                EditorGUILayout.BeginHorizontal();

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = _unselectedColor;
                if (GUILayout.Button("Run", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IARunState);
                }
                GUILayout.FlexibleSpace();

                GUI.backgroundColor = _selectedColor;
                if (GUILayout.Button("Seek Bomb", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IASeekBombState);
                }

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = _unselectedColor;
                if (GUILayout.Button("Hunt", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IAHuntState);
                }

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = _unselectedColor;
                if (GUILayout.Button("Death", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IADeathState);
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                break;

            case StateEnum.Hunt:
                GUILayout.Space(10);

                EditorGUILayout.BeginHorizontal();

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = _unselectedColor;
                if (GUILayout.Button("Run", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IARunState);
                }

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = _unselectedColor;
                if (GUILayout.Button("Seek Bomb", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IASeekBombState);
                }

                GUI.backgroundColor = _selectedColor;
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Hunt", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IAHuntState);
                }

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = _unselectedColor;
                if (GUILayout.Button("Death", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IADeathState);
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                break;
            
            case StateEnum.Dead:
                GUILayout.Space(10);

                EditorGUILayout.BeginHorizontal();

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = _unselectedColor;
                if (GUILayout.Button("Run", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IARunState);
                }

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = _unselectedColor;
                if (GUILayout.Button("Seek Bomb", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IASeekBombState);
                }

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = _unselectedColor;
                if (GUILayout.Button("Hunt", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IAHuntState);
                }

                GUILayout.FlexibleSpace();
                GUI.backgroundColor = _selectedColor;
                if (GUILayout.Button("Death", GUILayout.Width(100), GUILayout.Height(25)))
                {
                    stateManager.SwitchState(stateManager.IADeathState);
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                break;
        }
    }
}
