//-----------------------------------------------------------------------
// <copyright file="AugmentedImageVisualizer.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.AugmentedImage
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using GoogleARCore;
    using GoogleARCoreInternal;
    using UnityEngine;

    using UnityEngine.Video;
    /// <summary>
    /// Uses 4 frame corner objects to visualize an AugmentedImage.
    /// </summary>
    public class AugmentedImageVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;

        [SerializeField] private VideoClip[] _videoClip;
        /// <summary>
        /// A model for the lower left corner of the frame to place when an image is detected.
        // /// </summary> 
        // public GameObject FrameLowerLeft;

        // /// <summary>
        // /// A model for the lower right corner of the frame to place when an image is detected.
        // /// </summary>
        // public GameObject FrameLowerRight;

        // /// <summary>
        // /// A model for the upper left corner of the frame to place when an image is detected.
        // /// </summary>
        // public GameObject FrameUpperLeft;

        // /// <summary>
        // /// A model for the upper right corner of the frame to place when an image is detected.
        // /// </summary>
        // public GameObject FrameUpperRight;
        public GameObject VideoObject;
        private VideoPlayer _videoPlayer;
void Start()
    {
        _videoPlayer=VideoObject.GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached +=OnStop;    
   // transform.localScale=new Vector3(Image.ExtentX*10,1,Image.ExtentZ*10);
    }
    void OnStop(VideoPlayer source)
    {
        gameObject.SetActive(false);
    }
    int getVideoIndex(int imageIndex)
    {
        if(imageIndex==15 || imageIndex==16)
        {
            return 2;
        }
        else if(imageIndex==14)
        {
            return 1;
        }
        else if(imageIndex%3==1)
        {
            return 3;
        }
        else if(imageIndex%3==0)
        {
            return 0;
        }
        return 4;
    }
    
        /// <summary>
        /// The Unity Update method.
        /// </summary>
        public void Update()
        {
            if (Image == null || Image.TrackingState != TrackingState.Tracking)
             {
                   gameObject.SetActive(false);
            //     FrameLowerLeft.SetActive(false);
            //     FrameLowerRight.SetActive(false);
            //     FrameUpperLeft.SetActive(false);
            //     FrameUpperRight.SetActive(false);
                return;
            }
            if(!_videoPlayer.isPlaying)
            {
                _videoPlayer.clip=_videoClip[getVideoIndex(Image.DatabaseIndex)];
                    _videoPlayer.Play();                
            }
            
            //    transform.localScale=new Vector3(Image.ExtentX,Image.ExtentZ,1);
        /*   
            float halfWidth = Image.ExtentX / 2;
            float halfHeight = Image.ExtentZ / 2;
            FrameLowerLeft.transform.localPosition =
                (halfWidth * Vector3.left) + (halfHeight * Vector3.back);
            FrameLowerRight.transform.localPosition =
                (halfWidth * Vector3.right) + (halfHeight * Vector3.back);
            FrameUpperLeft.transform.localPosition =
                (halfWidth * Vector3.left) + (halfHeight * Vector3.forward);
            FrameUpperRight.transform.localPosition =
                (halfWidth * Vector3.right) + (halfHeight * Vector3.forward);

            FrameLowerLeft.SetActive(true);
            FrameLowerRight.SetActive(true);
            FrameUpperLeft.SetActive(true);
            FrameUpperRight.SetActive(true);
     */   }
    }
}
