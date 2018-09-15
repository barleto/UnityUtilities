//
//  NativeCameraRoll.h
//  PlayKids
//
//  Created by Pedro Almeida on 6/2/18.
//
//

#include "NativeCameraRollImpl.h"

extern "C"
{
    void _saveImageToPhotoAlbum(const char * photoPath)
    {
        SavePhotoToCameraRollImpl(photoPath);
    }
}
