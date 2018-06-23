/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  BezierPipe.cs
 *  Description  :  Render dynamic pipe mesh base on cubic bezier curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using UnityEngine;

namespace Mogoson.CurvePipe
{
    /// <summary>
    /// Render dynamic pipe mesh base on cubic bezier curve.
    /// </summary>
    [AddComponentMenu("Mogoson/CurvePipe/BezierPipe")]
    public class BezierPipe : MonoCurvePipe
    {
        #region Field and Property
        /// <summary>
        /// Anchor points of pipe curve.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        protected CubicBezierAnchor anchor = new CubicBezierAnchor(Vector3.one,
            new Vector3(3, 1, 3), new Vector3(1, 1, 2), new Vector3(3, 1, 2));

        /// <summary>
        /// Max time of pipe curve.
        /// </summary>
        public override float MaxTime { get { return 1.0f; } }

        /// <summary>
        /// Start point of pipe curve.
        /// </summary>
        public Vector3 StartPoint
        {
            set { anchor.start = transform.InverseTransformPoint(value); }
            get { return transform.TransformPoint(anchor.start); }
        }

        /// <summary>
        /// End point of pipe curve.
        /// </summary>
        public Vector3 EndPoint
        {
            set { anchor.end = transform.InverseTransformPoint(value); }
            get { return transform.TransformPoint(anchor.end); }
        }

        /// <summary>
        /// Start tangent point of pipe curve.
        /// </summary>
        public Vector3 StartTangentPoint
        {
            set { anchor.startTangent = transform.InverseTransformPoint(value); }
            get { return transform.TransformPoint(anchor.startTangent); }
        }

        /// <summary>
        /// End tangent point of pipe curve.
        /// </summary>
        public Vector3 EndTangentPoint
        {
            set { anchor.endTangent = transform.InverseTransformPoint(value); }
            get { return transform.TransformPoint(anchor.endTangent); }
        }
        #endregion

        #region Protected Method
        /// <summary>
        /// Get local point from center curve of pipe at time.
        /// </summary>
        /// <param name="time">Time of pipe center curve.</param>
        /// <returns>Local point on pipe curve at time.</returns>
        protected override Vector3 GetLocalPointAt(float time)
        {
            return CubicBezierCurve.GetPointAt(anchor, time);
        }
        #endregion
    }
}