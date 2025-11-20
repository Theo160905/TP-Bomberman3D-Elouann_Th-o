using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FontGradientAnimation : MonoBehaviour
{
    private TMP_Text _textMesh;

    private Mesh _mesh;

    private Vector3[] _vertices;

    private List<int> _wordIndexes;
    private List<int> _wordLengths;

    public Gradient Gradient;
    public bool IsAnimated = true;

    // Start is called before the first frame update
    void Start()
    {
        _textMesh = GetComponent<TMP_Text>();

        _wordIndexes = new List<int> { 0 };
        _wordLengths = new List<int>();

        string s = _textMesh.text;
        for (int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            _wordLengths.Add(index - _wordIndexes[_wordIndexes.Count - 1]);
            _wordIndexes.Add(index + 1);
        }
        _wordLengths.Add(s.Length - _wordIndexes[_wordIndexes.Count - 1]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAnimated) return;
        _textMesh.ForceMeshUpdate();
        _mesh = _textMesh.mesh;
        _vertices = _mesh.vertices;

        Color[] colors = _mesh.colors;

        for (int w = 0; w < _wordIndexes.Count; w++)
        {
            int wordIndex = _wordIndexes[w];
            Vector3 offset = Wobble(Time.time + w);

            for (int i = 0; i < _wordLengths[w]; i++)
            {
                TMP_CharacterInfo c = _textMesh.textInfo.characterInfo[wordIndex + i];

                int index = c.vertexIndex;

                colors[index] = Gradient.Evaluate(Mathf.Repeat(Time.time + _vertices[index].x * 0.001f, 1f));
                colors[index + 1] = Gradient.Evaluate(Mathf.Repeat(Time.time + _vertices[index + 1].x * 0.001f, 1f));
                colors[index + 2] = Gradient.Evaluate(Mathf.Repeat(Time.time + _vertices[index + 2].x * 0.001f, 1f));
                colors[index + 3] = Gradient.Evaluate(Mathf.Repeat(Time.time + _vertices[index + 3].x * 0.001f, 1f));

                _vertices[index] += offset;
                _vertices[index + 1] += offset;
                _vertices[index + 2] += offset;
                _vertices[index + 3] += offset;
            }
        }



        _mesh.vertices = _vertices;
        _mesh.colors = colors;
        _textMesh.canvasRenderer.SetMesh(_mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 100f), Mathf.Cos(time * 100f));
    }

    public void ToggleAnimationOff()
    {
        IsAnimated = false;
        Color[] colors = _mesh.colors;

        for (int w = 0; w < _wordIndexes.Count; w++)
        {
            int wordIndex = _wordIndexes[w];
            Vector3 offset = Wobble(Time.time + w);

            for (int i = 0; i < _wordLengths[w]; i++)
            {
                TMP_CharacterInfo c = _textMesh.textInfo.characterInfo[wordIndex + i];

                int index = c.vertexIndex;

                colors[index] = Color.black;
                colors[index + 1] = Color.black;
                colors[index + 2] = Color.black;
                colors[index + 3] = Color.black;
            }
        }
        _mesh.colors = colors;
        _textMesh.canvasRenderer.SetMesh(_mesh);
    }
}