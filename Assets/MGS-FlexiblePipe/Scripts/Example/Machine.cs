/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Machine.cs
 *  Description  :  Machine cable example.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/21/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.FlexiblePipe
{
    public class Machine : MonoBehaviour
    {
        #region Field and Property
        public Transform nozzle;
        public float offset = 0.75f;
        public float speed = 1.0f;

        public BezierPipe cable;
        public Transform joint;
        public Transform tangent;

        private Vector3 startPoint;
        private float currentOffset;
        #endregion

        #region Private Method
        private void Start()
        {
            startPoint = nozzle.localPosition;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                currentOffset -= speed * Time.deltaTime;
                MoveNozzle();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                currentOffset += speed * Time.deltaTime;
                MoveNozzle();
            }
        }

        private void MoveNozzle()
        {
            currentOffset = Mathf.Clamp(currentOffset, -offset, offset);
            nozzle.localPosition = startPoint + Vector3.right * currentOffset;

            cable.EndPoint = joint.position;
            cable.EndTangentPoint = tangent.position;
            cable.Rebuild();
        }
        #endregion
    }
}