using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fireBullet : MonoBehaviour
{
    public float timeEachBullets = 0.15f;
    public GameObject projectile;


    //Ammo info
    public Slider ammoSlider;
    public int maxRounds;
    public int startRounds;
    int remainRounds;


    float nextBullet;

    //Audio

    AudioSource gunMuzzleAS;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    // Start is called before the first frame update
    void Awake()
    {
        nextBullet = 0f;
        remainRounds = startRounds;
        ammoSlider.maxValue = maxRounds;
        ammoSlider.value = remainRounds;
        gunMuzzleAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerController myPlayer = transform.root.GetComponent<playerController>();
        if (Input.GetAxisRaw("Fire1") >0 && nextBullet < Time.time && remainRounds > 0)
        {
            nextBullet = Time.time + timeEachBullets;
            Vector3 rotation;
            if (myPlayer.GetFacing() == -1f)
            {
                rotation = new Vector3(0, -90, 0);
            }
            else rotation = new Vector3(0, 90, 0);

            Instantiate(projectile, transform.position, Quaternion.Euler(rotation));

            playASound(shootSound);


            remainRounds -= 1;
            ammoSlider.value = remainRounds;

        }
    }

    public void reload()
    {
        remainRounds = maxRounds;
        ammoSlider.value = remainRounds;
        playASound(reloadSound);
        Debug.Log(message: $"Player pickup {maxRounds} ammo", gameObject);

    }
    void playASound(AudioClip playTheSound)
    {
        gunMuzzleAS.clip = playTheSound;
        gunMuzzleAS.Play();
        
    }
}