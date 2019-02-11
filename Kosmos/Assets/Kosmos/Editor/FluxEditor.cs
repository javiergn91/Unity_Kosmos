using UnityEngine;
using UnityEditor;
using System.Collections;

public class FluxEditor
{
    // Widgets
    public static void BeginGroup(bool selectionStyle = false, int space = 2,int indent = 2)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(indent);
        if(selectionStyle)
        {
            GUI.backgroundColor = Color.gray;
            EditorGUILayout.BeginHorizontal("sv_iconselector_labelselection", GUILayout.MinHeight(10f));
            GUI.backgroundColor = Color.white;
        }
        else
        {
            EditorGUILayout.BeginHorizontal("As TextArea", GUILayout.MinHeight(10f));
        }
        GUILayout.BeginVertical();
        GUILayout.Space(space);
    }

    public static void BeginGroup(string heading, Color ? color = null, int space = 2, int indent = 0)
    {
        Color defaultColor = GUI.color;
        Color changedColor = GUI.color;
        if (color == null)
        {
            changedColor = defaultColor;
        }
        else
        {
            changedColor = color.Value;
        }

        GUILayout.BeginHorizontal();
        GUILayout.Space(indent);
        EditorGUILayout.BeginHorizontal("WindowBackground", GUILayout.MinHeight(10f));
        GUILayout.BeginVertical();
        GUI.color = changedColor;
        EditorGUILayout.LabelField(heading, EditorStyles.whiteBoldLabel);
        GUI.color = defaultColor;
        GUILayout.Space(space);
    }

    public static void EndGroup(int space = 10)
    {
        GUILayout.Space(3f);
        GUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(3f);
        GUILayout.EndHorizontal();

        if (space > 0)
        {
            GUILayout.Space(space);
        }
    }

    public static bool Button(string caption, bool Large = false)
    {
        GUI.backgroundColor = new Color(1, 0.77f, 0.05f);
        bool result = false;
        if(Large)
        {
            result = GUILayout.Button(caption, "LargeButtonMid");
        }        
        else
        {
            result = GUILayout.Button(caption, "ButtonMid");
        }
        GUI.backgroundColor = Color.white;

        return result;
    }

    public static bool BeginFoldout(bool value, string text, int space = 2)
    {
        text = "<b><size=11>" + text + "</size></b>";
        if (value)
        {
            text = "\u25BC " + text;
            GUI.color = new Color(1, 0.77f, 0.05f);
        }
        else
        {
            text = "\u25BA " + text;
            GUI.color = Color.gray;
        }
        
        GUILayout.Space(space);
        if (!GUILayout.Toggle(true, text, "GUIEditor.BreadcrumbLeft"))
        {
            value = !value;
        }
        GUI.color = Color.white;
        return value;
    }
}
