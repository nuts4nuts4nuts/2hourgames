using UnityEngine;

public class FollowTheCharacters : MonoBehaviour
{
    [SerializeField] private float camPositionLerpSpeed = 2f;
    [SerializeField] private float camSizeLerpSpeed = 0.25f;

    public static FollowTheCharacters Instance;
    private GameObject[] players;
    private Camera cam;
    public float minimumDistance = 3f;
    private bool _isGameOver;

    private void Start()
    {
        Instance = this;
        players = GameObject.FindGameObjectsWithTag("Player");
        cam = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        float maxPlayerDistance = 0f;
        Vector3 averagePosition = Vector3.zero;
        foreach (var player in players)
        {
            averagePosition += player.transform.position;

            foreach (var other in players)
            {
                float distance = Vector3.Distance(player.transform.position, other.transform.position);
                if (distance > maxPlayerDistance)
                {
                    maxPlayerDistance = distance;
                }
            }
        }

        averagePosition /= players.Length;

        Vector3 targetPosition = new Vector3(averagePosition.x, averagePosition.y, cam.transform.position.z);
        Vector3 position = Vector3.Lerp(cam.transform.position, targetPosition, camPositionLerpSpeed * Time.deltaTime);
        cam.transform.position = position;

        float targetCamSize = Mathf.Max(maxPlayerDistance, minimumDistance);

        if (_isGameOver)
        {
            targetCamSize *= 0.33f;
        }

        float camSize = Mathf.Lerp(cam.orthographicSize, targetCamSize, camSizeLerpSpeed * Time.deltaTime);

        cam.orthographicSize = camSize;
    }

    public void ZoomOut()
    {
        // real hack shit :)
        _isGameOver = true;
        players = FindObjectsOfType<GameObject>();
    }
}
