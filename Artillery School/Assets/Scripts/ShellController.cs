using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShellController : MonoBehaviour
{
    private Rigidbody shellRb;
    [SerializeField] SphereCollider explosionRadius;
    [SerializeField] PlayerController playerController;
    [SerializeField] Counter counter;

    // Start is called before the first frame update
    void Start()
    {
        explosionRadius.radius = .5f;
        shellRb = GetComponent<Rigidbody>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        counter = GameObject.Find("Player").GetComponent<Counter>();

        shellRb.AddRelativeForce(Vector3.up * playerController.firepower, ForceMode.Impulse);
    }

    private void Update()
    {
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerController.explosionSource.PlayOneShot(playerController.explosionSource.clip, 0.3f);
        explosionRadius.isTrigger = true;
        explosionRadius.radius = playerController.radius;
        Instantiate(playerController.explosionParticle, collision.GetContact(0).point, playerController.explosionParticle.transform.rotation);
        Invoke("DestroyObject",.05f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            counter.count++;
            playerController.UpdateTimer();
            playerController.points += 100;
            playerController.UpdatePoints();
            Destroy(other.gameObject);
        }
    }
}
