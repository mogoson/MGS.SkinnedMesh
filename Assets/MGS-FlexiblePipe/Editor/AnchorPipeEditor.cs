/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AnchorPipeEditor.cs
 *  Description  :  Editor for AnchorPipe component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.FlexiblePipe
{
    [CustomEditor(typeof(AnchorPipe), true)]
    public class AnchorPipeEditor : CurvePipeEditor
    {
        #region Field and Property
        protected new AnchorPipe Target { get { return target as AnchorPipe; } }
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            Handles.color = Blue;
            var constDelta = Mathf.Max(Delta, Delta * HandleUtility.GetHandleSize(Target.transform.position));
            for (float t = 0; t < Target.CurveMaxTime; t += constDelta)
            {
                Handles.DrawLine(Target.GetWorldPoint(t), Target.GetWorldPoint(Mathf.Min(Target.CurveMaxTime, t + constDelta)));
            }

            if (Application.isPlaying)
                return;

            for (int i = 0; i < Target.AnchorsCount; i++)
            {
                var anchorItem = Target.GetAnchorAt(i);
                var handleSize = HandleUtility.GetHandleSize(anchorItem);
                var constSize = handleSize * AnchorSize;

                if (Event.current.alt)
                {
                    Handles.color = Color.green;
                    if (Handles.Button(anchorItem, Quaternion.identity, constSize, constSize, SphereCap))
                    {
                        var offset = Vector3.forward * handleSize;
                        if (i > 0)
                            offset = (anchorItem - Target.GetAnchorAt(i - 1)).normalized * handleSize;

                        Undo.RecordObject(Target, "Insert Anchor");
                        Target.InsertAnchor(i + 1, anchorItem + offset);
                        Target.Rebuild();
                        MarkSceneDirty();
                    }
                }
                else if (Event.current.shift)
                {
                    Handles.color = Color.red;
                    if (Handles.Button(anchorItem, Quaternion.identity, constSize, constSize, SphereCap))
                    {
                        if (Target.AnchorsCount > 1)
                        {
                            Undo.RecordObject(Target, "Remove Anchor");
                            Target.RemoveAnchorAt(i);
                            Target.Rebuild();
                            MarkSceneDirty();
                        }
                    }
                }
                else
                {
                    Handles.color = Blue;
                    EditorGUI.BeginChangeCheck();
                    var position = Handles.FreeMoveHandle(anchorItem, Quaternion.identity, constSize, MoveSnap, SphereCap);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(Target, "Change Anchor Position");
                        Target.SetAnchorAt(i, position);
                        Target.Rebuild();
                        MarkSceneDirty();
                    }
                }
            }
        }
        #endregion
    }
}