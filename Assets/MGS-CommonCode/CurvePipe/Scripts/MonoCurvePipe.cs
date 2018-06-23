/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurvePipe.cs
 *  Description  :  Define MonoCurvePipe to render dynamic pipe mesh base
 *                  on center curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Skin;
using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.CurvePipe
{
    /// <summary>
    /// Render dynamic pipe mesh base on center curve.
    /// </summary>
    public abstract class MonoCurvePipe : MonoSkin, ICurvePipe
    {
        #region Field and Property
        /// <summary>
        /// Segment of around pipe.
        /// </summary>
        [SerializeField]
        protected int around = 8;

        /// <summary>
        /// Segment of extend pipe.
        /// </summary>
        [SerializeField]
        protected int extend = 16;

        /// <summary>
        /// Radius of pipe mesh.
        /// </summary>
        [SerializeField]
        protected float radius = 0.1f;

        /// <summary>
        /// Is seal at both ends of pipe?
        /// </summary>
        [SerializeField]
        protected bool seal = false;

        /// <summary>
        /// Radian of circle.
        /// </summary>
        protected const float CircleRadian = Mathf.PI * 2;

        /// <summary>
        /// Delta to calculate tangent.
        /// </summary>
        protected const float Delta = 0.001f;

        /// <summary>
        /// Segment of around pipe.
        /// </summary>
        public int AroundSegment
        {
            set
            {
                around = value;
                Rebuild();
            }
            get { return around; }
        }

        /// <summary>
        /// Segment of extend pipe.
        /// </summary>
        public int ExtendSegment
        {
            set
            {
                extend = value;
                Rebuild();
            }
            get { return extend; }
        }

        /// <summary>
        /// Radius of pipe mesh.
        /// </summary>
        public float Radius
        {
            set
            {
                radius = value;
                Rebuild();
            }
            get { return radius; }
        }

        /// <summary>
        /// Is seal at both ends of pipe?
        /// </summary>
        public bool Seal
        {
            set
            {
                seal = value;
                Rebuild();
            }
            get { return seal; }
        }

        /// <summary>
        /// Max time of pipe center curve.
        /// </summary>
        public abstract float MaxTime { get; }
        #endregion

        #region Protected Method
        /// <summary>
        /// Create the vertices of pipe mesh.
        /// </summary>
        /// <returns>Vertices of pipe mesh.</returns>
        protected override Vector3[] CreateVertices()
        {
            var vertices = new List<Vector3>();
            var space = 1.0f / extend;
            for (int i = 0; i < extend; i++)
            {
                var t = i * space;
                var center = GetLocalPoint(t);
                var tangent = (GetLocalPoint(t + Delta) - center).normalized;
                vertices.AddRange(CreateSegmentVertices(center, Quaternion.LookRotation(tangent)));
            }

            var lastCenter = GetLocalPoint(1.0f);
            var lastTangent = (lastCenter - GetLocalPoint(1.0f - Delta)).normalized;
            vertices.AddRange(CreateSegmentVertices(lastCenter, Quaternion.LookRotation(lastTangent)));

            if (seal && around > 2)
            {
                vertices.Add(GetLocalPoint(0));
                vertices.Add(GetLocalPoint(1));
            }
            return vertices.ToArray();
        }

        /// <summary>
        /// Create triangles of pipe mesh.
        /// </summary>
        /// <returns>Triangles array.</returns>
        protected override int[] CreateTriangles()
        {
            var triangles = new List<int>();
            for (int i = 0; i < extend; i++)
            {
                for (int j = 0; j < around - 1; j++)
                {
                    triangles.Add(around * i + j);
                    triangles.Add(around * i + j + 1);
                    triangles.Add(around * (i + 1) + j + 1);

                    triangles.Add(around * i + j);
                    triangles.Add(around * (i + 1) + j + 1);
                    triangles.Add(around * (i + 1) + j);
                }

                triangles.Add(around * i);
                triangles.Add(around * (i + 1));
                triangles.Add(around * (i + 2) - 1);

                triangles.Add(around * i);
                triangles.Add(around * (i + 2) - 1);
                triangles.Add(around * (i + 1) - 1);
            }

            if (seal && around > 2)
            {
                for (int i = 0; i < around - 1; i++)
                {
                    triangles.Add(around * (extend + 1));
                    triangles.Add(i + 1);
                    triangles.Add(i);

                    triangles.Add(around * (extend + 1) + 1);
                    triangles.Add(around * extend + i);
                    triangles.Add(around * extend + i + 1);
                }

                triangles.Add(around * (extend + 1));
                triangles.Add(0);
                triangles.Add(around - 1);

                triangles.Add(around * (extend + 1) + 1);
                triangles.Add(around * (extend + 1) - 1);
                triangles.Add(around * extend);
            }
            return triangles.ToArray();
        }

        /// <summary>
        /// Create vertices of current segment base pipe.
        /// </summary>
        /// <param name="center">Center point of segment.</param>
        /// <param name="rotation">Rotation of segment vertices.</param>
        /// <returns>Segment vertices.</returns>
        protected virtual Vector3[] CreateSegmentVertices(Vector3 center, Quaternion rotation)
        {
            var vertices = new Vector3[around];
            for (int i = 0; i < around; i++)
            {
                var angle = CircleRadian / around * i;
                var vertice = center + rotation * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                vertices[i] = vertice;
            }
            return vertices;
        }

        /// <summary>
        /// Get local point from center curve of pipe at normalized time.
        /// </summary>
        /// <param name="t">Normalized time in the range(0~1).</param>
        /// <returns>Local point on pipe curve at t.</returns>
        protected Vector3 GetLocalPoint(float t)
        {
            return GetLocalPointAt(MaxTime * t);
        }

        /// <summary>
        /// Get local point from center curve of pipe at time.
        /// </summary>
        /// <param name="time">Time of pipe center curve.</param>
        /// <returns>Local point on pipe curve at time.</returns>
        protected abstract Vector3 GetLocalPointAt(float time);
        #endregion

        #region Public Method
        /// <summary>
        /// Get point from center curve of pipe at time.
        /// </summary>
        /// <param name="time">Time of pipe center curve.</param>
        /// <returns>Point on pipe curve at time.</returns>
        public Vector3 GetPointAt(float time)
        {
            return transform.TransformPoint(GetLocalPointAt(time));
        }
        #endregion
    }
}