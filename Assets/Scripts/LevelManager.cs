using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform cube, EndPoint;
    Transform startPoint;
    [SerializeField] Transform LevelsHolder;
    [SerializeField] GameObject AnimObj, DisplayDialogue,MenuPanel,GamePanel;
    [SerializeField] float animTime = 1.5f;
    [SerializeField] Image progressBar;
    float currentLevel, TotalDistance;
    public AudioSource MoveSound,finishedSound;
    [SerializeField]TextMeshProUGUI level;
    private void Awake()
    {
        instance = this;
    }
    IEnumerator Start()
    {
        yield return null;
        if (Levelinstance.inst.BeganGame)
        {
            cube.gameObject.SetActive(true);
            SetLevel();
            GamePanel.SetActive(true);
            MenuPanel.SetActive(false);
            level.text = Levelinstance.inst.currentLevel.ToString();
        }else
        {
            cube.gameObject.SetActive(false);
            MenuPanel.SetActive(true);
            GamePanel.SetActive(false);
            foreach (Transform child in LevelsHolder)
            {
                child.gameObject.SetActive(false);
            }
        }
        if (startPoint != null && EndPoint != null)
        {
            TotalDistance = Mathf.Abs(EndPoint.position.x-startPoint.position.x);
        }
    }

    public void FinishedLevel()
    {
        Levelinstance.inst.currentLevel += 1;
        StartCoroutine("SwitchLevel");
    }
    void Update()
    {
        if (startPoint != null && EndPoint != null)
        {
            progressBar.fillAmount = Mathf.Abs(startPoint.position.x-cube.position.x) / TotalDistance;
        }
    }
    IEnumerator SwitchLevel()
    {
        AnimObj.SetActive(true);
        yield return new WaitForSeconds(animTime / 2);
        RestartLevel();
        yield return new WaitForSeconds(animTime);
        AnimObj.SetActive(true);
        StopAllCoroutines();
    }
    void SetLevel()
    {
        if (Levelinstance.inst.currentLevel < 4)
        {
            foreach (Transform child in LevelsHolder)
            {
                child.gameObject.SetActive(false);
            }
            var lvl = LevelsHolder.GetChild(Levelinstance.inst.currentLevel - 1).gameObject;
            lvl.SetActive(true);
            foreach (Transform ch in lvl.transform)
            {
                if (ch.name == "start")
                {
                    startPoint = ch;
                    cube.position = ch.localPosition;
                }
            }
        }
        else
        {
            cube.gameObject.SetActive(false);
            DisplayDialogue.SetActive(true);
        }

    }

    public void ReturnToMenu(){
        Levelinstance.inst.BeganGame = false;
        Levelinstance.inst.currentLevel = 0;
    }
    public void BeginGame(){
        Levelinstance.inst.BeganGame = true;
        Levelinstance.inst.currentLevel = 1;
        RestartLevel();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
