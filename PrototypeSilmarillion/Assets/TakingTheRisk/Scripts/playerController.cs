using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

namespace Watikita
{


    public class playerController : MonoBehaviour
    {

        NavMeshAgent playerAgent;

        GameObject curSelectedObject;

        Vector3 targetCoord;
        public bool isMoving = false;
        bool willInteract = false;
        GameObject interactObject;

        Color targetOriginalColor;


        // GUI
        public GameObject townMenu;

        private void Start()
        {
            playerAgent = GetComponent<NavMeshAgent>();
            townMenu.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

            // Movement
            // If click on a party, select it and order player to interact/move
            // If nothing, it is terrain - player party moves to ray point
            if (Input.GetMouseButtonDown(0) && !townMenu.activeInHierarchy)
            {
                RaycastHit hitInfo = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hitInfo))
                {
                    // Move
                    playerAgent.SetDestination(hitInfo.point);

                    // If mouse selects a party, prepare interaction
                    if (
                        hitInfo.transform.gameObject.tag == "Town" ||
                        hitInfo.transform.gameObject.tag == "Village" ||
                        hitInfo.transform.gameObject.tag == "Castle" ||
                        hitInfo.transform.gameObject.tag == "Lord" ||
                        hitInfo.transform.gameObject.tag == "Caravan"
                        )

                    {
                        willInteract = true;
                        // Select
                        selectObject(hitInfo.transform.gameObject);
                    }
                    else  // fallback
                    {
                        willInteract = false;
                        deselectObject();
                    }

                    // Save target coordinate
                    targetCoord = hitInfo.point;

                    // Start distance check
                    isMoving = true;


                }
            }

            if (isMoving)
            {

                Debug.Log("Moving... " + Vector3.Distance(transform.position, targetCoord));

                if (Vector3.Distance(transform.position, targetCoord) <= 4)
                {
                    Debug.Log("Target reached.");

                    if (willInteract)
                    {
                        playerInteract(curSelectedObject);
                    }

                    targetCoord = Vector3.zero;
                    isMoving = false;
                }
            }


            // Pause game if player is not moving


        }

        void playerInteract(GameObject objToInteract)
        {
            if (objToInteract.gameObject.tag == "Castle")
            {
                Debug.Log("Castle menu opens.");
            }
            if (objToInteract.gameObject.tag == "Town")
            {
                townMenu.SetActive(true);
            }
            if (objToInteract.gameObject.tag == "Village")
            {
                Debug.Log("Village menu opens.");
            }
            if (objToInteract.gameObject.tag == "Lord")
            {
                Debug.Log("Lord dialog opens.");
            }
            if (objToInteract.gameObject.tag == "Caravan")
            {
                Debug.Log("Caravan dialog opens.");
            }
        }

        void selectObject(GameObject newObject)
        {


            MeshRenderer renderer = newObject.GetComponent<MeshRenderer>();
            targetOriginalColor = renderer.material.color;
            renderer.material.color = Color.white;

            curSelectedObject = newObject;
            Debug.Log("Selected " + newObject.name);
        }


        void deselectObject()
        {
            if (curSelectedObject != null)
            {
                curSelectedObject.GetComponent<MeshRenderer>().material.color = targetOriginalColor;
                curSelectedObject = null;
            }
        }

    }
}
