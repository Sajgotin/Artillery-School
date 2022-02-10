using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject barrel;
    [SerializeField] GameObject turret;
    [SerializeField] GameObject shellPrefab;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameManager gameManager;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationAngleMax = 50f;
    [SerializeField] float barrelRotationMax;
    [SerializeField] float barrelRotationMin;    
    [SerializeField] TextMeshProUGUI angleText;
    [SerializeField] TextMeshProUGUI powerText;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI explosionRadiusText;
    [SerializeField] PlayableDirector timeline;  
    [SerializeField] ParticleSystem shotParticle;
    [SerializeField] float fireRate = 1.5f;
    [SerializeField] float time = 0f;
    [SerializeField] AudioSource shotSound;
    public AudioSource explosionSource;
    public ParticleSystem explosionParticle;
    private ShellController shellController;
    public float firepower = 100f;
    public int points;
    public int radius = 10;

    // Start is called before the first frame update
    void Start()
    {
        radius = 10;
        points = 0;
        time = 0;
        UpdatePoints();
        explosionRadiusText.SetText("Explosion Radius: " + radius + "m");
        shellController = shellPrefab.GetComponent<ShellController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            RotateArtillery();
            FireArtillery();           
        }   
    }

    void FireArtillery()
    {
        time += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && time >= fireRate)
        {
            Instantiate(shellPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            timeline.Play();
            Instantiate(shotParticle, spawnPoint.transform.position, spawnPoint.transform.rotation);
            time = 0;
            shotSound.Play();
        }
    }
    void RotateArtillery()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        CheckRotationY();
        CheckRotationZ();
        SetFirepower();

        turret.transform.Rotate(Vector3.up * rotationSpeed * horizontalInput * Time.deltaTime);
        barrel.transform.Rotate(Vector3.forward * -rotationSpeed * verticalInput * Time.deltaTime);
    }

    void CheckRotationY()
    {
        if (turret.transform.eulerAngles.y > 180)
        {
            float angle = turret.transform.localEulerAngles.y - 360;
            if (angle < -rotationAngleMax)
            {
                turret.transform.rotation = Quaternion.Euler(0, -rotationAngleMax, 0);             
            }
        }
        else if (turret.transform.eulerAngles.y > rotationAngleMax)
        {
            turret.transform.rotation = Quaternion.Euler(0, rotationAngleMax, 0);
        }
    }

    void CheckRotationZ()
    {
        if (barrel.transform.eulerAngles.z > 359)
        {
            barrel.transform.rotation = Quaternion.Euler(barrel.transform.rotation.eulerAngles.x, barrel.transform.rotation.eulerAngles.y, 359);
        }
        if (barrel.transform.eulerAngles.z < 320)
        {
            barrel.transform.rotation = Quaternion.Euler(barrel.transform.rotation.eulerAngles.x, barrel.transform.rotation.eulerAngles.y, 320);
        }
        angleText.SetText("Angle: " + Mathf.Abs(Mathf.Round(barrel.transform.eulerAngles.z) - 374));
    }

    void SetFirepower()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            firepower -= 50 * Time.deltaTime;
            if(firepower < 200)
            {
                firepower = 100f;
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            firepower += 50 * Time.deltaTime;
            if(firepower > 600)
            {
                firepower = 600f;
            }
        }

        powerText.SetText("Power: " + Mathf.Round(firepower));
    }

    public void UpdatePoints()
    {
        pointsText.SetText("Points: " + points);
    }

    public void UpgradeExplosionRadius()
    {
        if(points >= 100)
        {
            points -= 100;
            UpdatePoints();
            radius += 1;
            explosionRadiusText.SetText("Explosion Radius: " + radius + "m");
        }
    }
    public void UpdateTimer()
    {
        gameManager.timer += 10;
    }
}
