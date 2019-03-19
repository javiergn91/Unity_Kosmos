using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;
using System.Reflection;
using Kosmos.UI.Transition;

[CustomEditor(typeof(FluxAnimation))]
public class FluxAnimationInspector : Editor 
{
    SerializedProperty onOpenStart;
    SerializedProperty onOpenEnd;
    SerializedProperty onCloseStart;
    SerializedProperty onCloseEnd;

    SerializedProperty onMovementOpenStart;
    SerializedProperty onMovementOpenEnd;
    SerializedProperty onMovementCloseStart;
    SerializedProperty onMovementCloseEnd;

    SerializedProperty onRotationOpenStart;
    SerializedProperty onRotationOpenEnd;
    SerializedProperty onRotationCloseStart;
    SerializedProperty onRotationCloseEnd;

    SerializedProperty onScaleOpenStart;
    SerializedProperty onScaleOpenEnd;
    SerializedProperty onScaleCloseStart;
    SerializedProperty onScaleCloseEnd;

    SerializedProperty onAlphaOpenStart;
    SerializedProperty onAlphaOpenEnd;
    SerializedProperty onAlphaCloseStart;
    SerializedProperty onAlphaCloseEnd;

    SerializedProperty onColorOpenStart;
    SerializedProperty onColorOpenEnd;
    SerializedProperty onColorCloseStart;
    SerializedProperty onColorCloseEnd;

    void OnEnable()
    {
        onOpenStart = serializedObject.FindProperty("onOpenStart");
        onOpenEnd = serializedObject.FindProperty("onOpenEnd");
        onCloseStart = serializedObject.FindProperty("onCloseStart");
        onCloseEnd = serializedObject.FindProperty("onCloseEnd");

        onMovementOpenStart = serializedObject.FindProperty("onMovementOpenStart");
        onMovementOpenEnd = serializedObject.FindProperty("onMovementOpenEnd");
        onMovementCloseStart = serializedObject.FindProperty("onMovementCloseStart");
        onMovementCloseEnd = serializedObject.FindProperty("onMovementCloseEnd");

        onRotationOpenStart = serializedObject.FindProperty("onRotationOpenStart");
        onRotationOpenEnd = serializedObject.FindProperty("onRotationOpenEnd");
        onRotationCloseStart = serializedObject.FindProperty("onRotationCloseStart");
        onRotationCloseEnd = serializedObject.FindProperty("onRotationCloseEnd");

        onScaleOpenStart = serializedObject.FindProperty("onScaleOpenStart");
        onScaleOpenEnd = serializedObject.FindProperty("onScaleOpenEnd");
        onScaleCloseStart = serializedObject.FindProperty("onScaleCloseStart");
        onScaleCloseEnd = serializedObject.FindProperty("onScaleCloseEnd");

        onAlphaOpenStart = serializedObject.FindProperty("onAlphaOpenStart");
        onAlphaOpenEnd = serializedObject.FindProperty("onAlphaOpenEnd");
        onAlphaCloseStart = serializedObject.FindProperty("onAlphaCloseStart");
        onAlphaCloseEnd = serializedObject.FindProperty("onAlphaCloseEnd");

        onColorOpenStart = serializedObject.FindProperty("onColorOpenStart");
        onColorOpenEnd = serializedObject.FindProperty("onColorOpenEnd");
        onColorCloseStart = serializedObject.FindProperty("onColorCloseStart");
        onColorCloseEnd = serializedObject.FindProperty("onColorCloseEnd");
    }

