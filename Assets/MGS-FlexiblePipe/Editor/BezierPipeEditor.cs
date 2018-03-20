/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  BezierPipeEditor.cs
 *  Description  :  Editor for BezierPipe component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEditor;
using UnityEngine;

namespace Developer.FlexiblePipe
{
    [CustomEditor(typeof(BezierPipe), true)]
    public class BezierPipeEditor : CurvePipeEditor
    {
        #region Field and Property
        protected new BezierPipe Target { get { return target as BezierPipe; } }
        #endregion

        #region Private Method
        private void DrawAnchorHandle(Vector3 anchor, Action<Vector3> callback)
        {
            EditorGUI.BeginChangeCheck();
            var position = Handles.FreeMoveHandle(anchor, Quaternion.identity, HandleUtility.GetHandleSize(anchor) * AnchorSize, MoveSnap, SphereCap);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(Target, "Change Anchor Position");
                callback.Invoke(position);
                Target.Rebuild();
                MarkSceneDirty();
            }
        }
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();

            if (Application.isPlaying)
                return;

            DrawAnchorHandle(Target.StartPoint, (position) => { Target.StartPoint = position; });
            DrawAnchorHandle(Target.EndPoint, (position) => { Target.EndPoint = position; });

            Handles.color = Color.green;
            DrawAnchorHandle(Target.StartTangentPoint, (position) => { Target.StartTangentPoint = position; });
            DrawAnchorHandle(Target.EndTangentPoint, (position) => { Target.EndTangentPoint = position; });

            Handles.DrawLine(Target.StartPoint, Target.StartTangentPoint);
            Handles.DrawLine(Target.EndPoint, Target.EndTangentPoint);
        }
        #endregion
    }
}