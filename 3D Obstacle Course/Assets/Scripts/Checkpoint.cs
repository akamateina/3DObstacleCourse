using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject[] checkpoint;
    [SerializeField] AudioSource hitSFX;
    public Vector3 spawnPoint;
    bool dead = false;

    private void Start()
    {
        spawnPoint = gameObject.transform.position;
    }

    private void Update()
    {
        if (gameObject.transform.position.y < -5f)
        {
            gameObject.transform.position = spawnPoint;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body"))
        {
            hitSFX.Play();
            gameObject.transform.position = spawnPoint;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            spawnPoint = other.gameObject.transform.position;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Rival"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<PlayerMovement>().enabled = false;
            Die();
        }
    }

    void Die()
    {
        Invoke(nameof(ReloadLevel), 1.3f);
        dead = true;
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
