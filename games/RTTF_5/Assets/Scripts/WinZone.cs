using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    private List<Player> _players;

    [SerializeField] private TextMeshPro _text;
    [SerializeField] private float _winDistance = 10f;

    private void Start()
    {
        _players = new List<Player>(FindObjectsOfType<Player>());
    }

    private void Update()
    {
        if (_players.TrueForAll(p => Vector3.Distance(p.transform.position, transform.position) < _winDistance))
        {
            _text.enabled = true;

            FollowTheCharacters.Instance.ZoomOut();
            Invoke(nameof(RestartGame), 5f);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
