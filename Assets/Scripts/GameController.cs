using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GameController : MonoBehaviour
{
    // Game states
    public enum GameState
    {
        LOADING,
        TUTORIAL,
        PLAYING,
        RESULTS,
        ADVIDEO,
        FINISHED
    }

    // Current game state
    public GameState currentState;

    [Header("Loading screen UI elements")]
    public GameObject loadingScreen;
    public Transform charLoadingPlace;
    public TMP_Text loadingTitle;
    private GameObject charSelected;

    [Header("Losing/Winning screen UI elements")]
    public GameObject losingScreen;
    public Button buyLifeButton;
    public Button watchVideoButton;
    public TMP_Text starsText;
    public GameObject winningScreen;
    public Transform charWinningPlace;

    [Header("Playing screen UI elements")]
    public Transform charPlayingPlace;
    public Transform cardSpawnPoint;
    public GameObject cardPrefab;
    public GameObject lives;
    
    [Header("List of questions")]
    public List<string> questions;
    public List<SwipeDirection> trueOrFalse;
    public int count = 0;

    private GameObject life1;
    private GameObject life2;
    private GameObject life3;
    private bool _lose = false;

    
    // Method to transition to tutorial state and deactivate loading screen
    public void StartTutorial()
    {
        currentState = GameState.PLAYING;
        loadingScreen.SetActive(false);
    }
    
    // Start function to set the initial game state
    private void Start()
    {
        currentState = GameState.LOADING;
        charSelected = GameManager.Instance.selectedCharacter;
        Instantiate(charSelected, charLoadingPlace);
        Instantiate(charSelected, charWinningPlace);
        Instantiate(charSelected, charPlayingPlace);
        loadingTitle.text = GameManager.Instance.SelectedStage;
        // Show loading screen and disable tutorial button
        loadingScreen.SetActive(true);
        Debug.Log(questions.Count);
        life1 = lives.transform.GetChild(0).gameObject;
        life2 = lives.transform.GetChild(1).gameObject;
        life3 = lives.transform.GetChild(2).gameObject;
    }

    // Update function to handle state transitions and game logic
    private void Update()
    {
        switch (currentState)
        {
            case GameState.LOADING:
                int i = 0;
                // Check if there are more questions and we haven't spawned 10 cards yet
                foreach (var t in questions)
                {
                    // Spawn a new card and set the question text
                    GameObject cardObject = Instantiate(cardPrefab, cardSpawnPoint);
                    SwipeEffect swipeEffect = cardObject.GetComponent<SwipeEffect>();
                    TMP_Text questionText = cardObject.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
                    questionText.text = t;
                    swipeEffect.correctSwipe = trueOrFalse[i];
                    i++;
                    swipeEffect.onSwipe += OnCardSwiped;
                }
                currentState = GameState.TUTORIAL;

                break;
            case GameState.TUTORIAL:
                // TODO: Implement tutorial logic
                currentState = GameState.PLAYING;
                break;
            case GameState.PLAYING:
                switch (GameManager.Instance.lives)
                {
                    case 3:
                        life1.SetActive(true);
                        life2.SetActive(true);
                        life3.SetActive(true);
                        break;
                    case 2:
                        life1.SetActive(false);
                        life2.SetActive(true);
                        life3.SetActive(true);
                        break;
                    case 1:
                        life1.SetActive(false);
                        life2.SetActive(false);
                        life3.SetActive(true);
                        break;
                    case 0:
                        life1.SetActive(false);
                        life2.SetActive(false);
                        life3.SetActive(false);
                        if (!_lose)
                        {
                            _lose = true;
                            StartCoroutine(Failed());
                        }
                        break;
                }
                if (cardSpawnPoint.childCount == 0)
                {
                    // No more questions, transition to results state if all cards are swiped
                    currentState = GameState.RESULTS;
                }
                break;
            case GameState.RESULTS:
                if (_lose)
                {
                    
                }
                else
                {
                    currentState = GameState.FINISHED;
                    Win();
                }
                
                break;
            case GameState.ADVIDEO:
                // TODO: Implement ad video logic
                break;
            case GameState.FINISHED:
                // TODO: Implement finished logic
                break;
        }
        
    }

    private void Win()
    {
        Debug.Log("you have won!");
        winningScreen.SetActive(true);
        GameManager.Instance.selectedCharacter.GetComponent<CharacterView>().changeExp(5);
    }
    
    private IEnumerator Failed()
    {
        yield return new WaitForSeconds(1f);
        losingScreen.SetActive(true);
        starsText.text = GameManager.Instance.Gems.ToString();
        if(GameManager.Instance.Gems > 1) buyLifeButton.interactable = true;
        if (cardSpawnPoint.childCount == 0)
        {
            buyLifeButton.interactable = false;
            watchVideoButton.interactable = false;
        }
        currentState = GameState.RESULTS;
    }

    public void PaidForLives()
    {
        if (GameManager.Instance.Gems > 1)
        {
            GameManager.Instance.changeGems(-1);
            GameManager.Instance.addLives(1);
            _lose = false;
            losingScreen.SetActive(false);
            currentState = GameState.PLAYING;
        }
    }

    public void GiveUp()
    {
        GameManager.Instance.addLives(3);
        GameManager.Instance.SelectedStage = "MainMenu";
        GameManager.Instance.LoadStageSelected();
    }

    public void WatchVideo()
    {
        
    }
    
    private void OnCardSwiped(SwipeDirection swipeDirection)
    {
        
    }
}

