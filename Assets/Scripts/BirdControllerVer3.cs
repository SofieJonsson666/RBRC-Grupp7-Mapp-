using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BirdControllerVer4 : MonoBehaviour
{
    [SerializeField] private float flapForce = 5f;
    [SerializeField] private float floorY = 1.1f;
    [SerializeField] private float shootHorizontalOffset = 0.1f;
    [SerializeField] private int health = 3;

    private Rigidbody2D rigidBody;
    public bool canDie;
    private float minHeight;
    private float maxHeight;
    private float verticalPadding = 0.8f;
    private bool isFlying = false;

    private Gyroscope gyro;
    private float rotationZ;

    [SerializeField] private TMP_Text seedCounter;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigidBody.gravityScale = 1f;

        gyro = Input.gyro;
        gyro.enabled = true;

        UpdateScreenBounds();
    }

    private void Update()
    {
        if (DataSaver.instance.gyro)
        {
            Quaternion deviceRotation = gyro.attitude;

            Quaternion correctedRotation = new Quaternion(
                deviceRotation.x,
                deviceRotation.y,
                -deviceRotation.z,
                -deviceRotation.w);

            Vector3 eulerRotation = correctedRotation.eulerAngles;
            //print(eulerRotation.z);
            rotationZ = eulerRotation.z;
            return;
        }

        isFlying = false;

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if ((touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    && touch.position.x < Screen.width / 2)
                {
                    isFlying = true;
                }
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width / 2)
        {
            isFlying = true;
        }
#endif
    }

    private void FixedUpdate()
    {
        UpdateScreenBounds();

        if (DataSaver.instance.gyro)
        {
            print(Mathf.Clamp(((rotationZ - 90) * 0.02f), -1.5f, 1.5f));
            rigidBody.gravityScale = Mathf.Clamp(((rotationZ - 90) * 0.02f), -2f, 2f);
        }

        if (isFlying && transform.position.y < maxHeight)
        {
            rigidBody.AddForce(Vector2.up * flapForce * 5f, ForceMode2D.Force);
        }

        // Floor clamp
        if (transform.position.y <= floorY + 0.01f && rigidBody.velocity.y < 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            transform.position = new Vector3(transform.position.x, floorY + 0.01f, transform.position.z);
        }

        // Ceiling clamp
        if (transform.position.y >= maxHeight && rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        }

        // Lock to screen left offset
        float desiredX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * shootHorizontalOffset, 0f, GetCameraZDistance())).x;
        transform.position = new Vector3(desiredX, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Aj");
            health--;
            collision.collider.enabled = false;
            // Removed auto-destroy — teammate version prefers to keep enemy object alive
            if (health <= 0 && canDie)
            {
                SceneManager.LoadScene(0);
            }
        }

        if (collision.gameObject.CompareTag("Pickup"))
        {
            Debug.Log("Mmm");
            Destroy(collision.gameObject);
            DataSaver.instance.seedAmount++;
            seedCounter.text = DataSaver.instance.seedAmount.ToString();
        }

        if (collision.gameObject.CompareTag("StruggleEnemy"))
        {
            Debug.Log("Struggletime!");
            health--;
            collision.collider.enabled = false;

            if (health <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void UpdateScreenBounds()
    {
        float camZ = GetCameraZDistance();
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, camZ));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, camZ));
        minHeight = bottomLeft.y + verticalPadding;
        maxHeight = topRight.y - verticalPadding;
    }

    private float GetCameraZDistance()
    {
        return Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
    }
}
