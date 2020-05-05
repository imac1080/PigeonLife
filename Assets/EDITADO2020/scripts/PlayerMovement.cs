using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //movement speed in units per second
    private float movementSpeed = 5f;
    public GameObject paloma;
    private Touch touch;
    private float speedModifier = 0.01f;

    void OnTriggerEnter(Collider collidedObject)
    {
        if (collidedObject.tag == "car")
        {
           //Debug.Log("MUERTE " + collidedObject.tag);

            //collidedObject.SendMessage("hitByPlayerBullet", null, SendMessageOptions.DontRequireReceiver);
            Destroy(paloma);
            WaypointPatrol.muerto = true;
            GameStates.resetLvl = true;
        }

    }
    void Update()
    {
        if(paloma.transform.position.y < -1)
        {
            Destroy(paloma);
            WaypointPatrol.muerto = true;
            GameStates.resetLvl = true;
        }
        if (!GameStates.gameActive) return;
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        //update the position
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, 0, verticalInput * movementSpeed * Time.deltaTime);

        //touch movil
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                                                    transform.position.x + touch.deltaPosition.x * speedModifier,
                                                    transform.position.y,
                                                    transform.position.z + touch.deltaPosition.y * speedModifier);
            }
        }
        
        //output to log the position change
        //Debug.Log(transform.position);
    }
}