/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  USinCurve.cs
 *  Description  :  Define sin curve for unity.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/21/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Mathematics;
using UnityEngine;

namespace Mogoson.Curve
{
    /// <summary>
    ///  Sin curve for unity.
    /// </summary>
    public class USinCurve : SinCurve, ICurve
    {
        #region Field and Property
        /// <summary>
        /// Delta to lerp key.
        /// </summary>
        protected const float Delta = 0.05f;

        /// <summary>
        /// Length of sin curve.
        /// </summary>
        public float Length
        {
            get
            {
                var length = 0.0f;
                for (float key = 0; key < MaxKey; key += Delta)
                {
                    length += Vector3.Distance(GetPointAt(key), GetPointAt(key + Delta));
                }
                return length;
            }
        }

        /// <summary>
        /// Max key of sin curve.
        /// </summary>
        public virtual float MaxKey { set; get; }
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        public USinCurve() : base() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="args">Args of sin curve.</param>
        public USinCurve(SinArgs args) : base(args) { }

        /// <summary>
        /// Get point on sin curve at x.
        /// </summary>
        /// <param name="x">Value of x axis.</param>
        /// <returns>The point on sin curve at x.</returns>
        public virtual Vector3 GetPointAt(float x)
        {
            return GetPointAt(args, x);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Get point on sin curve at x.
        /// </summary>
        /// <param name="args">Args of sin curve.</param>
        /// <param name="x">Value of x axis.</param>
        /// <returns>The point on sin curve at x.</returns>
        public static Vector3 GetPointAt(SinArgs args, float x)
        {
            return new Vector3(x, (float)Evaluate(args, x));
        }
        #endregion
    }
}