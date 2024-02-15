using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    private float currentTime;
    private bool Smash, invincible;
    private Rigidbody rb;
    private int currentStrak,TotalStrak;

    public GameObject invincibleOBj;
    public Image invincibleFill;
    public GameObject FireEfect,SplashEfect,WinEfect;
     private Transform win;
    private bool nocolor =true;
    public enum PlayerState 
    {
        Peparing,
        playing,
        died,
        finish
    }
    public PlayerState playerState = PlayerState.Peparing;
    [Header("Sound Clip")]
    public AudioClip bounceOffclip,deadclip,winclip,destroyclip,invDestroyclip;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentStrak = 0;
        FireEfect.SetActive(false);
    }


    void Start()
    {
        TotalStrak = FindObjectsOfType<StackControlar>().Length;
        FindObjectOfType<GameUI>().LevelSliderFil(currentStrak / (float) TotalStrak);
    }


    void Update()
    {
         if(playerState == PlayerState.playing)
        {
            if(Input.GetMouseButtonDown(0))
                Smash = true;
            if(Input.GetMouseButtonUp(0))
                Smash = false;   

            if(currentTime>= .3f || invincibleFill.color == Color.red)
                    invincibleOBj.SetActive(true);
            else 
                invincibleOBj.SetActive(false);
            if(currentTime>=1)
            {
                invincible = true;
                currentTime = 1;
                invincibleFill.color = Color.red;
            }  
            else if(currentTime <=0)
            {
                currentTime = 0;
                invincible = false;
                invincibleFill.color = Color.white;
            }      
            if(invincibleOBj.activeInHierarchy)
               invincibleFill.fillAmount = currentTime/1;
            // Debug.Log(invincible);
            // Debug.Log(currentTime);
        }
        
        if(win == null)
            win= GameObject.FindWithTag("Done").GetComponent<Transform>();
        
        if(transform.position.y < win.position.y /2 && nocolor)
        {
            nocolor = false;
            FindObjectOfType<LevelSpawner>().ChangeColor();
        }
        
        // if(Input.GetMouseButtonUp(0) && playerState == PlayerState.Peparing)
        //    playerState = PlayerState.playing;

        if(playerState == PlayerState.finish)
        {
            if(Input.GetMouseButtonDown(0))
               FindObjectOfType<LevelSpawner>().NextLavel();
        }

        if(Input.GetKeyDown(KeyCode.Space))
           Debug.Log(playerState);
    }


    void FixedUpdate()
    {

        if(playerState == PlayerState.playing)
        {
            // if(Input.GetMouseButtonDown(0))
            //     Smash = true;
            // if(Input.GetMouseButtonUp(0))
            //     Smash = false;   

            if(invincible)
            {
                currentTime -= Time.fixedDeltaTime*.6f;
                if(!FireEfect.activeInHierarchy)
                    FireEfect.SetActive(true);
            }
            else
            {
                if(FireEfect.activeInHierarchy)
                    FireEfect.SetActive(false);
                
                if(Smash)
                {
                    currentTime += Time.fixedDeltaTime* .8f;
                }
                else
                {
                    currentTime -= Time.fixedDeltaTime* .5f;
                }
            }
            
            
        }
        

        if(playerState == PlayerState.playing)
        {
            if(Input.GetMouseButton(0))
            {
                rb.velocity = new Vector3(0,-100*Time.fixedDeltaTime*5,0);
            }
        }
        if(rb.velocity.y > 5)
        {
            rb.velocity = new Vector3(rb.velocity.x,5,rb.velocity.y);
        } 
        
    }

    void OnCollisionStay(Collision other)
    {
        if(!Smash || other.gameObject.tag == "Finish" )
        {
            rb.velocity = new Vector3(0,50*Time.fixedDeltaTime*5,0);
            SoundManager.instance.PlaySoundFX(bounceOffclip,.5f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(!Smash)
        {
            rb.velocity = new Vector3(0,50*Time.fixedDeltaTime*5,0);
            if(other.gameObject.tag != "Finish")
            {
                GameObject splash = Instantiate(SplashEfect);
                splash.transform.SetParent(other.transform);
                splash.transform.localEulerAngles = new Vector3(90,Random.Range(0,360),0);
                float randomScale = Random.Range(.18f,.25f);
                splash.transform.localScale = new Vector3(randomScale,randomScale,1);
                splash.transform.position = new Vector3(transform.position.x,transform.position.y - .22f,
                                transform.position.z);
                splash.GetComponent<SpriteRenderer>().color = transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                Destroy(splash,5);
            }
        }
        else
        {
            if(invincible)
            {
                if(other.gameObject.tag == "enemy" || other.gameObject.tag == "plane")
                {
                    other.transform.parent.GetComponent<StackControlar>().ShatterAllParts();
                }
            }
            else
            {
                if(other.gameObject.tag == "enemy" && playerState != PlayerState.died)
                {
                    other.transform.parent.GetComponent<StackControlar>().ShatterAllParts();
                }
                if(other.gameObject.tag == "plane" && playerState != PlayerState.died)
                {
                    rb.isKinematic = true;
                    invincibleOBj.SetActive(false);
                    FindObjectOfType<GameUI>().GameoverUI();
                    Debug.Log("Game over");
                    SoundManager.instance.PlaySoundFX(deadclip,.5f);
                    playerState = PlayerState.died;
                    ScoreManager.instanec.RemoveScore();
                    // Destroy(gameObject);
                }
            }
        }
        FindObjectOfType<GameUI>().LevelSliderFil(currentStrak / (float) TotalStrak);

        if(other.gameObject.tag == "Finish" && playerState == PlayerState.playing)
        {
            invincibleOBj.SetActive(false);
            FindObjectOfType<GameUI>().FinishUI();
            playerState = PlayerState.finish;
            SoundManager.instance.PlaySoundFX(winclip,.8f);
            GameObject win = Instantiate(WinEfect);
            win.transform.SetParent(Camera.main.transform);
            win.transform.localPosition = new Vector3(0,0,3.5f);
            win.transform.eulerAngles = Vector3.zero;
        }
    }

    public void ADDPoint()
    {
        currentStrak++;
        if(!invincible)
        {
            ScoreManager.instanec.AddScore(1);
            SoundManager.instance.PlaySoundFX(destroyclip,.5f);
        }
        else
        {
            ScoreManager.instanec.AddScore(2);
            SoundManager.instance.PlaySoundFX(invDestroyclip,.5f);
        }
    }
}
