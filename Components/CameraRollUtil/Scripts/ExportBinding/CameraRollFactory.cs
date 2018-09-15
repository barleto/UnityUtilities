using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PKGames.CameraRollExporter
{
    public static class CameraRollFactory 
    {
        public static ICameraRollExporter CreateCameraRollExporter()
        {
            #if UNITY_EDITOR
            return new CameraRollEditor();
            #elif UNITY_ANDROID
            return new CameraRollAndroid();
            #elif UNITY_IOS
            return new CameraRollIOS();
            #else
            return null;
            #endif
        }
    }
}
