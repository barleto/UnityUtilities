//  NativeCameraRollImplImpl
//  PlayKids
//
//  Created by Pedro Almeida on 6/2/18.

#import <Foundation/Foundation.h>
#import <Photos/Photos.h>
#include "ConversionUtils.h"

void SavePhotoToCameraRollImpl(const char * photoPath)
{
    NSString *photoString = CreateNSString(photoPath);
    NSLog(@"===========> %@", photoString);
    NSURL *photoUrl = [NSURL URLWithString:[photoString stringByAddingPercentEscapesUsingEncoding:NSUTF8StringEncoding]];
    
    [[PHPhotoLibrary sharedPhotoLibrary] performChanges:^{
        // Create a change request from the asset to be modified.
        [PHAssetChangeRequest creationRequestForAssetFromImageAtFileURL:photoUrl];
    } completionHandler:^(BOOL success, NSError *error) {
        if(success)
        {
            NSError *error;
            [[NSFileManager defaultManager] removeItemAtPath:photoString error:&error];
        }
        NSLog(@"Finished updating asset. %@", (success ? @"Success." : error));
    }];
}
