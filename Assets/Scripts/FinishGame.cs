using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FinishGame : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerController.instance.IsAvailableToMove = false;
            collision.transform.DOMoveY(-7f, 1f).OnComplete(() =>
            {
                FindObjectOfType<GameManager>().GoToNextScene();
            });
        }
    }
}
