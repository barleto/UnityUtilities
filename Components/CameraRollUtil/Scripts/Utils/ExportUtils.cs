using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PKGames.CameraRollExporter;
using UnityEngine;
using UnityEngine.UI;

public class ExportUtils 
{
    //This const string can be changed and customized
	const string ExplorerPhotoPath = "/Explorer/Photos/";
    const string PhotoNamePrefix = "explorer_photo";

    public static IEnumerator ExportPhotoToCameraRoll(Camera camera, string albumName)
    {
        string photoName;

        if (!camera.gameObject.activeSelf)
        {
            camera.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
        }

        Texture2D screenshot = GenerateScreenshot(camera, camera.pixelWidth, camera.pixelHeight);
        SaveScreenshotAsFile(screenshot, PhotoNamePrefix, out photoName);

        camera.gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();

		ICameraRollExporter cameraRollExporter = CameraRollFactory.CreateCameraRollExporter();
        cameraRollExporter.SavePhotoToCameraRoll(photoName, albumName);
       
		//TODO log export event
		yield return null;
	}
	
	private static Texture2D GenerateScreenshot (Camera exportCamera, int width, int height)
	{
		RenderTexture renderTexture;
		
		exportCamera.rect = new Rect (0, 0, 1, 1);
		
		renderTexture = new RenderTexture (width, height, 24);
		renderTexture.isPowerOfTwo = false;
		renderTexture.antiAliasing = 8;

		renderTexture.useMipMap = false;
		RenderTexture.active = renderTexture;
		exportCamera.targetTexture = renderTexture;
		exportCamera.Render ();

		Texture2D texShot = new Texture2D (width, height, TextureFormat.RGB24, false);
		texShot.ReadPixels (new Rect (0, 0, width, height), 0, 0);
		texShot.Apply ();

		exportCamera.targetTexture = null;
		RenderTexture.active = null;
		renderTexture.Release ();

		texShot.anisoLevel = 9;
		texShot.filterMode = FilterMode.Bilinear;

		return texShot;
	}
	
	private static void SaveScreenshotAsFile (Texture2D screenshot, String nameWithoutExt, out String fullPath)
	{
		byte[] data = screenshot.EncodeToJPG (80);
		string extension = ".jpg";
		bool nameFound = false;
		string currentName = nameWithoutExt;
		int i = 0;
		
		while (!nameFound) 
		{
			if (!File.Exists (GetApplicationExportGallery () + currentName + extension)) 
			{
				nameFound = true;
			} 
			else 
			{
				currentName = string.Concat (nameWithoutExt, " (", (++i).ToString (), ")");
			}   
		}

		fullPath = GetApplicationExportGallery () + currentName + extension;

		FileStream fs = File.Create (fullPath);
        Debug.Log("Saving temp image at "+fullPath);
		fs.Write (data, 0, data.Length);
		fs.Close();
	}
	
	private static string GetApplicationExportGallery()
	{
		string rootPath = Application.persistentDataPath + ExplorerPhotoPath;
#if UNITY_IOS
		rootPath = GetiPhoneRootPath();
#elif UNITY_ANDROID
        rootPath = GetAndroidRootPath();
#endif
		if (!Directory.Exists(rootPath))
		{
			Directory.CreateDirectory(rootPath);
		}
		return rootPath;
	}
	
	private static String GetiPhoneRootPath()
	{
		return string.Concat (Application.persistentDataPath, ExplorerPhotoPath);
	}

	private static String GetAndroidRootPath()
	{
		String path = Application.persistentDataPath;
		return path + ExplorerPhotoPath;
	}
}
