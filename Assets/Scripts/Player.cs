using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public AudioClip firearm;
    AudioSource asource;
    float hp = 100;
    Image imgHP;
    public GameObject text1, button1;
    Zombie myZombie;
    
    // Start is called before the first frame update
    void Start()
    {
        asource = GetComponent<AudioSource>();
        imgHP =  GameObject.Find("HP").GetComponent<Image>();
 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            asource.PlayOneShot(firearm);
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))       {
                if (hit.collider.gameObject.CompareTag("Zombie")){
                    myZombie = hit.collider.gameObject.GetComponent<Zombie>();
                    myZombie.zombieHP -= 25;
                    if (myZombie.zombieHP <= 0)
                    {
                        NavMeshAgent agent = myZombie.GetComponent<NavMeshAgent>();
                        Animator anim = myZombie.GetComponent<Animator>();
                        agent.speed = 0;
                        anim.SetFloat("hp", 0);
                    }
                }
            }
        }
        imgHP.fillAmount = hp / 100;
        if (hp <= 0)
        {
            text1.SetActive(true);
            button1.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            hp -= 20;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            hp -= 0.5f;
        }
    }
}
