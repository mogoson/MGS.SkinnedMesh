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

using UnityEditor;
using UnityEngine;

namespace Mogoson.CurvePipe
{
    [CustomEditor(typeof(BezierPipe), true)]
    [CanEditMultipleObjects]
    public class BezierPipeEditor : CurvePipeEditor
    {
        #region Field and Property
        protected new BezierPipe Target { get { return target as BezierPipe; } }
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();

            if (Application.isPlaying)
                return;

            DrawFreeMoveHandle(Target.StartPoint, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
            {
                Target.StartPoint = position;
                Target.Rebuild();
            });

            DrawFreeMoveHandle(Target.EndPoint, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
            {
                Target.EndPoint = position;
                Target.Rebuild();
            });

            Handles.color = Color.green;
            DrawFreeMoveHandle(Target.StartTangentPoint, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
            {
                Target.StartTangentPoint = position;
                Target.Rebuild();
            });

            DrawFreeMoveHandle(Target.EndTangentPoint, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
            {
                Target.EndTangentPoint = position;
                Target.Rebuild();
            });

            Handles.DrawLine(Target.StartPoint, Target.StartTangentPoint);
            Handles.DrawLine(Target.EndPoint, Target.EndTangentPoint);
        }
        #endregion
    }
}