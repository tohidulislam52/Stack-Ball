using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{ 
    [SerializeField] private GameObject[] Models;
     private GameObject[] ModelPrefabs = new GameObject[4];
    [SerializeField] private GameObject WinPrefabs;
    [SerializeField] private float speed;

    private int lavel,addon=7;
    private GameObject temp1,temp2;
    private float ii =0;
    [SerializeField] private Material basmat,polmat,playermat;
    [SerializeField] private MeshRenderer playermash;
    [SerializeField] private Material[] skybox;
    void Awake()
    {
        basmat.color = Random.ColorHSV(0,1,.5f,1,1,1);
        polmat.color = basmat.color+Color.gray;
        playermash.material.color = basmat.color;
        // Camera.main.backgroundColor = Random.ColorHSV(0,1,.5f,1,1,1);
        RenderSettings.skybox = skybox[Random.Range(0,11)];
                   

        
        // PlayerPrefs.DeleteAll();
        lavel = PlayerPrefs.GetInt("Lavel",1);
        float random = Random.value;
       
        ModelSelection();
        if(lavel>9)
         addon =0;
        for (ii = 0; ii > -lavel -addon; ii-=.5f)
        {
            if(lavel <= 20)
                temp1 = Instantiate(ModelPrefabs[Random.Range(0,2)],transform);
            if(lavel > 20 && lavel<=50)
                temp1 = Instantiate(ModelPrefabs[Random.Range(0,3)],transform);
            if(lavel >50 && lavel<=100)
                temp1 = Instantiate(ModelPrefabs[Random.Range(1,4)],transform);
            if(lavel>100)
                temp1 = Instantiate(ModelPrefabs[Random.Range(1,4)],transform);

            temp1.transform.position = new Vector3(transform.position.x,ii-.01f ,transform.position.z);
            temp1.transform.eulerAngles = new Vector3(transform.eulerAngles.x,ii*8,transform.eulerAngles.z);
            if(Mathf.Abs(ii) >= lavel *.3f && Mathf.Abs(ii) <= lavel * .6f)
            {
                temp1.transform.eulerAngles = new Vector3(transform.eulerAngles.x,ii*8,transform.eulerAngles.z);
                temp1.transform.eulerAngles += Vector3.up*180;
            }
            else if(Mathf.Abs(ii) >= lavel *.8)
            {
                temp1.transform.eulerAngles = new Vector3(transform.eulerAngles.x,ii*8,transform.eulerAngles.z);
                if(random >=.6)
                {
                temp1.transform.eulerAngles += Vector3.up*180;
                }
            }
        }
        temp2 = Instantiate(WinPrefabs,transform);
        temp2.transform.position = new Vector3(transform.position.x,ii-0.01f,transform.position.z);
        Debug.Log("lavel " + lavel);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,speed*Time.fixedDeltaTime,0));
    }
    public void ChangeColor()
    {
        if(Input.GetMouseButtonDown(1))
        {
            basmat.color = Random.ColorHSV(0,1,.5f,1,1,1);
            polmat.color = basmat.color+Color.gray;
            playermash.material.color = basmat.color;
            // playermat.color = basmat.color;
        }
        basmat.color = Random.ColorHSV(0,1,.5f,1,1,1);
        polmat.color = basmat.color+Color.gray;
        playermash.material.color = basmat.color;
    }
    private void ModelSelection()
    {
        int RandomeModel = Random.Range(0, 5);
        switch(RandomeModel)
        {
            case 0:
            for (int i = 0; i < 4; i++)
            {
                ModelPrefabs[i] = Models[i];
            }
            break;
            case 1:
            for (int i = 0; i < 4; i++)
            {
                ModelPrefabs[i] = Models[i+4];
            }
            break;
            case 2:
            for (int i = 0; i < 4; i++)
            {
                ModelPrefabs[i] = Models[i+8];
            }
            break;
            case 3:
            for (int i = 0; i < 4; i++)
            {
                ModelPrefabs[i] = Models[i + 12];
            }
            break;
            case 4:
            for (int i = 0; i < 4; i++)
            {
                ModelPrefabs[i] = Models[i+16];
            }
            break;
            
        }
    }

    public void NextLavel()
    {
        PlayerPrefs.SetInt("Lavel",PlayerPrefs.GetInt("Lavel") +1);
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
     SceneManager.LoadScene(0);

    }
}
