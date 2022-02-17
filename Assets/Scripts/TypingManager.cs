using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

// 画面にあるテキストの文字を変更したい
public class TypingManager : MonoBehaviour
{
    // 画面にあるテキストを持ってくる
    [SerializeField] Text fText; // ふりがな用のテキスト
    [SerializeField] Text qText; // 問題用のテキスト
    [SerializeField] Text aText; // 答え用のテキスト

    // 問題を用意しておく
    /*
    private string[] _furigana = { "たいぴんぐ", "もんだい", "ようちえん" };
    private string[] _question = { "タイピング", "問題", "幼稚園" };
    private string[] _answer = { "taipingu", "mondai", "youchien" };*/

    // テキストデータを読み込む
    [SerializeField] TextAsset _furigana;
    [SerializeField] TextAsset _question;
    //[SerializeField] TextAsset _answer;

    // テキストデータを格納するためのリスト
    private List<string> _fList = new List<string>();
    private List<string> _qList = new List<string>();
    //private List<string> _aList = new List<string>();

    // 何番目か指定するためのstring
    private string _fString;
    private string _qString;
    private string _aString;

    // 何番目の問題か
    private int _qNum;

    // 問題の何文字目か
    private int _aNum;

    // 合ってるかどうかの判断
    bool isCorrect;

    private ChangeDictionary cd;

    // しんぶん→"si","n","bu","n"
    // しんぶん→"shi","n","bu","n"
    // {0,0,1,2,2,3}
    // {0,1,0,0,1,0}
    private List<string> _romSliceList = new List<string>();
    private List<int> _furiCountList = new List<int>();
    private List<int> _romNumList = new List<int>();

    // ゲームを始めた時に1度だけ呼ばれるもの
    void Start()
    {
        cd = GetComponent<ChangeDictionary>();

        // テキストデータをリストに入れる
        SetList();

        // 問題を出す
        OutPut();
    }

    // Update is called once per frame
    void Update()
    {
        // 入力された時に判断する
        if (Input.anyKeyDown)
        {
            isCorrect = false;
            int furiCount = _furiCountList[_aNum];

            // 完全に合ってたら正解！
            // し s  i
            if (Input.GetKeyDown(_aString[_aNum].ToString()))
            {
                // trueにする
                isCorrect = true;

                // 正解
                Correct();

                // 最後の文字に正解したら
                if (_aNum >= _aString.Length)
                {
                    // 問題を変える
                    OutPut();
                }
            }
            else if (Input.GetKeyDown("n") && furiCount > 0 && _romSliceList[furiCount - 1] == "n")
            {
                // nnにしたい
                _romSliceList[furiCount - 1] = "nn";
                //_aString = string.Join("", _romSliceList);
                _aString = string.Join("", GetRomSliceListWithoutSkip());

                ReCreatList(_romSliceList);

                // trueにする
                isCorrect = true;

                // 正解
                Correct();

                // 最後の文字に正解したら
                if (_aNum >= _aString.Length)
                {
                    // 問題を変える
                    OutPut();
                }
            }
            else
            {
                // し →si, ci, shi
                // 柔軟な入力があるかどうか
                // 「し」→ "si" , "shi"
                // 今どの ふりがな を打たないといけないのかを取得する
                string currentFuri = _fString[furiCount].ToString();

                if (furiCount < _fString.Length - 1)
                {
                    // 2文字を考慮した候補検索「しゃ」
                    string addNextMoji = _fString[furiCount].ToString() + _fString[furiCount + 1].ToString();
                    CheckIrregukarType(addNextMoji, furiCount, false);
                }

                if (!isCorrect)
                {
                    // 今まで通りの候補検索「し」「ゃ」
                    string moji = _fString[furiCount].ToString();
                    CheckIrregukarType(moji, furiCount, true);
                }
            }

            // 正解じゃなかったら
            if (!isCorrect)
            {
                // 失敗
                Miss();
            }
        }
    }

    void CheckIrregukarType(string currentFuri, int furiCount, bool addSmallMoji)
    {
        if (cd.dic.ContainsKey(currentFuri))
        {

            List<string> stringList = cd.dic[currentFuri]; // ci, shi
            Debug.Log(string.Join(",", stringList));

            // stringList[0] ci, stringList[1] shi
            for (int i = 0; i < stringList.Count; i++)
            {
                string rom = stringList[i];
                int romNum = _romNumList[_aNum];

                bool preCheck = true;

                for (int j = 0; j < romNum; j++)
                {
                    if (rom[j] != _romSliceList[furiCount][j])
                    {
                        preCheck = false;
                    }
                }

                if (preCheck && Input.GetKeyDown(rom[romNum].ToString()))
                {
                    _romSliceList[furiCount] = rom;
                    _aString = string.Join("", GetRomSliceListWithoutSkip());

                    ReCreatList(_romSliceList);

                    // trueにする
                    isCorrect = true;

                    if (addSmallMoji)
                    {
                        AddSmallMoji();
                    }

                    // 正解
                    Correct();

                    // 最後の文字に正解したら
                    if (_aNum >= _aString.Length)
                    {
                        // 問題を変える
                        OutPut();
                    }
                    break;
                }
            }
        }

    }

