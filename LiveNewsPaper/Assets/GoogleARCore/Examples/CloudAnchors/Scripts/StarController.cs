//-----------------------------------------------------------------------
// <copyright file="StarController.cs" company="Google">
//
// Copyright 2019 Google Inc. All Rights Reserved.
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

namespace GoogleARCore.Examples.CloudAnchors
{
    using GoogleARCore;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.UI;

    /// <summary>
    /// A controller for the Star object that handles setting mesh after the world origin has been
    /// placed.
    /// </summary>
    /// 
#pragma warning disable 618
    public class StarController : NetworkBehaviour
#pragma warning disable 618
    {
        /// <summary>
        /// The star mesh object.
        /// In order to avoid placing the star on identity pose, the mesh object should be disabled
        /// by default and enabled after the origin has been placed.
        /// </summary>
        private GameObject m_StarMesh;
        private GameObject m_NoteMesh;
        [SyncVar]
        public string Note;
       public GameObject inputField;
        public GameObject textDisplay;
        public GameObject StarOject;
        private GameObject NewStar;
        Transform mainCamTransform; // Stores the FPS camera transform
        private bool visible = true;
        public float distanceToAppear = 2F;
        Renderer objRenderer;
        Renderer NoteobjRenderer;
        /// <summary>
        /// The Cloud Anchors example controller.
        /// </summary>
        private CloudAnchorsExampleController m_CloudAnchorsExampleController;


        private void Start()
    {
            Random.InitState(12345); //This is the seed

            mainCamTransform = Camera.main.transform;//Get camera transform reference
            objRenderer =m_StarMesh.GetComponent<Renderer>(); //Get render reference
            NoteobjRenderer = m_NoteMesh.GetComponent<Renderer>();//Get render reference

        }
        /// <summary>
        /// The Unity Awake() method.
        /// </summary>
        /// 
        public void UpdateButtonClick()
        {
            Note = inputField.GetComponent<Text>().text;
            textDisplay.GetComponent<Text>().text = Note;
          
        }
        public void Awake()
        {
            m_CloudAnchorsExampleController =
                GameObject.Find("CloudAnchorsExampleController")
                    .GetComponent<CloudAnchorsExampleController>();
            m_StarMesh = transform.Find("StarMesh").gameObject;
            m_NoteMesh = transform.Find("NoteMesh").gameObject;
           
            m_NoteMesh.SetActive(false);
            m_StarMesh.SetActive(false);
        }

        /// <summary>
        /// The Unity Update() method.
        /// </summary>
        public void Update()
        {
          

            textDisplay.GetComponent<Text>().text = Note;

            if (m_StarMesh.activeSelf)
            {
                 disappearChecker();
                return;
            }

            // Only sets the Star object's mesh after the origin is placed to avoid being placed
            // at identity pose.
            if (!m_CloudAnchorsExampleController.IsOriginPlaced)
            {
                 disappearChecker();
                return;
            }
            //           

            disappearChecker();
            m_StarMesh.SetActive(true);
            m_NoteMesh.SetActive(true);
        }
        private void disappearChecker()
    {
        float distance = Vector3.Distance(mainCamTransform.position, transform.position);
            Debug.Log("Distance =" +distance);
          //  textDisplay.GetComponent<Text>().text = Note +"\nDistance ="+distance;

            // We have reached the distance to Enable Object
            if (distance < 2 )
        {
            if (!visible)
            {
                objRenderer.enabled = true; // Show Object
              //  NoteobjRenderer.enabled = true;
                visible = true;
                Debug.Log("Visible");
            }
        }
        else if (visible)
        {
            objRenderer.enabled = false; // Hide Object
          //  NoteobjRenderer.enabled = false;
            visible = false;
            Debug.Log("InVisible");
        }
    }
    }
}
