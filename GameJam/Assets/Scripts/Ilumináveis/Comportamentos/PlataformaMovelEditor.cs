#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlataformaMovel))]
public class PlataformaMovelEditor : Editor {

    SerializedProperty posicaoInicial;
    SerializedProperty posicaoFinal;

    PlataformaMovel plataforma;

    void OnEnable() {
        plataforma = target as PlataformaMovel;
        posicaoInicial = serializedObject.FindProperty("posicaoInicial");
        posicaoFinal = serializedObject.FindProperty("posicaoFinal");
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        serializedObject.Update();

        if (GUILayout.Button("Definir Posição Inicial")) {
            Undo.RecordObject(plataforma, "Posição inicial alterada.");
            plataforma.posicaoInicial = plataforma.transform.position;
            EditorUtility.SetDirty(target);
        }

        if (GUILayout.Button("Definir Posição Final")) {
            Undo.RecordObject(plataforma, "Posição final alterada.");
            plataforma.posicaoFinal = plataforma.transform.position;
            EditorUtility.SetDirty(target);
        }

        serializedObject.ApplyModifiedProperties();
    }

    public void OnSceneGUI() {
        Handles.color = Color.green;
        Handles.DrawWireCube(plataforma.posicaoInicial, plataforma.transform.localScale);

        Handles.color = Color.magenta;
        Handles.DrawWireCube(plataforma.posicaoFinal, plataforma.transform.localScale);
    }

}

#endif