/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ICurvePipe.cs
 *  Description  :  Define interface of curve pipe.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/23/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;

namespace Mogoson.CurvePipe
{
    /// <summary>
    /// Interface of curve pipe.
    /// </summary>
    public interface ICurvePipe : ICurve
    {
        #region Field and Property
        /// <summary>
        /// Segment of around pipe.
        /// </summary>
        int AroundSegment { set; get; }

        /// <summary>
        /// Segment of extend pipe.
        /// </summary>
        int ExtendSegment { set; get; }

        /// <summary>
        /// Radius of pipe mesh.
        /// </summary>
        float Radius { set; get; }

        /// <summary>
        /// Is seal at both ends of pipe?
        /// </summary>
        bool Seal { set; get; }

        /// <summary>
        /// Max time of pipe center curve.
        /// </summary>
        float MaxTime { get; }
        #endregion
    }
}