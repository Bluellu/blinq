using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/* Monster which chases and kills the player (restarting the current level). */
public class KillerMonster : MonoBehaviour   {
    private GameObject mandala;
    private TeleportationController tc;

    /* Movement parameters. */
    private float moveInterval;   //Seconds between each move.
    private float chaseTime;      //Number of seconds monster will chase for.
    private float chaseVelocity;  //How fast monster chases.
    private float chaseProximity; //How close player has to be to trigger chase.
    private float chaseLimitDist;     //Distance at which monster will quit chasing.

    private Transform player;

    private NavMeshAgent monster;

    private Vector3 startPos;
    private Transform monsterTr;

    private bool attacking;
    private bool moving;
    private bool returning;

    public Animator anim;

    // Use this for initialization
    void Start()  {
        moveInterval = 3;
        chaseTime = 8;
        chaseVelocity = 7.5f;
        chaseProximity = 12;
        //chaseLimitDist = 10;

        mandala = GameObject.Find("Mandala");
        if (mandala != null) {
            tc = mandala.GetComponent<TeleportationController>();
        }

        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        monster = GetComponent<NavMeshAgent>();
        monsterTr = GetComponent<Transform>();
        startPos = monsterTr.position;

        attacking = false;
        moving = false;
        returning = false;
    }


    // Update is called once per frame
    void Update()   {

        //Maintain movement cycle.
        if (!moving && !attacking && !returning)  {
            StartCoroutine(movementCycle());
        }

        //Begins attack if player is close enough.
        if (!returning && !attacking && (Vector3.Distance(player.position, monsterTr.position) < chaseProximity)) {
            attacking = true;
            moving = false;
        }

        //Attack cycle.
        if (attacking)   {
            //Chase
            monsterTr.LookAt(player);
            monsterTr.Translate(chaseVelocity * Vector3.forward * Time.deltaTime);

            StartCoroutine(setAttackLimit());
        }

        //Monster is back to its origin spot.
        if (returning && Vector3.Distance(monsterTr.position, startPos) <= 1) {
            Debug.Log("back to start");
            returning = false;
        }
    }


    //Detect player kill.
    void OnTriggerEnter(Collider obj)     {
        if ((obj.gameObject.tag == "Player") && (tc.nState == 0))   {
            anim.SetTrigger("bite"); // Bite animation.
            //Reset level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    /* Monster's movement cycle. Based on a random pattern of movement. */
    IEnumerator movementCycle()   {
        moving = true;

        NavMeshHit hit;
        // Set a random point as destination.
        Vector3 randomPos = (Random.insideUnitSphere * 3) + startPos;
        NavMesh.SamplePosition(randomPos, out hit, 10, 1);
        monster.destination = hit.position;

        yield return new WaitForSeconds(moveInterval); // wait before allowing cycle to repeat.
        moving = false;
    }


    /* Takes monster back to starting point after some time. */
    IEnumerator setAttackLimit()    {
        //Stop chasing after a time limit.
        yield return new WaitForSeconds(chaseTime);
        attacking = false;

        //Send monster back to start point.
        returning = true;
        monster.destination = startPos;
    }

}
