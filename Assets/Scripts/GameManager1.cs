using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int playerScore;
    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }

    public object Settings { get; private set; }

    [SerializeField]
    private GameObject[] ballPosition;

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private GameObject cueBall;

    [SerializeField]
    private float xInput = 0f;

    [SerializeField]
    private GameObject ballLine;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private TMP_Text notiText;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CameraBehindCueBAll();

        SetBall(BallColor.Red, 1);
        SetBall(BallColor.Yellow, 2);
        SetBall(BallColor.Green, 3);
        SetBall(BallColor.Brown, 4);
        SetBall(BallColor.Blue, 5);
        SetBall(BallColor.Pink, 6);
        SetBall(BallColor.Black, 7);

    }
    // Update is called once per frame
    void Update()
    {
        RotateBall();

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            ShootBall();

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            xInput = -0.05f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            xInput = 0.05f;
        else
            xInput = 0f;

        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
            StopBall();
        if (Keyboard.current.leftShiftKey.isPressed && Keyboard.current.sKey.wasPressedThisFrame)
            SaveGame();
    }

    private void SetBall(BallColor col, int i)
    {
        GameObject obj = Instantiate(ballPrefab,
                         ballPosition[i].transform.position,
                         Quaternion.identity);

        Ball b = obj.GetComponent<Ball>();
        b.SetColorAndPoint(col);
    }

    private void ShootBall()
    {
        Rigidbody rd = cueBall.GetComponent<Rigidbody>();
        rd.AddRelativeForce(Vector3.forward * 50, ForceMode.Impulse);

        ballLine.SetActive(false);
        cam.transform.parent = null;
        cam.transform.position = new Vector3(0f, 30f, -42f);
        cam.transform.eulerAngles = new Vector3(45f, 0f, 0f);

    }

    private void RotateBall()
    {
        if (cueBall != null)
            cueBall.transform.Rotate(new Vector3(0f, xInput, 0f));
    }
    private void StopBall()
    {
        Rigidbody rb = cueBall.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        cueBall.transform.eulerAngles = new Vector3(0f, 0f, 0f);

        ballLine.SetActive(true);
        CameraBehindCueBAll();
    }

    private void CameraBehindCueBAll()
    {
        cam.transform.parent = cueBall.transform;
        cam.transform.position = cueBall.transform.position + new Vector3(0f, 7f, -15f);
        cam.transform.eulerAngles = new Vector3(30f, 0f, 0f);
    }

    public void ShowScoreText(int n)
    {
        playerScore += n;
        notiText.text = $"Ball Point:{n}\nTotal Score:{playerScore}";
    }
    public void ShowString(string s)
    {
        notiText.text = s;
    }

    public void SaveGame()
    {
        StopBall();
        if (cueBall != null)
        {
            PlayerPrefs.SetFloat("cueBallPosX", cueBall.transform.position.x);
            PlayerPrefs.SetFloat("cueBallPosY", cueBall.transform.position.y);
            PlayerPrefs.SetFloat("cueBallPosZ", cueBall.transform.position.z);

            Debug.Log("Save");
        }
    }
    public void LoadGame()
    {

        if (cueBall != null)
        {
            float x = PlayerPrefs.GetFloat("cueBallPosX");
            float y = PlayerPrefs.GetFloat("cueBallPosY");
            float z = PlayerPrefs.GetFloat("cueBallPosZ");

            cueBall.transform.position = new Vector3(x, y, z);

            Debug.Log("Loaded");
        }



    }
}