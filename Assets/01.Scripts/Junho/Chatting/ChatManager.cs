using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    [SerializeField] private List<ChatAlphabet> _alphabets;

    private TMP_InputField _chattingBar;

    private void Awake()
    {
        _chattingBar = FindObjectOfType<TMP_InputField>();
    }

    private void Update()
    {
        print(ChatAlphabet.count);
        
        if (Input.GetKeyDown(KeyCode.Return) && _chattingBar.text.Length == 1)
        {
            int inputValue = _chattingBar.text[0] - 'A' + 1;
            ChatAlphabet a = new ChatAlphabet(inputValue, _alphabets[inputValue - 1]);
        }
    }
}
