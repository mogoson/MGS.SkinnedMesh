/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveHose.cs
 *  Description  :  Define MonoCurveHose to render dynamic hose mesh base
 *                  on center curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using Mogoson.Skin;
using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.CurveHose
{
    /// <summary>
    /// Render dynamic hose mesh base on center curve.
    /// </summary>
    public abstract class MonoCurveHose : MonoSkin, ICurveHose
    {
        #region Field and Property
        /// <summary>
        /// Polygon of hose cross section.
        /// </summary>
        [SerializeField]
        protected int polygon = 8;

        /// <summary>
        /// Segment length of subdivide hose.
        /// </summary>
        [SerializeField]
        protected float segment = 0.25f;

        /// <summary>
        /// Radius of hose mesh.
        /// </summary>
        [SerializeField]
        protected float radius = 0.1f;

        /// <summary>
        /// Is seal at both ends of hose?
        /// </summary>
        [SerializeField]
        protected bool seal = false;

        /// <summary>
        /// Polygon of hose cross section.
        /// </summary>
        public int Polygon
        {
            set { polygon = value; }
            get { return polygon; }
        }

        /// <summary>
        ///  Segment length of subdivide hose.
        /// </summary>
        public float Segment
        {
            set { segment = value; }
            get { return segment; }
        }

        /// <summary>
        /// Radius of hose mesh.
        /// </summary>
        public float Radius
        {
            set { radius = value; }
            get { return radius; }
        }

        /// <summary>
        /// Is seal at both ends of hose?
        /// </summary>
        public bool Seal
        {
            set { seal = value; }
            get { return seal; }
        }

        /// <summary>
        /// Max key of hose center curve.
        /// </summary>
        public virtual float MaxKey { get { return Curve.MaxKey; } }

        /// <summary>
        /// Length of hose center curve.
        /// </summary>
        public virtual float Length { get { return length; } }

        /// <summary>
        /// Curve for hose.
        /// </summary>
        protected abstract ICurve Curve { get; }

        /// <summary>
        /// Radian of circle.
        /// </summary>
        protected const float CircleRadian = 2 * Mathf.PI;

        /// <summary>
        /// Delta to calculate tangent.
        /// </summary>
        protected const float Delta = 0.001f;

        /// <summary>
        /// Length of hose center curve.
        /// </summary>
        protected float length = 0.0f;

        /// <summary>
        /// Segment count of subdivide hose.
        /// </summary>
        protected int segmentCount = 0;
        #endregion

        #region Protected Method
        /// <summary>
        /// Create the vertices of hose mesh.
        /// </summary>
        /// <returns>Vertices array.</returns>
        protected override Vector3[] CreateVertices()
        {
            var vertices = new List<Vector3>();
            var keySegment = MaxKey / segmentCount;
            for (int i = 0; i < segmentCount; i++)
            {
                var key = keySegment * i;
                var center = Curve.GetPointAt(key);
                var tangent = (Curve.GetPointAt(key + Delta) - center).normalized;
                vertices.AddRange(CreateSegmentVertices(center, Quaternion.LookRotation(tangent)));
            }

            var lastCenter = Curve.GetPointAt(MaxKey);
            var lastTangent = (lastCenter - Curve.GetPointAt(MaxKey - Delta)).normalized;
            vertices.AddRange(CreateSegmentVertices(lastCenter, Quaternion.LookRotation(lastTangent)));

            if (seal && polygon > 2)
            {
                vertices.Add(Curve.GetPointAt(0));
                vertices.Add(Curve.GetPointAt(MaxKey));
            }
            return vertices.ToArray();
        }

        /// <summary>
        /// Create triangles of hose mesh.
        /// </summary>
        /// <returns>Triangles array.</returns>
        protected override int[] CreateTriangles()
        {
            var triangles = new List<int>();
            for (int i = 0; i < segmentCount; i++)
            {
                for (int j = 0; j < polygon - 1; j++)
                {
                    triangles.Add(polygon * i + j);
                    triangles.Add(polygon * i + j + 1);
                    triangles.Add(polygon * (i + 1) + j + 1);

                    triangles.Add(polygon * i + j);
                    triangles.Add(polygon * (i + 1) + j + 1);
                    triangles.Add(polygon * (i + 1) + j);
                }

                triangles.Add(polygon * i);
                triangles.Add(polygon * (i + 1));
                triangles.Add(polygon * (i + 2) - 1);

                triangles.Add(polygon * i);
                triangles.Add(polygon * (i + 2) - 1);
                triangles.Add(polygon * (i + 1) - 1);
            }

            if (seal && polygon > 2)
            {
                for (int i = 0; i < polygon - 1; i++)
                {
                    triangles.Add(polygon * (segmentCount + 1));
                    triangles.Add(i + 1);
                    triangles.Add(i);

                    triangles.Add(polygon * (segmentCount + 1) + 1);
                    triangles.Add(polygon * segmentCount + i);
                    triangles.Add(polygon * segmentCount + i + 1);
                }

                triangles.Add(polygon * (segmentCount + 1));
                triangles.Add(0);
                triangles.Add(polygon - 1);

                triangles.Add(polygon * (segmentCount + 1) + 1);
                triangles.Add(polygon * (segmentCount + 1) - 1);
                triangles.Add(polygon * segmentCount);
            }
            return triangles.ToArray();
        }

        /// <summary>
        /// Create uv of hose mesh.
        /// </summary>
        /// <returns>UV array.</returns>
        protected override Vector2[] CreateUV()
        {
            return null;
        }

        /// <summary>
        /// Create vertices of current segment base hose.
        /// </summary>
        /// <param name="center">Center point of segment.</param>
        /// <param name="rotation">Rotation of segment vertices.</param>
        /// <returns>Segment vertices.</returns>
        protected virtual Vector3[] CreateSegmentVertices(Vector3 center, Quaternion rotation)
        {
            var vertices = new Vector3[polygon];
            for (int i = 0; i < polygon; i++)
            {
                var angle = CircleRadian / polygon * i;
                var vertice = center + rotation * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                vertices[i] = vertice;
            }
            return vertices;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild the mesh of hose.
        /// </summary>
        public override void Rebuild()
        {
            length = Curve.Length;
            segmentCount = (int)(length / segment);
            base.Rebuild();
        }

        /// <summary>
        /// Get point from center curve of hose at key.
        /// </summary>
        /// <param name="key">Key of hose center curve.</param>
        /// <returns>Point on hose curve at key.</returns>
        public Vector3 GetPointAt(float key)
        {
            return transform.TransformPoint(Curve.GetPointAt(key));
        }
        #endregion
    }
}