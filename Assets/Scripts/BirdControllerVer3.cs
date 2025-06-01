using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BirdControllerVer3 : MonoBehaviour
{
    [SerializeField] private float flapForce = 7f;
    [SerializeField] private float floorY = 1.1f;
    [SerializeField] private float shootHorizontalOffset = 0.2f;
    [SerializeField] private float startingSpeed = 1f;
    [SerializeField] private float accelerationAmplitude = 1f;
    [SerializeField] private float meters = 0;
    [SerializeField] private int health = 3;
    [SerializeField] private AudioClip pickupSound;
    private bool isDead = false;

    private Rigidbody2D rigidBody;
    private Animator animator;

    public bool canDie;
    private float minHeight;
    private float maxHeight;
    private float verticalPadding = 0.8f;
    private bool isFlying = false;
    public bool canMove = true;
    private int tappCount;

    [SerializeField] private TMP_Text seedCounter;
    [SerializeField] private TMP_Text healthUI;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private ParticleSystem seedParticle;
    [SerializeField] private ParticleSystem featherParticle;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CapsuleCollider2D capsuleCollider2;

    //Variabler f�r r�ststyrning
    [SerializeField] private bool voiceControlEnabled = true; //Hook to seetings menu

    [SerializeField] private float voiceSensitivity = 0.1f; //Calibrate based on mic
    [SerializeField] private int micSampleWindow = 128;

    private AudioClip micClip;
    private string micDevice;
    private bool micInitialized = false;
    //Slut

    private GameObject struggleEnemy;
    private Animator struggleEnemyAnimator;

    public bool CBSelected = false;
    private Animator CBAnimator;

    [SerializeField] private float struggleTimer = 5f;
    private float struggleTimeCounter;

    private AudioSource audioSource;

    [SerializeField] private GameObject backgroundDetector;
    [SerializeField] private GameObject backgroundSpawner;
    [SerializeField] private GameObject backgroundController;
    [SerializeField] private GameObject house;
    [SerializeField] private GameObject charaterSpawner;
    [SerializeField] private PhoneCamera phoneScript;

    private void Start()
    {
        struggleTimeCounter = 0f;
        DataSaver.instance.metersTraveled = 0;
        DataSaver.instance.seedAmount = 0;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigidBody.gravityScale = 1f;

        tappCount = 0;
        struggleEnemy = null;
        struggleEnemy = null;

        animator = GetComponent<Animator>();
        CBAnimator = transform.Find("CustomBird").GetComponentInChildren<Animator>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        UpdateScreenBounds();

        //Hitta mikrofon

        if (DataSaver.instance.voicecontrol && Microphone.devices.Length > 0)
        {
            micDevice = Microphone.devices[0];
            micClip = Microphone.Start(micDevice, true, 1, 44100);
            micInitialized = true;
        }
        //Slut

        CameraBackground();
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        healthUI.text = health.ToString();

        isFlying = false;

        //Ser till att f�geln flyger n�r man skriker
        if (voiceControlEnabled && micInitialized && canMove)
        {
            float volume = GetMicVolume();
            if (volume > voiceSensitivity)
            {
                isFlying = true;
            }
        }
        //Slut

        if (Input.touchCount > 0 && canMove)
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
        if ((Input.touchCount > 0 && tappCount <= 4) && !canMove)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width / 2)
                {
                    tappCount++;
                    //Debug.Log(tappCount);
                }
            }
        }

        if (tappCount == 5)
        {
            StartCoroutine(StopStruggling());
        }

        if (isStruggling)
        {
            if (struggleTimeCounter <= 5f)
            {
                struggleTimeCounter += Time.deltaTime;
                Debug.Log(struggleTimeCounter += Time.deltaTime);
            }
            if (struggleTimeCounter >= 5f)
            {
                Debug.Log("L");
                OnHit();

                if (health <= 0 && canDie && !isDead)
                {
                    Die();
                    SaveSeeds();
                }

                EndStruggle();
                struggleTimeCounter = 0f;
                StartCoroutine(StopStruggling());
            }
        }

        if (!isStruggling)
        {
            capsuleCollider2.enabled = true;
        }

        //Debug.Log(struggleEnemy);

#if UNITY_EDITOR
        if ((Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width / 2) && canMove)
        {
            isFlying = true;
        }
