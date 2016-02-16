using UnityEngine;
using System.Collections;

public class MandalaEatingMonster : MonoBehaviour {

    public GameObject mandalaObj;
    public Transform mandala;
    public TeleportationController tc;

    private NavMeshAgent monster;

    private Vector3 startPos;
    private Transform monsterTr;

    private bool attacking;
    private bool moving;
    private bool returning;

    // Use this for initialization
    void Start () {
        monster = GetComponent<NavMeshAgent>();
        monsterTr = GetComponent<Transform>();
        startPos = monsterTr.position;

        attacking = false;
        moving  = false;
        returning = false;
    }

	
	// Update is called once per frame
	void Update ()  {
        //Maintain movement cycle.
        if (!moving && !attacking && !returning) { 
            StartCoroutine(movementCycle());
        }

        //Begins attack if mandala is close enough.
        if (!returning && (Vector3.Distance(mandala.position, monsterTr.position) < 8))
            attacking = true;


        //Attack cycle.
        if (attacking) {
            //Chase
            monsterTr.LookAt(mandala);
            monsterTr.Translate(5 * Vector3.forward * Time.deltaTime);

            StartCoroutine(setAttackLimit());
        }
        

        //Monster is back to its origin spot.
        if (Vector3.Distance(monsterTr.position, startPos) <= 1)
            returning = false;
    }


    void OnTriggerEnter(Collider obj)   {
        if (obj.gameObject.name == "Mandala") {
            //Disable teleportation.
            tc.canTeleport = false;
            mandalaObj.SetActive(false);

            StartCoroutine(reenableTeleportation());
        }
    }


    IEnumerator reenableTeleportation()   {
        yield return new WaitForSeconds(3); //time until teleportation is reactivated.
        tc.canTeleport = true;
        mandalaObj.SetActive(true);
    }


    /* Monster's movement cycle. Based on a random pattern of movement. */
    IEnumerator movementCycle() {
        moving = true;

        NavMeshHit hit;
        // Set a random point as destination.
        Vector3 radomPos = (Random.insideUnitSphere * 3) + startPos;
        NavMesh.SamplePosition(radomPos, out hit, 20, 1);
        monster.destination = hit.position;

        yield return new WaitForSeconds(3); // wait before allowing cycle to repeat.
        moving = false;
        
    }

    /* Takes monster back to starting point after some time. */
    IEnumerator setAttackLimit() {
        //Stop chasing after a time limit.
        yield return new WaitForSeconds(7);
        attacking = false;

        //Send monster back to start point.
        returning = true;
        monster.destination = startPos;
    }

    /* Disables mandala temporarily. */
    IEnumerator disableMandala() {
        yield return new WaitForSeconds(7);
    }
}
