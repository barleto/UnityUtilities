using System.Collections;
using System.Collections.Generic;
using PKGames.CameraRollExporter;
using UnityEngine;

namespace PKGames.CameraRollExporter
{
    public class CameraRollEditor : ICameraRollExporter
    {
        public void SavePhotoToCameraRoll(string fullPath, string albumName)
        {
            Debug.Log("CameraRollEditor: Should save image to camera roll in native.");
        }
    }
}
