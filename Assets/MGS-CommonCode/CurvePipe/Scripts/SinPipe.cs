/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SinPipe.cs
 *  Description  :  Render dynamic pipe mesh base on sin curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/31/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using Mogoson.Mathematics;
using UnityEngine;

namespace Mogoson.CurvePipe
{
    /// <summary>
    /// Render dynamic pipe mesh base on sin curve.
    /// </summary>
    [AddComponentMenu("Mogoson/CurvePipe/SinPipe")]
    public class SinPipe : MonoCurvePipe
    {
        #region Field and Property
        /// <summary>
        /// Args of sin curve.
        /// </summary>
        public SinArgs args = new SinArgs(1, 1, 0, 0);

        /// <summary>
        /// Max key of sin curve.
        /// </summary>
        public float maxKey = 2 * Mathf.PI;

        /// <summary>
        /// Curve for pipe.
        /// </summary>
        protected override ICurve Curve { get { return curve; } }

        /// <summary>
        /// Curve of pipe.
        /// </summary>
        protected USinCurve curve = new USinCurve();
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild pipe.
        /// </summary>
        public override void Rebuild()
        {
            curve.args = args;
            curve.MaxKey = maxKey;
            base.Rebuild();
        }
        #endregion
    }
}