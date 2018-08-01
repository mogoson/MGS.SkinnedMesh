/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CirclePipe.cs
 *  Description  :  Render dynamic pipe mesh base on circle curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/18/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using UnityEngine;

namespace Mogoson.CurvePipe
{
    /// <summary>
    /// Render dynamic pipe mesh base on circle curve.
    /// </summary>
    [AddComponentMenu("Mogoson/CurvePipe/CirclePipe")]
    public class CirclePipe : MonoCurvePipe
    {
        #region Field and Property
        /// <summary>
        /// Extend radius of pipe curve.
        /// </summary>
        public float extendRadius = 1.0f;

        /// <summary>
        /// Curve for pipe.
        /// </summary>
        protected override ICurve Curve { get { return curve; } }

        /// <summary>
        /// Curve of pipe.
        /// </summary>
        protected EllipseCurve curve = new EllipseCurve();
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild the mesh of pipe.
        /// </summary>
        public override void Rebuild()
        {
            curve.ellipse.semiMinorAxis = extendRadius;
            curve.ellipse.semiMajorAxis = extendRadius;
            base.Rebuild();
        }
        #endregion
    }
}