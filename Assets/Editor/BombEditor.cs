using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Bomb))]
public class BombEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Bomb bomb = (Bomb)target;

        if (GUILayout.Button("Explode Bomb"))
        {
            bomb.ExplodeBomb();
        }
    }
}