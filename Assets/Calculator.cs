using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // подключаем TextMeshPro

public class Calculator : MonoBehaviour
{
    // Поля для ввода чисел (TMP)
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;

    // Текстовое поле для вывода результата (TMP)
    public TMP_Text resultText;

    void Start()
    {
        // Ограничиваем ввод только числовыми значениями
        inputField1.contentType = TMP_InputField.ContentType.DecimalNumber;
        inputField2.contentType = TMP_InputField.ContentType.DecimalNumber;
        inputField1.ForceLabelUpdate();
        inputField2.ForceLabelUpdate();
    }

    private bool ValidateInputs(out float num1, out float num2)
    {
        num1 = 0;
        num2 = 0;

        if (string.IsNullOrWhiteSpace(inputField1.text) || string.IsNullOrWhiteSpace(inputField2.text))
        {
            resultText.text = "Ошибка! Поля не должны быть пустыми.";
            return false;
        }

        if (!float.TryParse(inputField1.text.Trim(), out num1) || !float.TryParse(inputField2.text.Trim(), out num2))
        {
            resultText.text = "Ошибка! Введите корректные числа.";
            return false;
        }

        return true;
    }

    // Метод для сложения
    public void Add()
    {
        if (!ValidateInputs(out float num1, out float num2)) return;

       
        float result = num1 + num2;
        resultText.text = "Результат: " + result.ToString();
    }

    // Метод для вычитания
    public void Subtract()
    {
        if (!ValidateInputs(out float num1, out float num2)) return;

       
        float result = num1 - num2;
        resultText.text = "Результат: " + result.ToString();
    }

    // Метод для умножения
    public void Multiply()
    {
        if (!ValidateInputs(out float num1, out float num2)) return;

        float result = num1 * num2;
        resultText.text = "Результат: " + result.ToString();
    }

    // Метод для деления
    public void Divide()
    {
        if (!ValidateInputs(out float num1, out float num2)) return;

        if (num1 == 0)
        {
            resultText.text = "Ошибка! Деление на ноль.";
            return;
        }

        if (num2 == 0)
        {
            resultText.text = "Ошибка! Деление на ноль!";
            return;
        }

        float result = num1 / num2;
        resultText.text = "Результат: " + result.ToString();
    }
}