using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Calculator : MonoBehaviour
{
   public TMP_InputField OneNumberInputField;
   public TMP_InputField TwoNumberInputField;
   public TMP_Text ResultText;
    public void Add()
    {
        float num1 = float.Parse(OneNumberInputField.text);
        float num2 = float.Parse(TwoNumberInputField.text);
        float result = num1 + num2;
        ResultText.text = result.ToString();
    }
    // Метод для вычитания
    public void Subtract()
    {
        float num1 = float.Parse(OneNumberInputField.text);
        float num2 = float.Parse(TwoNumberInputField.text);
        float result = num1 - num2;
        ResultText.text = result.ToString();
    }
    // Метод для умножения
    public void Multiply()
    {
        float num1 = float.Parse(OneNumberInputField.text);
        float num2 = float.Parse(TwoNumberInputField.text);
        float result = num1 * num2;
        ResultText.text = result.ToString();
    }
    // Метод для деления
    public void Divide()
    {
        float num1 = float.Parse(OneNumberInputField.text);
        float num2 = float.Parse(TwoNumberInputField.text);
        // Проверка на деление на ноль
        if (num2 != 0)
        {
            float result = num1 / num2;
            ResultText.text = result.ToString();
        }
        else
        {
            ResultText.text = "Ошибка! Деление на ноль!";
        }
    }
}
