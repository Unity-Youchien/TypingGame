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

    // 問題の何文字目か
    private int _aNum;

    // ゲームを始めた時に1度だけ呼ばれるもの
    void Start()
    {
        // 問題を出す
        OutPut();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_aString[_aNum].ToString()))
        {
            // 正解
            Correct();

            // 最後の文字に正解したら
            if (_aNum >= _aString.Length)
            {
                // 問題を変える
                OutPut();
            }
        }
        else if (Input.anyKeyDown)
        {
            // 失敗
            Miss();
        }
    }

    // 問題を出すための関数
    void OutPut()
    {
        // 0番目の文字に戻す
        _aNum = 0;

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

    // 正解用の関数
    void Correct()
    {
        // 正解した時の処理（やりたいこと）
        _aNum++;
        Debug.Log("正解したよ！");
    }

    // 間違え用の関数
    void Miss()
    {
        // 間違えた時にやりたいこと
        Debug.Log("間違えたよ！");
    }
}