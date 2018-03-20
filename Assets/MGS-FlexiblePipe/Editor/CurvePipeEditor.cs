/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CurvePipeEditor.cs
 *  Description  :  Editor for CurvePipe component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Developer.SkinnedMesh;
using UnityEditor;

namespace Developer.FlexiblePipe
{
    [CustomEditor(typeof(CurvePipe), true)]
    public class CurvePipeEditor : SkinEditor
    {
        #region Field and Property
        protected new CurvePipe Target { get { return target as CurvePipe; } }
        protected const float Delta = 0.05f;
        protected const float AnchorSize = 0.125f;
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            for (float t = 0; t < Target.CurveMaxTime; t += Delta)
            {
                Handles.DrawLine(Target.GetPointFromCurve(t), Target.GetPointFromCurve(t + Delta));
            }
        }
        #endregion
    }
}