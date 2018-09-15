using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PKGames.CameraRollExporter
{
    public interface ICameraRollExporter
    {
        void SavePhotoToCameraRoll(string fullPath, string albumName);
    }
}
