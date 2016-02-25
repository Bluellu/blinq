using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KillerMonster : MonoBehaviour
{

    public Transform player;

    private NavMeshAgent monster;

    private Vector3 startPos;
    private Transform monsterTr;

    private bool attacking;
    private bool moving;
    private bool returning;

    // Use this for initialization
    void Start()
    {
        monster = GetComponent<NavMeshAgent>();
        monsterTr = GetComponent<Transform>();
        startPos = monsterTr.position;

        attacking = false;
        moving = false;
        returning = false;
    }


    // Update is called once per frame
    void Update()
    {
        //Maintain movement cycle.
        if (!moving && !attacking && !returning)
        {
            StartCoroutine(movementCycle());
        }

        //Begins attack if player is close enough.
        if (!returning && (Vector3.Distance(player.position, monsterTr.position) < 8))
            attacking = true;


        //Attack cycle.
        if (attacking)
        {
            //Chase
            monsterTr.LookAt(player);
            monsterTr.Translate(5 * Vector3.forward * Time.deltaTime);

            StartCoroutine(setAttackLimit());
        }


        //Monster is back to its origin spot.
        if (Vector3.Distance(monsterTr.position, startPos) <= 1)
            returning = false;
    }


    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.name == "Player")
        {
            //Reset level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }



    /* Monster's movement cycle. Based on a random pattern of movement. */
    IEnumerator movementCycle()
    {
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
    IEnumerator setAttackLimit()
    {
        //Stop chasing after a time limit.
        yield return new WaitForSeconds(7);
        attacking = false;

        //Send monster back to start point.
        returning = true;
        monster.destination = startPos;
    }

}
