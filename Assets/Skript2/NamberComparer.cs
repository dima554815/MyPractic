using UnityEngine;
using TMPro; // подключаем TextMeshPro
using UnityEngine.UI; // для Text

public class TwoNumbersComparer : MonoBehaviour
{
    public TMP_InputField inputField1;  // поле ввода 1
    public TMP_InputField inputField2;  // поле ввода 2
    public TMP_Text resultText;         // поле для вывода результата

    public void CompareNumbers()
    {
        // Очищаем пробелы
        string input1 = inputField1.text.Trim();
        string input2 = inputField2.text.Trim();

        // Пробуем конвертировать
        if (float.TryParse(input1, out float num1) && float.TryParse(input2, out float num2))
        {
            if (Mathf.Approximately(num1, num2))
            {
                resultText.text = "Числа равны.";
            }
            else if (num1 > num2)
            {
                resultText.text = "Первое число больше.";
            }
            else
            {
                resultText.text = "Второе число больше.";
            }
        }
        else
        {
            resultText.text = "Ошибка ввода. Введите два числа.";
        }
    }
}

