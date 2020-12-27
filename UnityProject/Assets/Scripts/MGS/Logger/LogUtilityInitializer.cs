/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LogUtilityInitializer.cs
 *  Description  :  Initializer for log utility.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  9/19/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Logger
{
    /// <summary>
    /// Initializer for log utility.
    /// </summary>
    sealed class LogUtilityInitializer
    {
        #region Public Method
        /// <summary>
        /// Awake initializer.
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        static void Awake()
        {
            //Awake automatic execute after MonoBehaviour.Awake,
            //so if you need to output logs earlier,
            //your should move the following codes to your game start.

#if UNITY_EDITOR
            LogUtility.AddLogger(new UnityDebugger());
#else
            //Use persistentDataPath support more platforms, example Android.
            var logDir = string.Format("{0}/Log/", Application.persistentDataPath);
            LogUtility.AddLogger(new FileLogger(logDir));
#endif
        }
        #endregion
    }
}