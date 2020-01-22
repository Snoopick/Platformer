using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public GameObject coinUI;
    public float movementDistance = 2f;
    private Coroutine moveUpCoroutine;
    private Coroutine moveDownCoroutine;
    public bool isFallDownCoin;
    float distance = 0f;
    private float chestTransform;
    public float coinToGUISpeed = 0.4f;
    
    private Vector3 lenTocoinGUI;

    private void Start()
    {
        coinUI = GameObject.Find("CoinImage").gameObject;
        Debug.Log(coinUI);
        
        if (isFallDownCoin)
        {
            chestTransform = transform.position.y;
            if (moveUpCoroutine == null)
            {
                StartCoroutine(MoveUp());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (moveUpCoroutine == null && other.GetComponent<Player>())
        {
            StartCoroutine(MoveUp());
        }
    }

    private IEnumerator MoveUp()
    {
        animator.SetTrigger("Rotate");

        while (distance < movementDistance)
        {
            var shift = movementDistance * Time.deltaTime;
            distance += shift;
            transform.Translate(Vector3.up * shift);

            yield return null;
        }

        if (!isFallDownCoin)
        {
//            while (Vector3.Distance(GetVector(transform.position), GetVector(lenTocoinGUI)) > 0.1)
//            {
//                
//                Debug.Log(Vector3.Distance(GetVector(transform.position), GetVector(lenTocoinGUI)));
//                
//                transform.Translate(GetVector(coinUI.transform.position) * coinToGUISpeed * Time.deltaTime);
//                yield return null;
//            }
            
            GameObject CoinCountLabel = GameObject.Find("CoinCount");
            string words = CoinCountLabel.GetComponent<Text>().text;
            int coinCount = Int32.Parse(words);
            coinCount++;
            CoinCountLabel.GetComponent<Text>().text = coinCount.ToString();
            
            Destroy(gameObject);
        }
        else
        {
            if (moveDownCoroutine == null)
            {
                StartCoroutine(FallDown());
            }
        }
    }

    private IEnumerator FallDown()
    {
        isFallDownCoin = false;

        while (distance > 0)
        {
            var shift = chestTransform * Time.deltaTime * 1f;
            distance += shift;
            transform.Translate(Vector3.up * shift);

            yield return null;
        }
    }
    
    private void Update()
    {
        if (moveUpCoroutine == null)
        {
            return;
        }
        
        lenTocoinGUI = GetVector(coinUI.transform.position) - GetVector(transform.position);
    }
    
    private Vector3 GetVector(Vector3 position)
    {
        return new Vector3(position.x, position.y, 0f);
    }
}
