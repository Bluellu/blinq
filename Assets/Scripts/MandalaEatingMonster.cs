using UnityEngine;
using System.Collections;

/* Monster which chases the player's mandala and disables teleportation for a
few seconds once it is touched. Can be optionally connected to a KillerPartner. */
public class MandalaEatingMonster : MonoBehaviour {

    /* Movement parameters. */
    private float moveInterval;   //Seconds between each move.
    private float chaseTime;      //Number of seconds monster will chase for.
    private float chaseVelocity;  //How fast monster chases.
    private float chaseProximity; //How close player has to be to trigger chase.
    private float chaseLimitDist;     //Distance at which monster will quit chasing.
    private float teleportDisableTime; //How long teleportation is disabled for in secs.

    private GameObject mandala;
    private TeleportationController tc;
    public MandalaKillerMonster nearbyKiller;

    //private GameObject facingSphere; //To fix the F-d up facing.

    private NavMeshAgent monster;

    private Vector3 startPos;
    private Transform monsterTr;

    private bool attacking;
    private bool moving;
    private bool returning;

    public Animator anim;

    // Use this for initialization
    void Start () {

        //Initialize monster parameters.
        moveInterval = 3;
        chaseTime = 8;
        chaseVelocity = 7.5f;
        chaseProximity = 8;
        //chaseLimitDist = 10;
        teleportDisableTime = 4;

        //Get Mandala information.
        mandala =  GameObject.Find("Mandala");
        if (mandala != null) {
            tc = mandala.GetComponent<TeleportationController>();
        }
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
        if (!returning && !attacking && (Vector3.Distance(mandala.transform.position, monsterTr.position) < chaseProximity)) {
            attacking = true;
            moving = false;
        }

        //Attack cycle.
        if (attacking && !returning && mandala.activeSelf) {
            //Chase
            monsterTr.LookAt(mandala.transform);
            monsterTr.Translate(chaseVelocity * Vector3.forward * Time.deltaTime);

            StartCoroutine(setAttackLimit());
        }

        if (returning && !attacking) {
            //monsterTr.position = Vector3.MoveTowards(monsterTr.position, startPos, chaseVelocity * Time.deltaTime);
            monster.destination = startPos;
        }
        
        //Monster is back to its origin spot.
        if (returning && Vector3.Distance(monsterTr.position, startPos) <= 1)
            returning = false;
    }


    //Attack cycle.
    void OnTriggerEnter(Collider obj)   {
        // If object is the mandala and  player is not currently teleporting:
        if ((obj.gameObject.name == "Mandala") && tc.nState==0) {

            anim.SetTrigger("bite"); // Bite animation.

            //Disable teleportation and hide mandala.
            tc.canTeleport = false;
            mandala.SetActive(false);
            //mandala.GetComponent<Renderer>().enabled = false;

            //Notify possible killer.
            if (nearbyKiller != null) {
                nearbyKiller.attacking = true;
            }

            StartCoroutine(reenableTeleportation());
        }
    }


    IEnumerator reenableTeleportation()   {
        yield return new WaitForSeconds(teleportDisableTime); //time until teleportation is reactivated.
        tc.canTeleport = true;
        mandala.SetActive(true);
        //mandala.GetComponent<Renderer>().enabled = true;
    }


    /* Monster's movement cycle. Based on a random pattern of movement. */
    IEnumerator movementCycle() {
        moving = true;

        NavMeshHit hit;
        // Set a random point as destination.
        Vector3 radomPos = (Random.insideUnitSphere * 3) + startPos;
        NavMesh.SamplePosition(radomPos, out hit, 20, 1);
        
        monsterTr.rotation = Quaternion.LookRotation(hit.position.normalized);
        monster.destination = hit.position;


        yield return new WaitForSeconds(moveInterval); // wait before allowing cycle to repeat.
        moving = false;        
    }


    /* Takes monster back to starting point after some time. */
    IEnumerator setAttackLimit() {
        //Stop chasing after a time limit.
        yield return new WaitForSeconds(chaseTime);
        attacking = false;

        //Send monster back to start point.
        returning = true;
    }

}