    public override void OnInspectorGUI()
    {
        FluxAnimation VCTransfrom = (FluxAnimation)target;

        GUI.backgroundColor = Color.gray;
        FluxEditor.BeginGroup();

        FluxEditor.BeginGroup("Flux Animation", new Color(1, 0.77f, 0.05f), 2, 1);
        FluxEditor.EndGroup();

        GUI.color = Color.white;
        GUI.backgroundColor = Color.white;

        // Animations

        VCTransfrom.ShowAnimations = FluxEditor.BeginFoldout(VCTransfrom.ShowAnimations, "Animations");
        if (VCTransfrom.ShowAnimations)
        {
            FluxEditor.BeginGroup();

            GUILayout.Space(2);
            VCTransfrom.TimeMode = (FluxAnimation.VCTimeMode)EditorGUILayout.EnumPopup("Time Mode ", VCTransfrom.TimeMode);
            GUILayout.Space(2);

            // Movement
            DrawMovement();

            // Rotation
            DrawRotation();

            // Scale
            DrawScale();

            // Alpha
            DrawAlpha();

            // Color
            DrawColor();

            FluxEditor.EndGroup();
        }

        // Events
        VCTransfrom.ShowEvents = FluxEditor.BeginFoldout(VCTransfrom.ShowEvents, "Events");
        if (VCTransfrom.ShowEvents)
        {
            FluxEditor.BeginGroup();

            DrawEvents();

            FluxEditor.EndGroup();
        }

        FluxEditor.EndGroup();

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(VCTransfrom);
    }

    void DrawEvents()
    {
        FluxAnimation VCTransfrom = (FluxAnimation)target;

        // Events
        VCTransfrom.ShowNormalEvents = FluxEditor.BeginFoldout(VCTransfrom.ShowNormalEvents, "Common Events");
        if (VCTransfrom.ShowNormalEvents)
        {
            FluxEditor.BeginGroup();

            EditorGUILayout.PropertyField(onOpenStart);
            EditorGUILayout.PropertyField(onOpenEnd);
            EditorGUILayout.PropertyField(onCloseStart);
            EditorGUILayout.PropertyField(onCloseEnd);

            FluxEditor.EndGroup();
        }

        // Events
        VCTransfrom.ShowMovementEvents = FluxEditor.BeginFoldout(VCTransfrom.ShowMovementEvents, "Movement Events");
        if (VCTransfrom.ShowMovementEvents)
        {
            FluxEditor.BeginGroup();

            EditorGUILayout.PropertyField(onMovementOpenStart);
            EditorGUILayout.PropertyField(onMovementOpenEnd);
            EditorGUILayout.PropertyField(onMovementCloseStart);
            EditorGUILayout.PropertyField(onMovementCloseEnd);

            FluxEditor.EndGroup();
        }

        // Events
        VCTransfrom.ShowRotationEvents = FluxEditor.BeginFoldout(VCTransfrom.ShowRotationEvents, "Rotation Events");
        if (VCTransfrom.ShowRotationEvents)
        {
            FluxEditor.BeginGroup();

            EditorGUILayout.PropertyField(onRotationOpenStart);
            EditorGUILayout.PropertyField(onRotationOpenEnd);
            EditorGUILayout.PropertyField(onRotationCloseStart);
            EditorGUILayout.PropertyField(onRotationCloseEnd);

            FluxEditor.EndGroup();
        }

        // Events
        VCTransfrom.ShowScaleEvents = FluxEditor.BeginFoldout(VCTransfrom.ShowScaleEvents, "Scale Events");
        if (VCTransfrom.ShowScaleEvents)
        {
            FluxEditor.BeginGroup();

            EditorGUILayout.PropertyField(onScaleOpenStart);
            EditorGUILayout.PropertyField(onScaleOpenEnd);
            EditorGUILayout.PropertyField(onScaleCloseStart);
            EditorGUILayout.PropertyField(onScaleCloseEnd);

            FluxEditor.EndGroup();
        }

        // Events
        VCTransfrom.ShowAlphaEvents = FluxEditor.BeginFoldout(VCTransfrom.ShowAlphaEvents, "Alpha Events");
        if (VCTransfrom.ShowAlphaEvents)
        {
            FluxEditor.BeginGroup();

            EditorGUILayout.PropertyField(onAlphaOpenStart);
            EditorGUILayout.PropertyField(onAlphaOpenEnd);
            EditorGUILayout.PropertyField(onAlphaCloseStart);
            EditorGUILayout.PropertyField(onAlphaCloseEnd);

            FluxEditor.EndGroup();
        }

        // Events
        VCTransfrom.ShowColorEvents = FluxEditor.BeginFoldout(VCTransfrom.ShowColorEvents, "Color Events");
        if (VCTransfrom.ShowColorEvents)
        {
            FluxEditor.BeginGroup();

            EditorGUILayout.PropertyField(onColorOpenStart);
            EditorGUILayout.PropertyField(onColorOpenEnd);
            EditorGUILayout.PropertyField(onColorCloseStart);
            EditorGUILayout.PropertyField(onColorCloseEnd);

            FluxEditor.EndGroup();
        }
    }

