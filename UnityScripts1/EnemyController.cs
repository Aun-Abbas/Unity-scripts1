using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float lookRadius = 3f;    
    [SerializeField] EnemyShooting enemyShooting;
    [SerializeField] Breakable breakable;

    Transform target;
    NavMeshAgent agent;
    bool shoot;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Target").transform;        
        breakable.stopShootingBullets += stopShooting;
        target.GetComponent<Target>().stopShootingAtTarget += stopShooting;
        shoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position,transform.position);
        if (distance <= lookRadius) {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance) {
                faceTarget();
                if (shoot) {
                    enemyShooting.shoot();
                }                                
            }            
        }
    }

    public void stopShooting() {
        shoot = false;
    } 

    void faceTarget() {

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
