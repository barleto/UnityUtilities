using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace PKGames.CameraRollExporter
{
	public class CameraRollIOS : ICameraRollExporter
	{
		[DllImport("__Internal")]
		private static extern void _saveImageToPhotoAlbum(string filePath);
		
		public void SavePhotoToCameraRoll(string filePath, string albumName)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				_saveImageToPhotoAlbum( filePath );
			}
		}
	}
}
