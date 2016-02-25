using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MandalaKillerMonster : MonoBehaviour
{

    public GameObject playerObj;
    public Transform player;

    private NavMeshAgent monster;

    private Vector3 startPos;
    private Transform monsterTr;

    public bool attacking;
    private bool moving;
    private bool returning;

    // Use this for initialization
    void Start()     {
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

        //Begins attack if player is close enough or mandala monster has eaten the teleporter.
        if (!returning && (Vector3.Distance(player.position, monsterTr.position) < 5))
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
        if (obj.gameObject.name == "Player") {
            //Reset level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
