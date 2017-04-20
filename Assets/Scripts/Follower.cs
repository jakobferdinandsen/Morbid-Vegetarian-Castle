using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Follower : MonoBehaviour
{
    public float speed;

    private Rigidbody2D playerObject;
    private GameObject[] nonTraversables;
    private GameObject mapBoundary;

    // Use this for initialization
    void Start()
    {
        playerObject = GetComponent<Rigidbody2D>();
        nonTraversables = GameObject.FindGameObjectsWithTag("nontraversable");
        mapBoundary = GameObject.FindGameObjectWithTag("mapboundary");

        foreach (GameObject nonTraversable in nonTraversables)
        {
            Transform pos = nonTraversable.GetComponent<Transform>();
            Boundary boundary = new Boundary(pos);
            Debug.Log(boundary.left);

        }
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
    }

    public class Boundary
    {
        public float left { get; set; }
        public float right { get; set; }
        public float top { get; set; }
        public float bottom { get; set; }

        public Boundary(Transform transform)
        {
            left = transform.position.x-(transform.lossyScale.x/2);
            right = transform.position.x+(transform.lossyScale.x/2);
            top = transform.position.y+(transform.lossyScale.y/2);
            bottom = transform.position.y-(transform.lossyScale.y/2);
        }


        public override string ToString()
        {
            return "left: " + left+", right: "+right;
        }
    }


}
