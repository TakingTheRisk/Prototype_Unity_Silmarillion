using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class partyData : MonoBehaviour
{

    NavMeshAgent agentAI;
    public bool isActive;

    GameObject[] towns;
    GameObject[] castles;
    GameObject[] caravans;
    GameObject[] lords;
    GameObject[] villages;
    public enum ownerIndex { major_1, major_2, major_3, major_4, minor_1, minor_2, outlaws}
    public ownerIndex ownerFaction;

    float counterSinceLastMovement;
    float l_counterSinceLastMovement;

    public GameObject nameObj;
    private void Start()
    {
        nameObj.GetComponent<TextMesh>().text = this.gameObject.name;
        agentAI = GetComponent<NavMeshAgent>();
        
        towns = GameObject.FindGameObjectsWithTag("Town");

        if (this.gameObject.tag == "Caravan")
        {
            int targetIndex = Random.Range(0, towns.Length);
            GameObject targetTown = towns[targetIndex];
            agentAI.SetDestination(targetTown.transform.position);
        }

        if (this.gameObject.tag == "Lord")
        {
            int targetIndex = Random.Range(0, towns.Length);
            GameObject targetTown = towns[targetIndex];
            Vector3 targetPosition = new Vector3(
                towns[targetIndex].transform.position.x + Random.Range(-5, 5),
                towns[targetIndex].transform.position.y + Random.Range(-5, 5),
                towns[targetIndex].transform.position.z + Random.Range(-5, 5));
            agentAI.SetDestination(targetPosition);
        }
    }

    private void Update()
    {

        orderCaravanMove();
        orderLordPatrol();
    }


    void orderCaravanMove()
    {
        if (this.gameObject.tag == "Caravan")
        {
            counterSinceLastMovement += Time.deltaTime;
            if (counterSinceLastMovement > 12)
            {
                int targetIndex = Random.Range(0, towns.Length);
                GameObject targetTown = towns[targetIndex];
                agentAI.SetDestination(targetTown.transform.position);
                counterSinceLastMovement = 0;
            }


        }

    }

    void orderLordPatrol()
    {
        if (this.gameObject.tag == "Lord")
        {
            l_counterSinceLastMovement += Time.deltaTime;
            if (l_counterSinceLastMovement > 24)
            {
                int targetIndex = Random.Range(0, towns.Length);
                GameObject targetTown = towns[targetIndex];
                Vector3 targetPosition = new Vector3(
                    towns[targetIndex].transform.position.x + Random.Range(-5, 5),
                    towns[targetIndex].transform.position.y + Random.Range(-5, 5),
                    towns[targetIndex].transform.position.z + Random.Range(-5, 5));
                agentAI.SetDestination(targetPosition);
                l_counterSinceLastMovement = 0;
            }


        }

    }

}
