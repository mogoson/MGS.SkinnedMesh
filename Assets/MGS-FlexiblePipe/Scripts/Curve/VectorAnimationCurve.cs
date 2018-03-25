/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  VectorAnimationCurve.cs
 *  Description  :  AnimationCurve in three dimensional space.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/21/2017
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  2/28/2018
 *  Description  :  Add the static method FromAnchors.
 *************************************************************************/

using System;
using UnityEngine;

namespace Mogoson.Curve
{
    /// <summary>
    /// Key frame data.
    /// </summary>
    [Serializable]
    public struct VectorKeyframe
    {
        #region Field and Property
        /// <summary>
        /// Time of key frame.
        /// </summary>
        public float time;

        /// <summary>
        /// Value of key frame.
        /// </summary>
        public Vector3 value;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="time">Time of key frame.</param>
        /// <param name="value">Value of key frame.</param>
        public VectorKeyframe(float time, Vector3 value)
        {
            this.time = time;
            this.value = value;
        }
        #endregion
    }

    /// <summary>
    /// AnimationCurve in three dimensional space.
    /// </summary>
    public class VectorAnimationCurve
    {
        #region Field and Property
        public VectorKeyframe this[int index]
        {
            get
            {
                return new VectorKeyframe(xCurve[index].time, new Vector3(xCurve[index].value, yCurve[index].value, zCurve[index].value));
            }
        }

        /// <summary>
        /// Keyframe count.
        /// </summary>
        public int Length { get { return xCurve.length; } }

        /// <summary>
        /// The behaviour of the animation after the last keyframe.
        /// </summary>
        public WrapMode PostWrapMode
        {
            set { xCurve.postWrapMode = yCurve.postWrapMode = zCurve.postWrapMode = value; }
            get { return xCurve.postWrapMode; }
        }

        /// <summary>
        /// The behaviour of the animation before the first keyframe.
        /// </summary>
        public WrapMode PreWrapMode
        {
            set { xCurve.preWrapMode = yCurve.preWrapMode = zCurve.preWrapMode = value; }
            get { return xCurve.preWrapMode; }
        }

        protected AnimationCurve xCurve;
        protected AnimationCurve yCurve;
        protected AnimationCurve zCurve;
        #endregion

        #region Public Method
        public VectorAnimationCurve()
        {
            xCurve = new AnimationCurve();
            yCurve = new AnimationCurve();
            zCurve = new AnimationCurve();
        }

        /// <summary>
        /// Add a new key to the curve.
        /// </summary>
        /// <param name="key">The key to add to the curve.</param>
        /// <returns>The index of the added key, or -1 if the key could not be added.</returns>
        public int AddKey(VectorKeyframe key)
        {
            xCurve.AddKey(key.time, key.value.x);
            yCurve.AddKey(key.time, key.value.y);
            return zCurve.AddKey(key.time, key.value.z);
        }

        /// <summary>
        /// Add a new key to the curve.
        /// </summary>
        /// <param name="time">The time at which to add the key (horizontal axis in the curve graph).</param>
        /// <param name="value">The value for the key (vertical axis in the curve graph).</param>
        /// <returns>The index of the added key, or -1 if the key could not be added.</returns>
        public int AddKey(float time, Vector3 value)
        {
            xCurve.AddKey(time, value.x);
            yCurve.AddKey(time, value.y);
            return zCurve.AddKey(time, value.z);
        }

        /// <summary>
        /// Evaluate the curve at time.
        /// </summary>
        /// <param name="time">The time within the curve you want to evaluate (the horizontal axis in the curve graph).</param>
        /// <returns>The value of the curve, at the point in time specified.</returns>
        public Vector3 Evaluate(float time)
        {
            return new Vector3(xCurve.Evaluate(time), yCurve.Evaluate(time), zCurve.Evaluate(time));
        }

        /// <summary>
        /// Removes a key.
        /// </summary>
        /// <param name="index">The index of the key to remove.</param>
        public void RemoveKey(int index)
        {
            xCurve.RemoveKey(index);
            yCurve.RemoveKey(index);
            zCurve.RemoveKey(index);
        }

        /// <summary>
        /// Smooth the in and out tangents of the keyframe at index.
        /// </summary>
        /// <param name="index">The index of the keyframe to be smoothed.</param>
        /// <param name="weight">The smoothing weight to apply to the keyframe's tangents.</param>
        public void SmoothTangents(int index, float weight)
        {
            xCurve.SmoothTangents(index, weight);
            yCurve.SmoothTangents(index, weight);
            zCurve.SmoothTangents(index, weight);
        }

        /// <summary>
        /// Smooth the in and out tangents of keyframes.
        /// </summary>
        /// <param name="weight">The smoothing weight to apply to the keyframe's tangents.</param>
        public void SmoothTangents(float weight)
        {
            for (int i = 0; i < Length; i++)
            {
                SmoothTangents(i, weight);
            }
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Create a curve base on anchors.
        /// </summary>
        /// <param name="anchors">Anchor points of curve.</param>
        /// <param name="close">Curve is close.</param>
        /// <returns>New curve.</returns>
        public static VectorAnimationCurve FromAnchors(Vector3[] anchors, bool close = false)
        {
            var curve = new VectorAnimationCurve();

            //No anchor.
            if (anchors == null || anchors.Length == 0)
                Debug.LogWarning("Created a curve with no key frame : The anchors is null or empty.");
            else
            {
                //Add frame keys to curve.
                var time = 0f;
                for (int i = 0; i < anchors.Length - 1; i++)
                {
                    curve.AddKey(time, anchors[i]);
                    time += Vector3.Distance(anchors[i], anchors[i + 1]);
                }

                //Add the last key.
                curve.AddKey(time, anchors[anchors.Length - 1]);

                if (close)
                {
                    //Add the close key(the first key).
                    time += Vector3.Distance(anchors[anchors.Length - 1], anchors[0]);
                    curve.AddKey(time, anchors[0]);
                }

                //Smooth curve keys out tangent.
                curve.SmoothTangents(0);
            }
            return curve;
        }
        #endregion
    }
}