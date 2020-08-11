using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class TMProExtensionMethods
{
    public static void AlignmentMidlineLeft(this TMP_InputField input)
    {
        input.textComponent.alignment = TextAlignmentOptions.MidlineLeft;
    }

    public static void CharacterValidationDecimal(this TMP_InputField input)
    {
        input.characterValidation = TMP_InputField.CharacterValidation.Decimal;
    }

    public static void CharacterValidationInteger(this TMP_InputField input)
    {
        input.characterValidation = TMP_InputField.CharacterValidation.Integer;
    }

    public static void CharacterValidationEmail(this TMP_InputField input)
    {
        input.characterValidation = TMP_InputField.CharacterValidation.EmailAddress;
    }
}
