/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AnchorPipe.cs
 *  Description  :  Render dynamic pipe mesh base on anchor vector
 *                  animation curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.CurvePipe
{
    /// <summary>
    /// Render dynamic pipe mesh base on anchor vector animation curve.
    /// </summary>
    [AddComponentMenu("Mogoson/CurvePipe/AnchorPipe")]
    public class AnchorPipe : MonoCurvePipe
    {
        #region Field and Property
        /// <summary>
        /// Anchors of pipe curve.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        protected List<Vector3> anchors = new List<Vector3>() { Vector3.one,
            new Vector3(1, 1, 2), new Vector3(3, 1, 2), new Vector3(3, 1, 3)};

        /// <summary>
        /// Count of pipe curve anchors.
        /// </summary>
        public int AnchorsCount { get { return anchors.Count; } }

        /// <summary>
        /// Max time of pipe curve.
        /// </summary>
        public override float MaxTime
        {
            get
            {
                if (curve.Length > 0)
                    return curve[curve.Length - 1].time;
                else
                    return 0;
            }
        }

        /// <summary>
        /// VectorAnimationCurve of pipe.
        /// </summary>
        protected VectorAnimationCurve curve = new VectorAnimationCurve();
        #endregion

        #region Protected Method
        /// <summary>
        /// Get local point from center curve of pipe at time.
        /// </summary>
        /// <param name="time">Time of pipe center curve.</param>
        /// <returns>Local point on pipe curve at time.</returns>
        protected override Vector3 GetLocalPointAt(float time)
        {
            return curve.GetPointAt(time);
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild the mesh of pipe.
        /// </summary>
        public override void Rebuild()
        {
            curve = VectorAnimationCurve.FromAnchors(anchors.ToArray());
            base.Rebuild();
        }

        /// <summary>
        /// Add anchor item.
        /// </summary>
        /// <param name="item">Anchor item.</param>
        public void AddAnchor(Vector3 item)
        {
            anchors.Add(transform.InverseTransformPoint(item));
        }

        /// <summary>
        /// Insert Anchor item at index.
        /// </summary>
        /// <param name="index">Index of anchor.</param>
        /// <param name="item">Anchor item.</param>
        public void InsertAnchor(int index, Vector3 item)
        {
            anchors.Insert(index, transform.InverseTransformPoint(item));
        }

        /// <summary>
        /// Set the anchor item at index.
        /// </summary>
        /// <param name="index">Index of anchor.</param>
        /// <param name="item">Anchor item.</param>
        public void SetAnchorAt(int index, Vector3 item)
        {
            anchors[index] = transform.InverseTransformPoint(item);
        }

        /// <summary>
        /// Get the anchor item at index.
        /// </summary>
        /// <param name="index">Anchor index.</param>
        /// <returns>Anchor item.</returns>
        public Vector3 GetAnchorAt(int index)
        {
            return transform.TransformPoint(anchors[index]);
        }

        /// <summary>
        /// Remove the anchor item.
        /// </summary>
        /// <param name="item">Anchor item.</param>
        public void RemoveAnchor(Vector3 item)
        {
            anchors.Remove(item);
        }

        /// <summary>
        /// Remove the anchor item at index.
        /// </summary>
        /// <param name="index">Anchor index.</param>
        public void RemoveAnchorAt(int index)
        {
            anchors.RemoveAt(index);
        }

        /// <summary>
        /// Clear all anchor items.
        /// </summary>
        public void ClearAnchors()
        {
            anchors.Clear();
        }
        #endregion
    }
}