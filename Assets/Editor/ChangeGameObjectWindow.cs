using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeGameObjectWindow : EditorWindow
{
    private string nonEditableText = "This Window is used to modify one or more GameObject";

    private GameObject[] selectedGameObjects;

    private string objectName;
    private Vector3 objectPosition;
    private Vector3 objectScale;
    private Color color;

    [MenuItem("Window/ChangeGameObjectWindow")]
    public static void ShowWindow()
    {
        GetWindow<ChangeGameObjectWindow>("Change GameObjects Window");
    }

    private void OnEnable()
    {
        UpdateSelectedGameObjects();
    }

    private void OnGUI()
    {
        if (selectedGameObjects != null && selectedGameObjects.Length > 0)
        {
            EditorGUILayout.LabelField(nonEditableText);

            GUILayout.Space(10);

            EditorGUILayout.LabelField("Modify selected GameObjects", EditorStyles.boldLabel);

            objectName = EditorGUILayout.TextField("Name", objectName);

            GUIContent position = new GUIContent("Position", "Here we add the following values to the position of the selected GameObject");
            objectPosition = EditorGUILayout.Vector3Field(position, objectPosition);

            GUIContent scale = new GUIContent("Scale", "Here we add the following values to the size of selected GameObject");
            objectScale = EditorGUILayout.Vector3Field(scale, objectScale);


            color = EditorGUILayout.ColorField("Color", color);

            if (GUILayout.Button("Apply Changes to All"))
            {
                foreach (GameObject gameObject in selectedGameObjects)
                {
                    if (gameObject != null)
                    {
                        Undo.RecordObject(gameObject.transform, "Modify GameObject Properties");
                        gameObject.name = objectName;
                        gameObject.transform.position += objectPosition;
                        gameObject.transform.localScale += objectScale;

                        Renderer renderer = gameObject.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.material.color = color;
                        }

                        EditorUtility.SetDirty(gameObject);
                    }
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("No GameObjects selected.");
        }
    }

    private void OnSelectionChange()
    {
        UpdateSelectedGameObjects();
    }

    private void UpdateSelectedGameObjects()
    {
        selectedGameObjects = Selection.gameObjects;

        if (selectedGameObjects != null && selectedGameObjects.Length > 0)
        {
            GameObject firstSelected = selectedGameObjects[0];
            objectPosition = firstSelected.transform.position;
            objectScale = firstSelected.transform.localScale;
            Renderer renderer = firstSelected.GetComponent<Renderer>();
            if (renderer != null)
            {
                color = renderer.material.color;
            }
        }
    }
}