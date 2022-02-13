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
    private string[] _furigana = { "たいぴんぐ", "もんだい", "ようちえん" };
    private string[] _question = { "タイピング", "問題", "幼稚園" };
    private string[] _answer = { "taipingu", "mondai", "youchien" };

    // 何番目か指定するためのstring
    private string _fString;
    private string _qString;
    private string _aString;

    // 何番目の問題か
    private int _qNum;

    // ゲームを始めた時に1度だけ呼ばれるもの
    void Start()
    {
        // _qNumに０〜問題数の数までのランダムな数字を1つ入れる
        _qNum = Random.Range(0, _question.Length);

        _fString = _furigana[_qNum];
        _qString = _question[_qNum];
        _aString = _answer[_qNum];

        // 文字を変更する
        fText.text = _fString;
        qText.text = _qString;
        aText.text = _aString;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