#endif
    }

    private void FixedUpdate()
    {
        UpdateScreenBounds();

        if (isDead)
        {
            return;
        }

        if (isFlying && transform.position.y < maxHeight)
        {
            //rigidBody.AddForce(Vector2.up * flapForce * 5f, ForceMode2D.Force);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            //rigidBody.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, flapForce);
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
        if (!isStruggling)
        {
            float desiredX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * shootHorizontalOffset, 0f, GetCameraZDistance())).x;
            transform.position = new Vector3(desiredX, transform.position.y, transform.position.z);
        }

        // Set speed
        DataSaver.instance.metersTraveled += DataSaver.instance.mps * Time.deltaTime;

        DataSaver.instance.mps = startingSpeed + accelerationAmplitude * DataSaver.instance.metersTraveled / 100;

        meters = DataSaver.instance.metersTraveled;
    }

    private bool isHit = false;

    public void OnHit()
    {
        if (!isHit)
        {
            isHit = false;
            Debug.Log("Hit triggered");

            animator.SetTrigger("isHit");

            Handheld.Vibrate();

            //add effects here later
            rigidBody.velocity = Vector2.zero;
            rigidBody.gravityScale = 1;
            //musssiiiiccc

            health--;

            if (CBSelected)
            {
                //CBAnimator.SetBool(damageTaken);
            }
        }
    }

    //Anpassa ljudstyrka beroende p� hur h�gt man skriker
    private float GetMicVolume()
    {
        if (!micInitialized || micClip == null) return 0f;

        float[] samples = new float[micSampleWindow];
        int micPos = Microphone.GetPosition(micDevice) - micSampleWindow;
        if (micPos < 0) return 0f;

        micClip.GetData(samples, micPos);

        float sum = 0f;
        foreach (float sample in samples)
        {
            sum += sample * sample;
        }

        return Mathf.Sqrt(sum / micSampleWindow); //RMS value
    }

    private void SaveSeeds()
    {
        DataSaver.instance.totalSeedAmount += DataSaver.instance.seedAmount;
        if (DataSaver.instance.seedAmount > DataSaver.instance.highScore) DataSaver.instance.highScore = DataSaver.instance.seedAmount;
        DataSaver.instance.totalMetersTraveled += DataSaver.instance.metersTraveled;
    }

    private bool isStruggling = false;

    public void OnStruggle()
    {
        if (isStruggling)
        {
            animator.SetBool("struggle", true);
            canMove = false;
            Time.timeScale = 0.8f;
            rigidBody.velocity = Vector2.zero;
            rigidBody.gravityScale = 0f;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            Debug.Log("Struggletime!");
        }
        else
        {
            Debug.Log("Struggle END!");
        }
    }

    public void EndStruggle()
    {
        isStruggling = false;
        animator.SetBool("struggle", false);
        capsuleCollider2.enabled = true;
        Time.timeScale = 1.0f;
        tappCount = 0;
        canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(featherParticle, collision.gameObject.transform.position, Quaternion.identity);
            //Invoke(DestroyObject(featherParticle), 1.5f);
            OnHit();
            Debug.Log("Aj");
            //health--;
            collision.collider.enabled = false;

            CameraShake cameraShake = mainCamera.GetComponent<CameraShake>();
            StartCoroutine(cameraShake.Shake());

            if (health <= 0 && canDie && !isDead)
            {
                Die();
                SaveSeeds();
            }
            //Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Instantiate(featherParticle, collision.gameObject.transform.position, Quaternion.identity);
            //Invoke(DestroyObject(featherParticle), 1.5f);
            OnHit();
            Debug.Log("Aj");
            //health--;
            collision.collider.enabled = false;

            CameraShake cameraShake = mainCamera.GetComponent<CameraShake>();
            StartCoroutine(cameraShake.Shake());

            if (health <= 0 && canDie && !isDead)
            {
                Die();
                SaveSeeds();
            }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Pickup"))
        {
            Instantiate(seedParticle, collision.gameObject.transform.position, Quaternion.identity);
            //Destroy(seedParticle, seedParticle.GetComponent<ParticleSystem>().main.duration);
            //DestroyImmediate(seedParticle, true);

            Debug.Log("Mmm");
            Destroy(collision.gameObject);
            DataSaver.instance.seedAmount++;
            seedCounter.text = DataSaver.instance.seedAmount.ToString();

            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }
        }

        if (collision.gameObject.CompareTag("StruggleEnemy"))
        {
            Instantiate(featherParticle, collision.gameObject.transform.position, Quaternion.identity);
            //DestroyImmediate(featherParticle, true);
            //Destroy(seedParticle, seedParticle.GetComponent<ParticleSystem>().main.duration);
            isStruggling = true;
            OnStruggle();
            StartCoroutine(Collider(false));

            struggleEnemy = collision.gameObject;
            struggleEnemyAnimator = collision.gameObject.GetComponent<Animator>();

            //collision.collider.enabled = false;

            /*if (canDie)
            {
                SaveSeeds();

                SceneManager.LoadScene(0);
            }*/
        }
    }

    private void Die()
    {
        isDead = true;
        GameManager.Instance.isGameOver = true;
        Debug.Log("Game Over set to TRUE");

        rigidBody.velocity = Vector2.zero;
        rigidBody.gravityScale = 1f;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        animator.SetTrigger("Die");

        FindObjectOfType<AdaptiveMusicPlayer>()?.StopAllMusic();


        //Time.timeScale = 0f;

        Invoke("ShowGameOverUI", 1f);
    }

    private void ShowGameOverUI()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
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

    private IEnumerator Collider(bool set)
    {
        yield return new WaitForSeconds(0.5f);
        capsuleCollider2.enabled = set;
    }

    private IEnumerator StopStruggling()
    {
        yield return new WaitForSeconds(0.5f);
        EndStruggle();
        struggleEnemyAnimator.SetTrigger("destroyNet");
        Destroy(struggleEnemy, 1f);
    }

    private void CameraBackground(){
        if (DataSaver.instance.useCameraBackground && phoneScript.Activate())
        {
            charaterSpawner.SetActive(false);
            backgroundDetector.SetActive(false);
            backgroundSpawner.SetActive(false);
            backgroundController.SetActive(false);
            house.SetActive(false);
        }
    }
}