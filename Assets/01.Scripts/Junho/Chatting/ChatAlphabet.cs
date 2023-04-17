using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChatAlphabet : MonoBehaviour
{
    public static int count = 0;

    [SerializeField] private int _weight;

    public ChatAlphabet(int weight = 1, ChatAlphabet alphabet = null)
    {
        Instantiate(alphabet)._weight = weight; // 이 부분 바꿔야됨  => Pool형태로 바꿀 예정
    }

    private void Start()
    {
        
    }

    
}
