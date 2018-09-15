using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class CaptureCameraPhoto : MonoBehaviour {
    
    public Camera CameraToPhotograph;
    public string AlbumName;
    public Image cameraFlash;
    public float flashTime = 1;

    [SerializeField]
    bool _debugGUI = false;

    private Camera _camera;
    private bool isTakingPhoto = false;

    public void Awake()
    {
        _camera = GetComponent<Camera>();
        if (cameraFlash != null)
        {
            var flashColor = cameraFlash.color;
            flashColor.a = 0;
            cameraFlash.color = flashColor;
        }
    }

    public void CapturePhoto(){
        if(isTakingPhoto){
            return;
        }
        gameObject.SetActive(true);
        _camera.transform.position = CameraToPhotograph.transform.position;
        _camera.transform.rotation = CameraToPhotograph.transform.rotation;
        _camera.farClipPlane = CameraToPhotograph.farClipPlane;
        _camera.nearClipPlane = CameraToPhotograph.nearClipPlane;
        _camera.fieldOfView = CameraToPhotograph.fieldOfView;
        cameraFlash.FlashImage(flashTime,OnFinishedTakingPhoto);
        isTakingPhoto = true;
        StartCoroutine(ExportUtils.ExportPhotoToCameraRoll(_camera, AlbumName));
    }

    public void OnFinishedTakingPhoto(){
        isTakingPhoto = false;
    }

    void OnGUI()
    {   
        if(!_debugGUI){
            return;
        }
        if(GUILayout.Button("Capture Photo")){
            CapturePhoto();
        }
    }
}
