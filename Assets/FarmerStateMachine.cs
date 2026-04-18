using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerStateMachine : MonoBehaviour
{
    public enum State {Idle, Movement, Hostile, Flee, Dead}
    public State CurrentState;
    public float StateTime;
    bool InRange = false;
    public NavMeshAgent NavMeshAgent;
    private void Awake()
    {
        CurrentState = State.Idle;
        StateTime = 0f;
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Movement:
                Movement();
                break;
            case State.Hostile:
                Hostile();
                break;
            case State.Dead:
                Dead();
                break;
        }
    }

    void Idle()
    {
        StateTime += Time.deltaTime;
        if (StateTime > 5)
        {
            CurrentState = State.Movement;
            StateTime = 0;
        }
    }
    void Movement()
    {
        StateTime += Time.deltaTime;
        NavMeshAgent.destination = transform.position + transform.right;
        if (StateTime > 5)
        {
            CurrentState = State.Idle;
            StateTime = 0;
        }
        if (InRange)
        {
            CurrentState = State.Hostile;
        }
    }
    void Hostile()
    {
        NavMeshAgent.destination = FindAnyObjectByType<Controller>().transform.position;
        if (!InRange)
        {
            CurrentState = State.Idle;
            StateTime = 0;
        }
    }
    void Flee()
    {

    }
    void Dead()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            InRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            InRange = false;
        }
    }
}
