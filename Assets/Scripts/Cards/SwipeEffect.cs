using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeEffect : MonoBehaviour,IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Color color;
    public SwipeDirection correctSwipe;

    private Vector3 _initialPosition;
    private float leftOrRight = 0;
    private float _distanceMoved;
    private bool _swipeLeft;
    private TMP_Text questionText;
    private SwipeDirection _swipeDirection;
    private ParticleSystem badCardParticle;
    private Animator wrong;
    private Transform particlePlay;
    private ParticleSystem goodParticle;
    private ParticleSystem badParticle;
    
    public event Action<SwipeDirection> onSwipe;

    private void Start()
    {
        questionText = transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        badCardParticle = transform.GetChild(0).GetChild(2).GetComponent<ParticleSystem>();
        particlePlay = GameObject.FindGameObjectWithTag("ParticlePlay").transform;
        goodParticle = particlePlay.GetChild(0).GetComponent<ParticleSystem>();
        badParticle = particlePlay.GetChild(1).GetComponent<ParticleSystem>();
        wrong = GameObject.Find("Wrong").GetComponent<Animator>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition = new Vector2(transform.localPosition.x+eventData.delta.x,transform.localPosition.y);

        leftOrRight = transform.localPosition.x - _initialPosition.x;
        if (leftOrRight > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0,
                Mathf.LerpAngle(0, -30, (_initialPosition.x + transform.localPosition.x) / (Screen.width / 2)));
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0,
                Mathf.LerpAngle(0, 30, (_initialPosition.x - transform.localPosition.x) / (Screen.width / 2)));
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _initialPosition = transform.localPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _distanceMoved = Mathf.Abs(transform.localPosition.x - _initialPosition.x);
        if(_distanceMoved<0.4*Screen.width)
        {
            transform.localPosition = _initialPosition;
            transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            if (transform.localPosition.x > _initialPosition.x)
            {
                _swipeLeft = false;
                _swipeDirection = SwipeDirection.Right;

            }
            else
            {
                _swipeLeft = true;
                _swipeDirection = SwipeDirection.Left;
            }
            onSwipe?.Invoke(_swipeDirection);
            StartCoroutine(MovedCard());
        }

        if (_swipeDirection == correctSwipe)
        {
            Debug.Log("Respuesta correcta");
            goodParticle.Play();
            AudioManager.Instance.PlayGoodChoice();
        }
        else
        {
            badCardParticle.Play();
            wrong.SetTrigger("Wrong");
            GameManager.Instance.takeLives(1);
            AudioManager.Instance.PlayBadChoice();
            badParticle.Play();
            Debug.Log("Respuesta errónea");
        }
    }

    private IEnumerator MovedCard()
    {
        float time = 0;
        while (GetComponent<Image>().color != new Color(color.r, color.g, color.b, 0))
        {
            time += Time.deltaTime;
            if (_swipeLeft)
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x-Screen.width,time),transform.localPosition.y,0);
            }
            else
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x+Screen.width,time),transform.localPosition.y,0);
            }
            GetComponent<Image>().color = new Color(color.r,color.g,color.b,Mathf.SmoothStep(1,0,4*time));
            yield return null;
        }
        Destroy(gameObject);
    }

    public void ChangeCardText(string question)
    {
        questionText.text = question;
    }
}

public enum SwipeDirection
{
    Left,
    Right
}
