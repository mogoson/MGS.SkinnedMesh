/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SinCurve.cs
 *  Description  :  Define sin curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace Mogoson.Mathematics
{
    /// <summary>
    /// Args of sin curve.
    /// </summary>
    [Serializable]
    public struct SinArgs
    {
        #region Field and Property
        /// <summary>
        /// Amplitude of sin curve.
        /// </summary>
        public double amplitude;

        /// <summary>
        /// Angular of sin curve.
        /// </summary>
        public double angular;

        /// <summary>
        /// Initial phase of sin curve.
        /// </summary>
        public double phase;

        /// <summary>
        /// Setover of sin curve.
        /// </summary>
        public double setover;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="amplitude">Amplitude of sin curve.</param>
        /// <param name="angular">Angular of sin curve.</param>
        /// <param name="phase">Initial phase of sin curve.</param>
        /// <param name="setover">Setover of sin curve.</param>
        public SinArgs(double amplitude, double angular, double phase, double setover)
        {
            this.amplitude = amplitude;
            this.angular = angular;
            this.phase = phase;
            this.setover = setover;
        }
        #endregion
    }

    /// <summary>
    ///  sin curve.
    /// </summary>
    public class SinCurve
    {
        #region Field and Property
        /// <summary>
        /// Args of sin curve.
        /// </summary>
        public SinArgs args;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        public SinCurve()
        {
            args = new SinArgs();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="args">Args of sin curve.</param>
        public SinCurve(SinArgs args)
        {
            this.args = args;
        }

        /// <summary>
        /// Evaluate the value of sin curve at x.
        /// </summary>
        /// <param name="x">Value of x axis.</param>
        /// <returns>The value of sin curve at x.</returns>
        public virtual double Evaluate(double x)
        {
            return Evaluate(args, x);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Evaluate the value of sin curve at x.
        /// </summary>
        /// <param name="args">Args of sin curve.</param>
        /// <param name="x">Value of x axis.</param>
        /// <returns>The value of sin curve at x.</returns>
        public static double Evaluate(SinArgs args, double x)
        {
            return args.amplitude * Math.Sin(args.angular * x + args.phase) + args.setover;
        }
        #endregion
    }
}