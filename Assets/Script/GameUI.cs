using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private GameObject HomeUI;
    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private GameObject finishUI;
    [SerializeField] private GameObject AllButton;
    [SerializeField] private Text Scortext,levelNum;
    [SerializeField] private Text bestScoretext;
    private Player player;
    private bool Button;
    [SerializeField] private Button SoundButton;
    
    [SerializeField] private Sprite soundon,soundoff;
    

    [Header("In game")]
    [SerializeField] private Slider slider;
    [SerializeField] private Image fildImage;
    [SerializeField] private Image CurrentLevelText;
    [SerializeField] private Image NextLevelText;
    [SerializeField] private Text currentLevelNum;
    [SerializeField] private Text NextLevelNum;

    [SerializeField] private Material Playermaterial;

    private float scorrr;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        SoundButton.onClick.AddListener(() => SoundManager.instance.SoundOnOff());
        if(ScoreManager.instanec.score >1 )
        {
            HomeUI.SetActive(false);
            player.playerState = Player.PlayerState.playing;
        }

    }
    void Start()
    {
        currentLevelNum.text = PlayerPrefs.GetInt("Lavel").ToString();
        float level = PlayerPrefs.GetInt("Lavel") +1;
        NextLevelNum.text = level.ToString();
        sliderCOlor();

    }
    private void sliderCOlor()
    {
        Playermaterial = FindObjectOfType<Player>().transform.GetChild(0)
                .GetComponent<MeshRenderer>().material;
        fildImage.color = Playermaterial.color+ Color.gray ;
        CurrentLevelText.color = Playermaterial.color + Color.gray ;
        NextLevelText.color = Playermaterial.color;
    }

    void Update()
    {
        scorrr += ScoreManager.instanec.score;
        sliderCOlor();
        if(player.playerState == Player.PlayerState.Peparing)
        {
            if(SoundManager.instance.sound && SoundButton.GetComponent<Image>() != soundon )
                SoundButton.GetComponent<Image>().sprite = soundon;

            if(!SoundManager.instance.sound && SoundButton.GetComponent<Image>() != soundoff )
                SoundButton.GetComponent<Image>().sprite = soundoff;
        }
        if(slider.value == 1)
           NextLevelText.color = Playermaterial.color+ Color.gray;
        
        if(Input.GetMouseButtonUp(0) && !IgnorUi() && player.playerState == Player.PlayerState.Peparing)
        {
            player.playerState = Player.PlayerState.playing;
            HomeUI.SetActive(false);
            // InGameUI.SetActive(true);
        }
    }
    private bool IgnorUi()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultsList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);
        for (int i = 0; i < raycastResultsList.Count; i++)
        {
            if(raycastResultsList[i].gameObject.GetComponent<IgnorUI>() != null)
            {
                raycastResultsList.RemoveAt(i);
                i--;
            }
        }
        Debug.Log(raycastResultsList.Count);
        return raycastResultsList.Count > 0;
    }



    public void LevelSliderFil(float fill)
    {
        slider.value = fill;
    }
    public void Setting()
    {
        Button = !Button;
        AllButton.SetActive(Button);
    }
    public void GameoverUI()
    {
        gameoverUI.SetActive(true);
        Scortext.text = ScoreManager.instanec.score.ToString();
        bestScoretext.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void FinishUI()
    {
        finishUI.SetActive(true);
        levelNum.text = PlayerPrefs.GetInt("Lavel").ToString();
        // bestScoretext.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
