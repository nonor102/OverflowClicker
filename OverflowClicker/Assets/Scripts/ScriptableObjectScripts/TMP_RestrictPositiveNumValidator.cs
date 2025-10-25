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
        if (!char.IsDigit(ch)) // ���͂���������Ȃ�������null������Ԃ�
        {
            return '\0';
        }
        text += ch; // �e�L�X�g���X�V
        pos++; // �J�[�\���ʒu�����炷

        return ch;
    }
}
