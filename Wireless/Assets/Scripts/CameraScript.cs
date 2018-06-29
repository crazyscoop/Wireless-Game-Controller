using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour

{
	
	private Rigidbody myRigidbody;

    private Vector3 offset;

    private Quaternion rot;

    [SerializeField]
    private Transform cameraAround;

    [SerializeField]
    private float lag;

    void Start()
    {
        //offset = transform.position - cameraAround.transform.position;
    }

    void Update()
    {

        rot = transform.rotation;

    }

    void LateUpdate()
    {
        //transform.position = player.transform.position + offset;
        transform.position = cameraAround.position;
        ///transform.rotation = ;
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraAround.rotation, lag * Time.deltaTime);

    }






}
