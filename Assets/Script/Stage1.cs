using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Stage1 : MonoBehaviour
{
    // input values
    public float speed = 0.15f;

    // reference
    public Transform head = null;
    public Transform mesh = null;
    public XRController controller = null;

    // Componenets
    private Animator animator = null;
    private CharacterController character = null;

    // Values
    private Vector3 currentDirection = Vector3.zero;

    // �÷��̾� �ֺ� �ν� ������ ����
    GameObject nearObject;
    GameObject equipObject;
    private bool isPicked = false;
    public GameObject[] pickys; // �ֿ� �� �ִ� ���ǵ�
    public bool[] hasPickys; // �÷��̾ �ֿ� ��������

    // water animation
    private bool isWatered = false; // �� �Ѹ� ���� �ʱⰪ false
    public ParticleSystem ps;

    public virtual void OnInteract()
    {

    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.enableInputActions)
        {
            CheckForMovement(controller.inputDevice);
            //CheckForWave(controller.inputDevice);
            Pick(controller.inputDevice);
            Push(controller.inputDevice);
            //Interaction();
        }
    }

    private void CheckForMovement(InputDevice device) // joystick direction
    {
        // Look for input, and potential value
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 joystickDirection))
        {
            // Sets character direction, also factoring head
            CalculateDirection(joystickDirection);

            // Apply character direction, and speed v
            MoveCharacter();

            // Orient the character mesh seperately
            OrientMesh();

            // Animate blend tree
            Animate();
        }
    }


    private void CalculateDirection(Vector2 joystickDirection)
    {
        // Joystick direction
        Vector3 newDirection = new Vector3(joystickDirection.x, 0, joystickDirection.y);

        // Look rotate
        Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);

        // Rotate our joystick direction using the rotation of the head
        currentDirection = Quaternion.Euler(headRotation) * newDirection;
    }

    private void MoveCharacter() // head rotation
    {
        Vector3 movement = currentDirection * speed;

        character.SimpleMove(movement);
    }

    private void OrientMesh()
    {
        if (currentDirection != Vector3.zero)
            mesh.transform.forward = currentDirection;
    }

    private void Animate()
    {
        float blend = currentDirection.magnitude;
        animator.SetFloat("Move", blend);
    }


    // �̼� ���� �ν�
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tool")
            nearObject = other.gameObject;
        Debug.Log(nearObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tool")
            nearObject = null;
    }
    private void Pick(InputDevice device) // B button
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool primary))
        {
            if (isPicked != primary)
            {
                
                isPicked = primary; // button on trigger
                if (isPicked)
                {
                    Item item = nearObject.GetComponent<Item>();
                    int toolIndex = item.value;
                    hasPickys[toolIndex] = true;
                    animator.SetTrigger("Pick");
                }
                else
                {
                    
                    animator.ResetTrigger("Pick");
                    Destroy(nearObject);
                    
                }
            }
        }
    }

    // Tools are picked
    private void Push(InputDevice device) // A button
    {
        // A Button
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primary))
        {
            if (isWatered != primary)
            {
                isWatered = primary; // button on trigger
                if (isWatered)
                {
                    
                    equipObject = pickys[0];
                    equipObject.SetActive(true);
                    animator.SetTrigger("PourWater");
                    ps.Play();
                }
                else
                {
                    animator.ResetTrigger("PourWater");
                    ps.Stop();
                }
            }
        }
    }
}

