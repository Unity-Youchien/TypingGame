using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 画面にあるテキストの文字を変更したい
public class TypingManager : MonoBehaviour
{
    // 画面にあるテキストを持ってくる
    [SerializeField] Text fText; // ふりがな用のテキスト
    [SerializeField] Text qText; // 問題用のテキスト
    [SerializeField] Text aText; // 答え用のテキスト

    // 問題を用意しておく
    private string _furigana = "たいぴんぐ";
    private string _question = "タイピング";
    private string _answer = "taipingu";

    // ゲームを始めた時に1度だけ呼ばれるもの
    void Start()
    {
        // 文字を変更する
        fText.text = _furigana;
        qText.text = _question;
        aText.text = _answer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