    void DrawMovement()
    {
        FluxAnimation VCTransfrom = (FluxAnimation)target;

        GUI.color = Color.white;

        VCTransfrom.Movement.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.Movement.ShowInspector, "Movement");
        if (VCTransfrom.Movement.ShowInspector)
        {
            FluxEditor.BeginGroup();
            VCTransfrom.Movement.Enable = EditorGUILayout.Toggle("Enable", VCTransfrom.Movement.Enable);
            if (VCTransfrom.Movement.Enable)
            {
                VCTransfrom.Movement.OpenAnimation.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.Movement.OpenAnimation.ShowInspector, "Open Animation");
                if (VCTransfrom.Movement.OpenAnimation.ShowInspector)
                {
                    // Open
                    DrawAnimationInfo(VCTransfrom.Movement.OpenAnimation);
                }

                VCTransfrom.Movement.CloseAnimation.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.Movement.CloseAnimation.ShowInspector, "Close Animation");
                if (VCTransfrom.Movement.CloseAnimation.ShowInspector)
                {
                    // Close
                    DrawAnimationInfo(VCTransfrom.Movement.CloseAnimation);
                }

                VCTransfrom.Movement.Autoclose = EditorGUILayout.Toggle("Auto Close", VCTransfrom.Movement.Autoclose);
                if (VCTransfrom.Movement.Autoclose)
                {
                    VCTransfrom.Movement.AutocloseDelay = EditorGUILayout.FloatField("Close Delay", VCTransfrom.Movement.AutocloseDelay);
                }
                VCTransfrom.Movement.Autoplay = EditorGUILayout.Toggle("Auto Play", VCTransfrom.Movement.Autoplay);
                VCTransfrom.Movement.Loop = EditorGUILayout.Toggle("Loop", VCTransfrom.Movement.Loop);
            }
            FluxEditor.EndGroup();
        }

        EditorUtility.SetDirty(VCTransfrom);
    }

    void DrawRotation()
    {
        FluxAnimation VCTransfrom = (FluxAnimation)target;

        GUI.color = Color.white;

        VCTransfrom.Rotation.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.Rotation.ShowInspector, "Rotation");
        if (VCTransfrom.Rotation.ShowInspector)
        {
            FluxEditor.BeginGroup();
            VCTransfrom.Rotation.Enable = EditorGUILayout.Toggle("Enable", VCTransfrom.Rotation.Enable);
            if (VCTransfrom.Rotation.Enable)
            {
                VCTransfrom.Rotation.OpenAnimation.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.Rotation.OpenAnimation.ShowInspector, "Open Animation");
                if (VCTransfrom.Rotation.OpenAnimation.ShowInspector)
                {
                    // Open
                    DrawAnimationInfo(VCTransfrom.Rotation.OpenAnimation);
                }

                VCTransfrom.Rotation.CloseAnimation.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.Rotation.CloseAnimation.ShowInspector, "Close Animation");
                if (VCTransfrom.Rotation.CloseAnimation.ShowInspector)
                {
                    // Close
                    DrawAnimationInfo(VCTransfrom.Rotation.CloseAnimation);
                }

                VCTransfrom.Rotation.Autoclose = EditorGUILayout.Toggle("Auto Close", VCTransfrom.Rotation.Autoclose);
                if (VCTransfrom.Rotation.Autoclose)
                {
                    VCTransfrom.Rotation.AutocloseDelay = EditorGUILayout.FloatField("Close Delay", VCTransfrom.Rotation.AutocloseDelay);
                }
                VCTransfrom.Rotation.Autoplay = EditorGUILayout.Toggle("Auto Play", VCTransfrom.Rotation.Autoplay);
                VCTransfrom.Rotation.Loop = EditorGUILayout.Toggle("Loop", VCTransfrom.Rotation.Loop);
            }
            FluxEditor.EndGroup();
        }

        EditorUtility.SetDirty(VCTransfrom);

    }

    void DrawScale()
    {
        FluxAnimation VCTransfrom = (FluxAnimation)target;

        GUI.color = Color.white;

        VCTransfrom.Scale.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.Scale.ShowInspector, "Scale");
        if (VCTransfrom.Scale.ShowInspector)
        {
            FluxEditor.BeginGroup();
            VCTransfrom.Scale.Enable = EditorGUILayout.Toggle("Enable", VCTransfrom.Scale.Enable);
            if (VCTransfrom.Scale.Enable)
            {
                VCTransfrom.Scale.OpenAnimation.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.Scale.OpenAnimation.ShowInspector, "Open Animation");
                if (VCTransfrom.Scale.OpenAnimation.ShowInspector)
                {
                    // Open
                    DrawAnimationInfo(VCTransfrom.Scale.OpenAnimation, true);
                }

                VCTransfrom.Scale.CloseAnimation.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.Scale.CloseAnimation.ShowInspector, "Close Animation");
                if (VCTransfrom.Scale.CloseAnimation.ShowInspector)
                {
                    // Close
                    DrawAnimationInfo(VCTransfrom.Scale.CloseAnimation, true);
                }

                VCTransfrom.Scale.Autoclose = EditorGUILayout.Toggle("Auto Close", VCTransfrom.Scale.Autoclose);
                if (VCTransfrom.Scale.Autoclose)
                {
                    VCTransfrom.Scale.AutocloseDelay = EditorGUILayout.FloatField("Close Delay", VCTransfrom.Scale.AutocloseDelay);
                }
                VCTransfrom.Scale.Autoplay = EditorGUILayout.Toggle("Auto Play", VCTransfrom.Scale.Autoplay);
                VCTransfrom.Scale.Loop = EditorGUILayout.Toggle("Loop", VCTransfrom.Scale.Loop);
            }
            FluxEditor.EndGroup();
        }

        EditorUtility.SetDirty(VCTransfrom);
    }

    void DrawAlpha()
    {
        FluxAnimation VCTransfrom = (FluxAnimation)target;

        GUI.color = Color.white;

        VCTransfrom.Alpha.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.Alpha.ShowInspector, "Alpha");
        if (VCTransfrom.Alpha.ShowInspector)
        {
            FluxEditor.BeginGroup();

            if (VCTransfrom.gameObject.GetComponent<CanvasGroup>())
            {
                VCTransfrom.Alpha.Enable = EditorGUILayout.Toggle("Enable", VCTransfrom.Alpha.Enable);
                if (VCTransfrom.Alpha.Enable)
                {
                    VCTransfrom.Alpha.OpenAnimation.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.Alpha.OpenAnimation.ShowInspector, "Open Animation");
                    if (VCTransfrom.Alpha.OpenAnimation.ShowInspector)
                    {
                        // Open
                        DrawAlphaInfo(VCTransfrom.Alpha.OpenAnimation);
                    }

                    VCTransfrom.Alpha.CloseAnimation.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.Alpha.CloseAnimation.ShowInspector, "Close Animation");
                    if (VCTransfrom.Alpha.CloseAnimation.ShowInspector)
                    {
                        // Close
                        DrawAlphaInfo(VCTransfrom.Alpha.CloseAnimation);
                    }

                    VCTransfrom.Alpha.Autoclose = EditorGUILayout.Toggle("Auto Close", VCTransfrom.Alpha.Autoclose);
                    if (VCTransfrom.Alpha.Autoclose)
                    {
                        VCTransfrom.Alpha.AutocloseDelay = EditorGUILayout.FloatField("Close Delay", VCTransfrom.Alpha.AutocloseDelay);
                    }
                    VCTransfrom.Alpha.Autoplay = EditorGUILayout.Toggle("Auto Play", VCTransfrom.Alpha.Autoplay);
                    VCTransfrom.Alpha.Loop = EditorGUILayout.Toggle("Loop", VCTransfrom.Alpha.Loop);
                }
            }
            else
            {
                EditorGUILayout.LabelField("GameObject should consist a CanvasGroup for Alpha Transitions", EditorStyles.wordWrappedLabel);
                if (FluxEditor.Button("Add CanvasGroup", true))
                {
                    VCTransfrom.gameObject.AddComponent<CanvasGroup>();
                }
            }

            FluxEditor.EndGroup();
        }

        EditorUtility.SetDirty(VCTransfrom);
    }

    void DrawColor()
    {
        FluxAnimation VCTransfrom = (FluxAnimation)target;

        GUI.color = Color.white;

        VCTransfrom.ColorData.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.ColorData.ShowInspector, "Color");
        if (VCTransfrom.ColorData.ShowInspector)
        {
            FluxEditor.BeginGroup();

            if (VCTransfrom.gameObject.GetComponent<SpriteRenderer>() ||
                VCTransfrom.gameObject.GetComponent<Renderer>() ||
                VCTransfrom.gameObject.GetComponent<Image>() ||
                VCTransfrom.gameObject.GetComponent<Text>())
            {
                VCTransfrom.ColorData.Enable = EditorGUILayout.Toggle("Enable", VCTransfrom.ColorData.Enable);
                if (VCTransfrom.ColorData.Enable)
                {
                    VCTransfrom.ColorData.OpenAnimation.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.ColorData.OpenAnimation.ShowInspector, "Open Animation");
                    if (VCTransfrom.ColorData.OpenAnimation.ShowInspector)
                    {
                        // Open
                        DrawColorInfo(VCTransfrom.ColorData.OpenAnimation);
                    }

                    VCTransfrom.ColorData.CloseAnimation.ShowInspector = FluxEditor.BeginFoldout(VCTransfrom.ColorData.CloseAnimation.ShowInspector, "Close Animation");
                    if (VCTransfrom.ColorData.CloseAnimation.ShowInspector)
                    {
                        // Close
                        DrawColorInfo(VCTransfrom.ColorData.CloseAnimation);
                    }

                    VCTransfrom.ColorData.Autoclose = EditorGUILayout.Toggle("Auto Close", VCTransfrom.ColorData.Autoclose);
                    if (VCTransfrom.ColorData.Autoclose)
                    {
                        VCTransfrom.ColorData.AutocloseDelay = EditorGUILayout.FloatField("Close Delay", VCTransfrom.ColorData.AutocloseDelay);
                    }
                    VCTransfrom.ColorData.Autoplay = EditorGUILayout.Toggle("Auto Play", VCTransfrom.ColorData.Autoplay);
                    VCTransfrom.ColorData.Loop = EditorGUILayout.Toggle("Loop", VCTransfrom.ColorData.Loop);
                }
            }
            else
            {
                EditorGUILayout.LabelField("GameObject should consist a Renderer, SpriteRenderer, Image or Text for Color Transitions", EditorStyles.wordWrappedLabel);
            }

            FluxEditor.EndGroup();
        }

        EditorUtility.SetDirty(VCTransfrom);
    }

    void DrawAnimationInfo(FluxAnimation.VCAnimationInfo Info, bool isScale = false)
    {
        GUILayout.Space(10);

        FluxEditor.BeginGroup(true);

        Info.AnimMethod = (FluxAnimation.VCAnimationMethod)EditorGUILayout.EnumPopup("Animation Method", Info.AnimMethod);
        if (Info.AnimMethod == FluxAnimation.VCAnimationMethod.AnimCurve)
        {
            Info.CurveX = EditorGUILayout.CurveField("Animation Curve X", Info.CurveX);
            Info.CurveY = EditorGUILayout.CurveField("Animation Curve Y", Info.CurveY);
            Info.CurveZ = EditorGUILayout.CurveField("Animation Curve Z", Info.CurveZ);
        }
        else
        {
            Info.XEquation = (FluxAnimation.Equations)EditorGUILayout.EnumPopup("Equation on X", Info.XEquation);
            Info.YEquation = (FluxAnimation.Equations)EditorGUILayout.EnumPopup("Equation on Y", Info.YEquation);
            Info.ZEquation = (FluxAnimation.Equations)EditorGUILayout.EnumPopup("Equation on Z", Info.ZEquation);
        }

        GUILayout.Space(5);
        Info.Start = EditorGUILayout.Vector3Field("Start", Info.Start);
        Info.End = EditorGUILayout.Vector3Field("End", Info.End);

        if (isScale)
        {
            Info.Start.x = Info.Start.x == 0 ? 1.0f : Info.Start.x;
            Info.Start.y = Info.Start.y == 0 ? 1.0f : Info.Start.y;
            Info.Start.z = Info.Start.z == 0 ? 1.0f : Info.Start.z;

            Info.End.x = Info.End.x == 0 ? 1.0f : Info.End.x;
            Info.End.y = Info.End.y == 0 ? 1.0f : Info.End.y;
            Info.End.z = Info.End.z == 0 ? 1.0f : Info.End.z;
        }

        Info.Duration = EditorGUILayout.FloatField("Duration", Info.Duration);
        if (Info.Duration <= 0)
        {
            Info.Duration = 1;
        }

        Info.Delay = EditorGUILayout.FloatField("Delay", Info.Delay);

        FluxEditor.EndGroup();
    }

    void DrawAlphaInfo(FluxAnimation.VCAlphaInfo Info)
    {
        GUILayout.Space(10);

        FluxEditor.BeginGroup(true);

        Info.Start = EditorGUILayout.Slider("Start", Info.Start, 0, 1);
        Info.End = EditorGUILayout.Slider("End", Info.End, 0, 1);

        Info.Duration = EditorGUILayout.FloatField("Duration", Info.Duration);
        if (Info.Duration <= 0)
        {
            Info.Duration = 1;
        }

        Info.Delay = EditorGUILayout.FloatField("Delay", Info.Delay);

        FluxEditor.EndGroup();
    }

    void DrawColorInfo(FluxAnimation.VCColorInfo Info)
    {
        GUILayout.Space(10);

        FluxEditor.BeginGroup(true);

        Info.AnimMethod = (FluxAnimation.VCAnimationMethod)EditorGUILayout.EnumPopup("Animation Method", Info.AnimMethod);
        if (Info.AnimMethod == FluxAnimation.VCAnimationMethod.AnimCurve)
        {
            Info.Curve = EditorGUILayout.CurveField("Animation Curve", Info.Curve);
        }
        else
        {
            Info.Equation = (FluxAnimation.Equations)EditorGUILayout.EnumPopup("Equation", Info.Equation);
        }

        Info.Start = EditorGUILayout.ColorField("Start", Info.Start);
        Info.End = EditorGUILayout.ColorField("End", Info.End);

        Info.Duration = EditorGUILayout.FloatField("Duration", Info.Duration);
        if (Info.Duration <= 0)
        {
            Info.Duration = 1;
        }

        Info.Delay = EditorGUILayout.FloatField("Delay", Info.Delay);

        FluxEditor.EndGroup();
    }
}


/*
 * Top
 * LeftTop,
 * RightTop,
 * Left,
 * Middle,
 * Right
 * LeftBottom,
 * RightBottom,
 * Bottom
*/