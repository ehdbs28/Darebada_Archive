using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PolyFishMoving : MonoBehaviour
    {
        public Transform target;
        Transform fallowTarget;
        //[SerializeField] Transform fallowTarget;

        public Vector2 fishSpeedRange = new Vector2(1, 2);
        float fishSpeed;
        public float rotateSpeed = 1;

        //Hit
        public bool isHit;
        public float hitDist = 1;
        public float hitRange = 1;


        void Start()
        {
            fishSpeed = Random.Range(fishSpeedRange.x, fishSpeedRange.y);
            //Get random target
            if (target != null && target.childCount > 0)
            {
                Transform[] childTrs = new Transform[target.childCount];
                for (int i = 0; i < childTrs.Length; i++)
                {
                    childTrs[i] = target.GetChild(i).transform;
                }
                int randomChild = Random.Range(0, childTrs.Length);
                fallowTarget = childTrs[randomChild].transform;
            }
            else
            {
                fallowTarget = target;
            }
        }

        void Update()
        {
            //fish move
            if (fallowTarget)
            {
                FollowTarget(fallowTarget);
            }

            //Hit check
            if (target)
            {
                RaycastHit hit;

                // Cast a sphere wrapping character controller 10 meters forward
                // to see if it is about to hit anything.
                if (Physics.SphereCast(transform.position, hitRange, transform.forward, out hit, hitDist))
                {
                    if (hit.transform.GetComponent<PolyFishMoving>() && hit.transform.GetComponent<PolyFishMoving>().fallowTarget != null)
                    {
                        PolyFishMoving fm = hit.transform.GetComponent<PolyFishMoving>();
                        Transform fmTr = fm.fallowTarget.transform;

                        if (fmTr == fallowTarget)
                        {
                            //print("hit's target == my target -> ChangeTarget()");
                            ChangeTarget(); //Change target
                        }
                    }
                    isHit = true;   //OnDrawGizmosSelected
                }
                else
                {
                    isHit = false;  //OnDrawGizmosSelected
                }
            }
        }

        public void FollowTarget(Transform _target)
        {
            Vector3 dir = _target.position - transform.position;
            transform.Translate(dir.normalized * fishSpeed * Time.deltaTime, Space.World);

            var targetRotation = Quaternion.LookRotation(_target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        public void ChangeTarget()
        {
            int newNum = Random.Range(0, target.childCount);

            int cur = -1;
            if (fallowTarget != null)
            {
                for (int i = 0; i < target.childCount; i++)
                {
                    if (target.GetChild(i) == fallowTarget)
                    {
                        cur = i;
                    }
                }
            }

            if (newNum == cur)
            {
                newNum = Random.Range(0, target.childCount);
                fallowTarget = target.GetChild(newNum);

                if (newNum == cur)
                {
                    //print("<color=red>unbelievable!!</color>");
                }
            }
            else
            {
                fallowTarget = target.GetChild(newNum);
                //print("<color=blue>targetChild has changed!</color>");
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;

            if (isHit)
            {
                Gizmos.color = Color.red;
            }

            Debug.DrawLine(transform.position, transform.position + transform.forward * hitDist);
            Gizmos.DrawWireSphere(transform.position + transform.forward * hitDist, hitRange);
        }
    }

