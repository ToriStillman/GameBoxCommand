using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Subtitle : MonoBehaviour
{
    [SerializeField] private Text dialog;
    [SerializeField] private string[] dialogueStart;

    private int num = 0;

    private void Awake()
    {
        TextLevel1();
    }

    public void Dialogue()
    {
        if (num < dialogueStart.Length + 1)
        {
            dialog.text = dialogueStart[num];
            num += 1;
        }
    }

    private void TextLevel1()
    {
        dialogueStart[0] = "Я никогда раньше не видела снега.";
        dialogueStart[1] = "Но все изменилось, когда Белые Ходоки напали на мой родной город.";
        dialogueStart[2] = "Все жители спрятались в ледяных сугробах, а я... я не буду прятаться.";
        dialogueStart[3] = "Есть легенда, что Санта сможет уничтожить Ходоков, а значит, мне нужно найти его.";
        dialogueStart[4] = "Потому что теперь я Принцесса Снежных долин.";
    }
    

}


