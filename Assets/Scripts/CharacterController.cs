using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class CharacterController : MonoBehaviour
{

    #region Standard Character Controller
    public float maxSpeed = 6f;
    public float turnSpeed = 90f;
    public Vector2 direction;

    public float currentSpeed;

    public float turnAcceleration = 5f;

    public float maxReverseSpeed = 3.8f;

    public float acceleration = 12f;
    public float stopSpeed = 9f;

    public List<float> averageFrameTime = new List<float>();
    public int averageStackLength;

    private float turnSpeedMultiplier;

    [SerializeField] private GameObject CurrentLandmark;

    private Vector3 groundNormal;
    public GameObject carObj;
    public GameObject rotationObj;

    public GameObject patio;

    private bool inOffice = false;

    //public Slider slider;

    public bool OVERWORLD = true;
    public FMODUnity.EventReference toot;
    public FMODUnity.EventReference engine;
    public float engineVolume;
    private FMOD.Studio.EventInstance engineInstance;
    public float useDelay = 2f; // to avoid instantly entering a place after going to the map
    private float currentUseDelay;
    #endregion

     
    
    #region Directional Character Controller
    [Space(100)]
    private Vector2 dirDirection;
    private Vector2 desiredDirection;
    private Vector2 speed;

    public float dirAccSpeed;
    public float dirMaxSpeed;
    public float dirRotSpeed;
    [SerializeField] private float currSpeed;

    private Rigidbody rb;

    private bool moving;
    #endregion

    public bool useDirectionalMovement;

    void Start()
    {
        if (OVERWORLD)
        {
            Debug.Log("About to check instance");
            bool check = GameManager.Instance.enabled;
            Debug.Log("Checked instance");
            GameManager.Flags[Flag.OFFICE_ARROW] = false;
            //GameManager.Flags[Flag.OSLO_ARROW] = true;
            engineInstance = GameManager.FMODPlayStatic(engine, transform.position, Vector3.zero, engineVolume, true, false);
            currentUseDelay = useDelay;
            Debug.Log("Setting position to: " + GameManager.GameData.overWorldPosition);
            transform.position = GameManager.GameData.overWorldPosition;
            
            //transform.position = new Vector3(80.4f, 86.46f, 294f);
            
            InvokeRepeating("SavePosition", 15f, 5f);
        }
    }

    private void SavePosition()
    {
        GameManager.GameData.overWorldPosition = transform.position;
        Debug.Log("AUTOSAVING");
        System.Threading.Thread save = new System.Threading.Thread(async () => await GameManager.GameData.SaveData());
        save.Start();
    }

    private void FixedUpdate()
    {
        //maxSpeed = slider.value;

        if (currentSpeed == 0 && currSpeed == 0)
        {
            patio.SetActive(true);
        }
        else
        {
            patio.SetActive(false);
        }
    }

    void Update()
    {
        if (!inOffice)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                GameManager.FMODPlayStatic(toot, transform.position, Vector3.zero);
            }
            if (!useDirectionalMovement)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    if (currentSpeed < maxSpeed)
                    {
                        currentSpeed += acceleration;
                    }
                    else
                    {
                        currentSpeed = maxSpeed;
                    }
                }
                if (Input.GetKey(KeyCode.S))
                {
                    if (currentSpeed > -maxReverseSpeed)
                    {
                        currentSpeed -= acceleration;
                    }
                    else
                    {
                        currentSpeed = -maxReverseSpeed;
                    }
                }
                if (Input.GetKey(KeyCode.A))
                {
                    if (turnSpeedMultiplier > 0f)
                        turnSpeedMultiplier -= turnAcceleration * 2f * Time.deltaTime;
                    else if (turnSpeedMultiplier >= -1f)
                        turnSpeedMultiplier -= turnAcceleration * Time.deltaTime;
                    else
                        turnSpeedMultiplier = -1f;

                    transform.Rotate(Vector3.up, turnSpeed * (currentSpeed * turnSpeedMultiplier) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    if (turnSpeedMultiplier < 0f)
                        turnSpeedMultiplier += turnAcceleration * 2f * Time.deltaTime;
                    else if (turnSpeedMultiplier <= 1f)
                        turnSpeedMultiplier += turnAcceleration * Time.deltaTime;
                    else
                        turnSpeedMultiplier = 1f;

                    transform.Rotate(Vector3.up, turnSpeed * (currentSpeed * turnSpeedMultiplier) * Time.deltaTime);
                }

                if (!Input.GetKey(KeyCode.W) && (!Input.GetKey(KeyCode.S)))
                {
                    if (currentSpeed > 0.1f)
                    {
                        currentSpeed -= stopSpeed * Time.deltaTime;
                    }
                    else if (currentSpeed < -0.1f)
                    {
                        currentSpeed += stopSpeed * Time.deltaTime;
                    }
                    else
                    {
                        currentSpeed = 0f;
                    }
                }
                if (!Input.GetKey(KeyCode.A) && (!Input.GetKey(KeyCode.D)))
                {
                    if (turnSpeedMultiplier > 0.1f)
                        turnSpeedMultiplier -= turnAcceleration * 0.5f * Time.deltaTime;
                    else if (turnSpeedMultiplier < -0.1f)
                        turnSpeedMultiplier += turnAcceleration * 0.5f * Time.deltaTime;
                    else
                        turnSpeedMultiplier = 0f;
                }
                Move(currentSpeed);
            }
            else
            {
                desiredDirection.x = 0;
                desiredDirection.y = 1;
                moving = false;


                if (Input.GetKey(KeyCode.D))
                {
                    desiredDirection.y = -1;
                    moving = true;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    desiredDirection.x = -1;
                    moving = true;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    desiredDirection.y = 1;
                    moving = true;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    desiredDirection.x = 1;
                    moving = true;
                }
                if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
                {
                    desiredDirection.y = 0;
                }
                if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
                {
                    desiredDirection.x = 0;
                }


                if (desiredDirection != Vector2.zero) transform.forward = new Vector3(direction.x, 0f, direction.y).normalized;

                DirectionalMove();
            }

            if (currentUseDelay <= 0)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interact();
                }
            }
            else currentUseDelay -= Time.deltaTime;

            /*
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Pause) || Input.GetKeyDown(KeyCode.Tab))
            {
                gameObject.GetComponent<PauseMenu>().Pause();
            }
            */

            if (Input.GetKeyDown(KeyCode.M))
            {
                if (OVERWORLD)
                {
                    WorldPoster wp = GetComponent<WorldPoster>();
                    wp.postUI.SetActive(true);
                    Time.timeScale = 1f;
                }
            }

            averageFrameTime.Add(Time.deltaTime);

            if (averageFrameTime.Count > averageStackLength) averageFrameTime.RemoveAt(0);


            if (OVERWORLD)
            {
                FMOD.ATTRIBUTES_3D attributes;
                engineInstance.get3DAttributes(out attributes);
                attributes.position = FMODUnity.RuntimeUtils.ToFMODVector(transform.position);
                engineInstance.set3DAttributes(attributes);
                if (currentSpeed > 0.1f || currentSpeed < -0.1f)
                {
                    engineInstance.setParameterByName("CarState", 1f);
                }
                else engineInstance.setParameterByName("CarState", 0f);
            }
        }
    }

    public void Unstuck()
    {
        transform.position = new Vector3(444.799988f, 13.2399998f, 758.5f);
    }

    public void ToggleOffice(bool i)
    {
        inOffice = i;
    }

    void Interact()
    {
        if (CurrentLandmark != null)
        {
            CurrentLandmark.GetComponent<Landmark>().Use();
        }
    }
    
    private void DirectionalMove()
    {
        if (moving)
        {
            currSpeed += dirAccSpeed * Time.deltaTime;
        }
        else
        {
            currSpeed -= dirAccSpeed * 2f * Time.deltaTime;
        }
        currSpeed = Mathf.Clamp(currSpeed, 0f, dirMaxSpeed);
        
        direction = Vector2.Lerp(direction, desiredDirection, dirRotSpeed * Time.deltaTime);

        if (Mathf.Abs(direction.x) > 0.2f || Mathf.Abs(direction.y) > 0.2f)
            direction.Normalize();

        transform.position += (new Vector3(direction.x, 0f, direction.y) * currSpeed * Time.deltaTime);
        
        
        RaycastHit hitInfo;
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + 30f, transform.position.z), Vector3.down);
        if (Physics.Raycast(ray, out hitInfo, 60f, 1 << 3))
        {
            transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);

            groundNormal = hitInfo.normal;
        }
        else
        {
            UnityEngine.Debug.LogWarning("Character Controller can't find ground.");
        }
        
        
        LerpToNormal();

    }

    void Move(float speed)
    {
        float averageFrame = averageFrameTime.Sum() / averageStackLength;

        //Debug.Log(averageFrame);

        Vector3 movement = transform.forward;
        movement.y = 0f;
        movement = movement * speed * averageFrame;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0f, 2.5f, 0f), movement.normalized, out hit, movement.magnitude))
        {
        }
        else transform.position += movement;

        RaycastHit hitInfo;
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + 30f, transform.position.z), Vector3.down);
        if (Physics.Raycast(ray, out hitInfo, 60f, 1 << 3))
        {
            transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);

            groundNormal = hitInfo.normal;
        }
        else
        {
            UnityEngine.Debug.LogWarning("Character Controller can't find ground.");
        }

        LerpToNormal();
    }

    private void LerpToNormal()
    {
        Quaternion tempRotation = Quaternion.FromToRotation(rotationObj.transform.up, groundNormal) * rotationObj.transform.rotation;

        rotationObj.transform.eulerAngles = new Vector3(tempRotation.eulerAngles.x, rotationObj.transform.eulerAngles.y, tempRotation.eulerAngles.z);

        carObj.transform.rotation = Quaternion.Slerp(carObj.transform.rotation, rotationObj.transform.rotation, 4f * Time.deltaTime);
    }

    public void SetLandmark(GameObject go)
    {
        CurrentLandmark = go;
    }
    public void RemoveLandmark()
    {
        CurrentLandmark = null;
    }
}