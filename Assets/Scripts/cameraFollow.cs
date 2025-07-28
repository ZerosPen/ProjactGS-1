using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public float speedFollow = 2f;
    public Transform target;

    void Update()
    {
        Vector3 newPost = new Vector3(target.position.x, target.position.y, -10f);
        newPost.x = Mathf.Lerp(transform.position.x, target.position.x, Time.deltaTime * speedFollow);
        newPost.y = Mathf.Lerp(transform.position.y, target.position.y, Time.deltaTime * speedFollow);
        newPost.z = transform.position.z;
        transform.position = newPost;
    }
}
