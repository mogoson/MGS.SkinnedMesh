/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Skin.cs
 *  Description  :  Define Skin to render dynamic mesh.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.SkinnedMesh
{
    /// <summary>
    /// Render dynamic skinned mesh.
    /// </summary>
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public abstract class Skin : MonoBehaviour
    {
        #region Field and Property
        /// <summary>
        /// Skinned mesh renderer of skin.
        /// </summary>
        public SkinnedMeshRenderer Renderer { protected set; get; }

        /// <summary>
        /// Mesh collider of skin.
        /// </summary>
        public MeshCollider Collider { protected set; get; }

        /// <summary>
        /// Mesh of skin.
        /// </summary>
        public Mesh Mesh { protected set; get; }
        #endregion

        #region Protected Method
        protected virtual void Reset()
        {
            Awake();
        }

        protected virtual void Awake()
        {
            Renderer = GetComponent<SkinnedMeshRenderer>();
            Collider = GetComponent<MeshCollider>();

            Mesh = new Mesh { name = "Skin" };
            Rebuild();
        }

        /// <summary>
        /// Create vertices of skin mesh.
        /// </summary>
        /// <returns></returns>
        protected abstract Vector3[] CreateVertices();

        /// <summary>
        /// Create triangles of skin mesh.
        /// </summary>
        /// <returns>Triangles of skin mesh.</returns>
        protected abstract int[] CreateTriangles();
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild the mesh of skin.
        /// </summary>
        public virtual void Rebuild()
        {
            Mesh.Clear();
            Mesh.vertices = CreateVertices();
            Mesh.triangles = CreateTriangles();

            Mesh.RecalculateNormals();
            Mesh.RecalculateBounds();

            Renderer.sharedMesh = Mesh;
            Renderer.localBounds = Mesh.bounds;

            if (Collider)
                Collider.sharedMesh = Mesh;
        }

        /// <summary>
        /// Attach MeshCollider to skin.
        /// </summary>
        public void AttachCollider()
        {
            if (Collider)
                return;
            else
            {
                var collider = GetComponent<MeshCollider>();
                if (collider == null)
                    collider = gameObject.AddComponent<MeshCollider>();
                Collider = collider;
            }
        }

        /// <summary>
        /// Remove MeshCollider from skin.
        /// </summary>
        public void RemoveCollider()
        {
            if (Collider)
                Destroy(Collider);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Rebuild the mesh of skin in editor (Only call this method in editor script).
        /// </summary>
        public void RebuildInEditor()
        {
            if (Renderer == null)
                Renderer = GetComponent<SkinnedMeshRenderer>();

            if (Collider == null)
                Collider = GetComponent<MeshCollider>();

            if (Mesh == null)
                Mesh = new Mesh { name = "Skin" };

            Rebuild();
        }
#endif
        #endregion
    }
}