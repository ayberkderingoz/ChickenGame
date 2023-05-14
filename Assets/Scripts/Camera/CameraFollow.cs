using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform player;

    // Update is called once per frame
    void Update () {
        transform.position = player.transform.position + new Vector3(0, 30, -20);
        //add slight rotation to the camera
        transform.rotation = Quaternion.Euler(0, 30, 0);
    }
}