    void SetList()
    {
        string[] _fArray = _furigana.text.Split('\n');
        _fList.AddRange(_fArray);

        string[] _qArray = _question.text.Split('\n');
        _qList.AddRange(_qArray);

        //string[] _aArray = _answer.text.Split('\n');
        //_aList.AddRange(_aArray);
    }

    // 柔軟な入力をしたときに次の文字が小文字なら小文字を挿入する
    void AddSmallMoji()
    {
        int nextMojiNum = _furiCountList[_aNum] + 1;

        // もし次の文字がなければ処理をしない
        if (_fString.Length - 1 < nextMojiNum)
        {
            return;
        }

        string nextMoji = _fString[nextMojiNum].ToString();
        string a = cd.dic[nextMoji][0];

        // もしaの0番目がxでもlでもなければ処理をしない
        if (a[0] != 'x' && a[0] != 'l')
        {
            return;
        }

        // romSliceListに挿入と表示の反映
        _romSliceList.Insert(nextMojiNum, a);
        // SKIPを削除する
        _romSliceList.RemoveAt(nextMojiNum + 1);

        // 変更したリストを再度表示させる
        ReCreatList(_romSliceList);
        _aString = string.Join("", GetRomSliceListWithoutSkip());
    }

    // しんぶん→"shi","n","bu","n"
    // { 0, 0, 1, 2, 2, 3 }
    // { 0, 1, 0, 0, 1, 0 }
    void CreatRomSliceList(string moji)
    {
        _romSliceList.Clear();
        _furiCountList.Clear();
        _romNumList.Clear();

        // 「し」→「si」,「ん」→「n」
        for (int i = 0; i < moji.Length; i++)
        {
            string a = cd.dic[moji[i].ToString()][0];

            if (moji[i].ToString() == "ゃ" || moji[i].ToString() == "ゅ" || moji[i].ToString() == "ょ")
            {
                a = "SKIP";
            }
            else if (moji[i].ToString() == "っ" && i + 1 < moji.Length)
            {
                a = cd.dic[moji[i + 1].ToString()][0][0].ToString();
            }
            else if (i + 1 < moji.Length)
            {
                // 次の文字も含めて辞書から探す
                string addNextMoji = moji[i].ToString() + moji[i + 1].ToString();
                if (cd.dic.ContainsKey(addNextMoji))
                {
                    a = cd.dic[addNextMoji][0];
                }
            }

            _romSliceList.Add(a);

            if (a == "SKIP")
            {
                continue;
            }
            for (int j = 0; j < a.Length; j++)
            {
                _furiCountList.Add(i);
                _romNumList.Add(j);
            }
        }
        Debug.Log(string.Join(",", _romSliceList));
    }

    void ReCreatList(List<string> romList)
    {
        _furiCountList.Clear();
        _romNumList.Clear();

        // 「し」→「si」,「ん」→「n」
        for (int i = 0; i < romList.Count; i++)
        {
            string a = romList[i];
            if (a == "SKIP")
            {
                continue;
            }
            for (int j = 0; j < a.Length; j++)
            {
                _furiCountList.Add(i);
                _romNumList.Add(j);
            }
        }
        //Debug.Log(string.Join(",", _romSliceList));
    }

    // SKIPなしの表示をさせるためのListを作り直す
    List<string> GetRomSliceListWithoutSkip()
    {
        List<string> returnList = new List<string>();
        foreach(string rom in _romSliceList)
        {
            if (rom == "SKIP")
            {
                continue;
            }
            returnList.Add(rom);
        }
        return returnList;
    }

    // 問題を出すための関数
    void OutPut()
    {
        // 0番目の文字に戻す
        _aNum = 0;

        // _qNumに０〜問題数の数までのランダムな数字を1つ入れる
        _qNum = Random.Range(0, _qList.Count);

        _fString = _fList[_qNum];
        _qString = _qList[_qNum];

        CreatRomSliceList(_fString);

        _aString = string.Join("", GetRomSliceListWithoutSkip());

        // 文字を変更する
        fText.text = _fString;
        qText.text = _qString;
        aText.text = _aString;

        //Debug.Log(string.Join("", _romSliceList));
    }

    // 正解用の関数
    void Correct()
    {
        // 正解した時の処理（やりたいこと）
        _aNum++;
        aText.text = "<color=#6A6A6A>" + _aString.Substring(0, _aNum) + "</color>" + _aString.Substring(_aNum);
        //Debug.Log(_aNum);
    }

    // 間違え用の関数
    void Miss()
    {
        // 間違えた時にやりたいこと
        aText.text = "<color=#6A6A6A>" + _aString.Substring(0, _aNum) + "</color>"
            + "<color=#FF0000>" + _aString.Substring(_aNum, 1) + "</color>"
            + _aString.Substring(_aNum + 1);
    }
}