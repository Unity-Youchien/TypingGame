using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 入力用の辞書を作る
public class ChangeDictionary : MonoBehaviour
{
    // デフォルトの入力方法
    public Dictionary<string, string> dic = new Dictionary<string, string>() {

        {"あ", "a"},{"い", "i"},{"う", "u"},{"え", "e"},{"お", "o"},
        {"か", "ka"},{"き", "ki"},{"く", "ku"},{"け", "ke"},{"こ", "ko"},
        {"さ", "sa"},{"し", "si"},{"す", "su"},{"せ", "se"},{"そ", "so"},
        {"た", "ta"},{"ち", "ti"},{"つ", "tu"},{"て", "te"},{"と", "to"},
        {"な", "na"},{"に", "ni"},{"ぬ", "nu"},{"ね", "ne"},{"の", "no"},
        {"は", "ha"},{"ひ", "hi"},{"ふ", "hu"},{"へ", "he"},{"ほ", "ho"},
        {"ま", "ma"},{"み", "mi"},{"む", "mu"},{"め", "me"},{"も", "mo"},
        {"や", "ya"},{"ゆ", "yu"},{"よ", "yo"},
        {"ら", "ra"},{"り", "ri"},{"る", "ru"},{"れ", "re"},{"ろ", "ro"},
        {"わ", "wa"},{"を", "wo"},{"ん", "n"},
        {"が", "ga"},{"ぎ", "gi"},{"ぐ", "gu"},{"げ", "ge"},{"ご", "go"},
        {"ざ", "za"},{"じ", "zi"},{"ず", "zu"},{"ぜ", "ze"},{"ぞ", "zo"},
        {"だ", "da"},{"ぢ", "di"},{"づ", "du"},{"で", "de"},{"ど", "do"},
        {"ば", "ba"},{"び", "bi"},{"ぶ", "bu"},{"べ", "be"},{"ぼ", "bo"},
        {"ぱ", "pa"},{"ぴ", "pi"},{"ぷ", "pu"},{"ぺ", "pe"},{"ぽ", "po"},
        {"ぁ","xa" },{"ぃ","xi" },{"ぅ","xu" },{"ぇ","xe" },{"ぉ","xo" },
        {"っ", "xtu"},
        {"ゃ","xya" },{"ゅ","xyu" },{"ょ","xyo"},
        {"きゃ","kya"},{"きぃ","kyi"},{"きゅ","kyu"},{"きぇ","kye"},{"きょ","kyo"},
        {"しゃ","sya"},{"しぃ","syi"},{"しゅ","syu"},{"しぇ","she"},{"しょ","syo"},
        {"ちゃ","tya"},{"ちぃ","tyi"},{"ちゅ","tyu"},{"ちぇ","tye"},{"ちょ","tyo"},
        {"にゃ","nya"},{"にぃ","nyi"},{"にゅ","nyu"},{"にぇ","nye"},{"にょ","nyo"},
        {"ひゃ","hya"},{"ひぃ","hyi"},{"ひゅ","hyu"},{"ひぇ","hye"},{"ひょ","hyo"},
        {"みゃ","mya"},{"みぃ","myi"},{"みゅ","myu"},{"みぇ","mye"},{"みょ","myo"},
        {"りゃ","rya"},{"りぃ","ryi"},{"りゅ","ryu"},{"りぇ","rye"},{"りょ","ryo"},
        {"ぎゃ","gya"},{"ぎぃ","gyi"},{"ぎゅ","gyu"},{"ぎぇ","gye"},{"ぎょ","gyo"},
        {"じゃ","zya"},{"じぃ","zhi"},{"じゅ","zyu"},{"じぇ","zye"},{"じょ","zyo"},
        {"ぢゃ","dya"},{"ぢぃ","dyi"},{"ぢゅ","dyu"},{"ぢぇ","dye"},{"ぢょ","dyo"},
        {"びゃ","bya"},{"びぃ","byi"},{"びゅ","byu"},{"びぇ","bye"},{"びょ","byo"},
        {"てゃ","tha"},{"てぃ","thi"},{"てゅ","thu"},{"てぇ","the"},{"てょ","tho"},
        {"うぁ","wha"},{"うぃ","whi"},{"うぇ","whe"},{"うぉ","who"},
        {"でゃ","dha"},{"でぃ","dhi"},{"でゅ","dhu"},{"でぇ","dhe"},{"でょ","dho"},
        {"くぁ","qa"},{"くぃ","qi"},{"くぇ","qe"},{"くぉ","qo"},
        {"ふぁ","fa"},{"ふぃ","fi"},{"ふぇ","fe"},{"ふぉ","fo"},
        {"ヴぁ","va"},{"ヴぃ","vi"},{"ヴ","vu"},{"ヴぇ","ve"},{"ヴぉ","vo"},
        {"ぴゃ","pya"},{"ぴぃ","pyi"},{"ぴゅ","pyu"},{"ぴぇ","pye"},{"ぴょ","pyo"},
        {"、","," },{"。","."},{"「","["},{"」","]"},
    };

    // デフォルトではない入力方法
    public Dictionary<string, List<string>> dicEx = new Dictionary<string, List<string>>()
    {
        {"か", new List<string>{"ca"}},{"し", new List<string>{"ci","shi"}},{"く", new List<string>{"cu","qu"}},
        {"せ", new List<string>{"ce"}},{"こ", new List<string>{"co"}},{"ちゃ", new List<string>{"cha","cya"}},
        {"ち", new List<string>{"chi","cyi"}},{"ちゅ", new List<string>{"chu","cyu"}},
        {"ちぇ", new List<string>{"che","cye"}},{"ちょ", new List<string>{"cho","cyo"}},
        {"ふ", new List<string>{"fu"}},{"じゃ", new List<string>{"ja"}},{"じ", new List<string>{"ji"}},
        {"じゅ", new List<string>{"ju"}},{"じぇ", new List<string>{"je"}},{"じょ", new List<string>{"jo"}},
        {"ぁ", new List<string>{"la","xa"}},{"ぃ", new List<string>{"li","xi"}},{"ぅ", new List<string>{"lu","xu"}},
        {"ぇ", new List<string>{"le","xe"}},{"ぉ", new List<string>{"lo","xo"}},{"ゃ", new List<string>{"lya","xya"}},
        {"ゅ", new List<string>{"lyu","xyu"}},{"ょ", new List<string>{"lyo","xyo"}},
        {"っ", new List<string>{"ltu","xtu"}},{"ん", new List<string>{"nn"}},{"くぃ", new List<string>{"qyi"}},
        {"くぇ", new List<string>{"qye"}},{"しゃ", new List<string>{"sha"}},{"しゅ", new List<string>{"shu"}},
        {"しぇ", new List<string>{"she"}},{"しょ", new List<string>{"sho"}},{"つ", new List<string>{"tsu"}},
    };
}