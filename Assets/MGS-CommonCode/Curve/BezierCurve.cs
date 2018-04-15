/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  BezierCurve.cs
 *  Description  :  Define BezierCurve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  1/7/2017
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  2/28/2018
 *  Description  :  Add static method GetPoint.
 *************************************************************************/

using System;
using UnityEngine;

namespace Mogoson.Curve
{
    /// <summary>
    /// Anchor points of linear bezier curve.
    /// </summary>
    [Serializable]
    public struct LinearBezierAnchor
    {
        #region Field and Property
        /// <summary>
        /// Start point of curve.
        /// </summary>
        public Vector3 start;

        /// <summary>
        /// End point of curve.
        /// </summary>
        public Vector3 end;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="start">Start point of curve.</param>
        /// <param name="end">End point of curve.</param>
        public LinearBezierAnchor(Vector3 start, Vector3 end)
        {
            this.start = start;
            this.end = end;
        }
        #endregion
    }

    /// <summary>
    /// Anchor points of quadratic bezier curve.
    /// </summary>
    [Serializable]
    public struct QuadraticBezierAnchor
    {
        #region Field and Property
        /// <summary>
        /// Start point of curve.
        /// </summary>
        public Vector3 start;

        /// <summary>
        /// End point of curve.
        /// </summary>
        public Vector3 end;

        /// <summary>
        /// Tangent point of curve.
        /// </summary>
        public Vector3 tangent;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="start">Start point of curve.</param>
        /// <param name="end">End point of curve.</param>
        /// <param name="tangent">Tangent point of curve.</param>
        public QuadraticBezierAnchor(Vector3 start, Vector3 end, Vector3 tangent)
        {
            this.start = start;
            this.end = end;
            this.tangent = tangent;
        }
        #endregion
    }

    /// <summary>
    /// Anchor points of cubic bezier curve.
    /// </summary>
    [Serializable]
    public struct CubicBezierAnchor
    {
        #region Field and Property
        /// <summary>
        /// Start point of curve.
        /// </summary>
        public Vector3 start;

        /// <summary>
        /// End point of curve.
        /// </summary>
        public Vector3 end;

        /// <summary>
        /// Start tangent point of curve.
        /// </summary>
        public Vector3 startTangent;

        /// <summary>
        /// End tangent point of curve.
        /// </summary>
        public Vector3 endTangent;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="start">Start point of curve.</param>
        /// <param name="end">End point of curve.</param>
        /// <param name="startTangent">Start tangent point of curve.</param>
        /// <param name="endTangent">End tangent point of curve.</param>
        public CubicBezierAnchor(Vector3 start, Vector3 end, Vector3 startTangent, Vector3 endTangent)
        {
            this.start = start;
            this.end = end;
            this.startTangent = startTangent;
            this.endTangent = endTangent;
        }
        #endregion
    }

    /// <summary>
    /// Linear bezier curve.
    /// </summary>
    public struct LinearBezierCurve
    {
        #region Field and Property
        /// <summary>
        /// Anchor points of curve.
        /// </summary>
        public LinearBezierAnchor anchor;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="anchor">Anchor points of curve.</param>
        public LinearBezierCurve(LinearBezierAnchor anchor)
        {
            this.anchor = anchor;
        }

        /// <summary>
        /// Get curve point base on t.
        /// </summary>
        /// <param name="t">t is in the range(0~1).</param>
        /// <returns>Point on curve.</returns>
        public Vector3 GetPoint(float t)
        {
            return GetPoint(anchor, t);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Get curve point base on anchor points and t.
        /// </summary>
        /// <param name="anchor">Anchor points of curve.</param>
        /// <param name="t">t is in the range(0~1).</param>
        /// <returns>Point on curve.</returns>
        public static Vector3 GetPoint(LinearBezierAnchor anchor, float t)
        {
            return (1 - t) * anchor.start + t * anchor.end;
        }
        #endregion
    }

    /// <summary>
    /// Quadratic bezier curve.
    /// </summary>
    public struct QuadraticBezierCurve
    {
        #region Field and Property
        /// <summary>
        /// Anchor points of curve.
        /// </summary>
        public QuadraticBezierAnchor anchor;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="anchor">Anchor points of curve.</param>
        public QuadraticBezierCurve(QuadraticBezierAnchor anchor)
        {
            this.anchor = anchor;
        }

        /// <summary>
        /// Get curve point base on t.
        /// </summary>
        /// <param name="t">t is in the range(0~1).</param>
        /// <returns>Point on curve.</returns>
        public Vector3 GetPoint(float t)
        {
            return GetPoint(anchor, t);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Get curve point base on anchor points and t.
        /// </summary>
        /// <param name="anchor">Anchor points of curve.</param>
        /// <param name="t">t is in the range(0~1).</param>
        /// <returns>Point on curve.</returns>
        public static Vector3 GetPoint(QuadraticBezierAnchor anchor, float t)
        {
            return Mathf.Pow(1 - t, 2) * anchor.start + 2 * t * (1 - t) * anchor.tangent + Mathf.Pow(t, 2) * anchor.end;
        }
        #endregion
    }

    /// <summary>
    /// Cubic bezier curve.
    /// </summary>
    public struct CubicBezierCurve
    {
        #region Field and Property
        /// <summary>
        /// Anchor points of curve.
        /// </summary>
        public CubicBezierAnchor anchor;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="anchor">Anchor points of curve.</param>
        public CubicBezierCurve(CubicBezierAnchor anchor)
        {
            this.anchor = anchor;
        }

        /// <summary>
        /// Get curve point base on t.
        /// </summary>
        /// <param name="t">t is in the range(0~1).</param>
        /// <returns>Point on curve.</returns>
        public Vector3 GetPoint(float t)
        {
            return GetPoint(anchor, t);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Get curve point base on anchor points and t.
        /// </summary>
        /// <param name="anchor">Anchor points of curve.</param>
        /// <param name="t">t is in the range(0~1).</param>
        /// <returns>Point on curve.</returns>
        public static Vector3 GetPoint(CubicBezierAnchor anchor, float t)
        {
            return Mathf.Pow(1 - t, 3) * anchor.start + 3 * t * Mathf.Pow(1 - t, 2) * anchor.startTangent +
                3 * (1 - t) * Mathf.Pow(t, 2) * anchor.endTangent + Mathf.Pow(t, 3) * anchor.end;
        }
        #endregion
    }
}