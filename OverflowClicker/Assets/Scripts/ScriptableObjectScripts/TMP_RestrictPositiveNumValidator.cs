using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(
    fileName = "RestricPositiveNumValidator",
    menuName = "TextMeshPro/Create Restric Positive Num Validator")]
public class TMP_RestricPositiveNumValidator : TMP_InputValidator
{
    public override char Validate(ref string text, ref int pos, char ch)
    {
        if (!char.IsDigit(ch)) // 入力が数字じゃなかったらnull文字を返す
        {
            return '\0';
        }
        text += ch; // テキストを更新
        pos++; // カーソル位置をずらす

        return ch;
    }
}
