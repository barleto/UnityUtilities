using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PKGames.CameraRollExporter
{
    public class CameraRollAndroid : ICameraRollExporter
	{
        const string javaPackageName = "com.movile.playkids.unityInterfaces";
        const string javaClassName = "CameraRollUtils";
        const string javaMethodName = "saveImageToCameraRoll";

        public void SavePhotoToCameraRoll(string fullPath, string albumName)
        {
            Debug.Log("Calling CameraRollUtils plugins");
            AndroidJavaClass _pluginClass = new AndroidJavaClass(javaPackageName + "." + javaClassName);
            _pluginClass.CallStatic(javaMethodName, fullPath, albumName );
        }
    }
}