using System;
using TMPro; //текст меш про
using UnityEngine;
using UnityEngine.UI;

// ТЗ: детский калькулятор в 4 действия
// Выводит пример по мере ввода данных, решает при нажатии "равно".
// ПЛАНИРУЕТСЯ потом: визуальное представление примера на яблоках и др. вещах. Ограничить ввод до... 20?
// - не хватает знаний: - научиться кодом генерить объекты, ресайз + анимация в зависимости от размера числа?

public class FieldController : MonoBehaviour
{
    //переменные типа кнопки для перетаскивания на них из иерархии в инспектор
    public Button btn_plus;
    public Button btn_minus;
    public Button btn_mult;
    public Button btn_div;
    public Button btn_result;

    // поля - состояние кнопок чтобы на них таскать спрайты для тех сотсояний, которые хочу менять кодом
    public Sprite img_btn_plus_normal;
    public Sprite img_btn_plus_selected;

    public Sprite img_btn_minus_normal;
    public Sprite img_btn_minus_selected;

    public Sprite img_btn_mult_normal;
    public Sprite img_btn_mult_selected;

    public Sprite img_btn_div_normal;
    public Sprite img_btn_div_selected;

    // результат
    private TMP_Text text_result;
    
    //поля ввода
    private TMP_InputField input_1;
    private TMP_InputField input_2;
    
    // переменная размером в один символ для знака действия
    private char math_sym = ' ';

    private void Start()
    {
        //  btn_plus = GetComponent<Button>();
        // на старте начинает слушать, не кликнули ли по каждой из кнопок?
        btn_plus.onClick.AddListener(btn_PlusClicked);
        btn_minus.onClick.AddListener(btn_MinusClicked);
        btn_mult.onClick.AddListener(btn_MultClicked);
        btn_div.onClick.AddListener(btn_DivClicked);

        btn_result.onClick.AddListener(btn_ResultClicked);

        // объекты (или переменные - что это??) для связи с полями
        // данные из полей ввода: ищет объект по имени и получает его "TextMeshPro_Imputfield" (неочевидно шо пипец, но это так)
        input_1 = GameObject.Find("Input1").GetComponent<TMP_InputField>();
        input_2 = GameObject.Find("Input2").GetComponent<TMP_InputField>();
        
        // результат на экране: ищет объект и получает его текстовое поле (!!!)
        text_result = GameObject.Find("ShowResult").GetComponent<TMP_Text>();
    }

    // ----------------------------------------

    // этот метод срабатывает, когда вводим в input первое число
    public void Input1_Changed(string new_text)
    {
        Debug.Log(new_text);
        
        //для последующего принудительного "бетонирования" выбранной кнопки через код: 
        btn_plus.enabled = true;
        btn_minus.enabled = true;
        btn_mult.enabled = true;
        btn_div.enabled = true;

        //основные состояния кнопок - нормальные
        btn_plus.GetComponent<Image>().sprite = img_btn_plus_normal;
        btn_minus.GetComponent<Image>().sprite = img_btn_minus_normal;
        btn_mult.GetComponent<Image>().sprite = img_btn_mult_normal;
        btn_div.GetComponent<Image>().sprite = img_btn_div_normal;
        
        // математический символ пока что будем выводить в виде пустого пробела
        math_sym = ' ';

        // при вводе данных начинаем сразу выводить пример в банке
        if (input_1.text != "") //НЕ РАВНО
            text_result.text = input_1.text; //дублирует текст из объекта первого инпута
        else text_result.text = "..."; //если стёр свой инпут - ставим обратно три точки

        // стереть данные второго числа, если правят первое
        input_2.text = "??";
    }

    // этот метод срабатывает, когда вводим в input второе число
    public void Input2_Changed(string new_text)
    {
        Debug.Log(new_text);
        
        if (input_2.text == "??")
            return; // выходим из метода

        // если первое число еще не вводили - выводим символ операции (_ или выбранный) и второе число к нему
        if (input_1.text == "??")
        {
            text_result.text = math_sym + input_2.text;
            return;
        }

        // выводим перове и второе числое и математический символ между ними (пробел, +, -, x или /)
        text_result.text = input_1.text + math_sym + input_2.text;
    }

    // ----------------------------------------

    // кнопка плюс
    public void btn_PlusClicked()
    {
        Debug.Log("Click +");

        btn_plus.enabled = false; // <-- делаем кнопку недоступной
        btn_minus.enabled = true;
        btn_mult.enabled = true;
        btn_div.enabled = true;
        
        btn_plus.GetComponent<Image>().sprite = img_btn_plus_selected; // <-- фиксируем кнопку "+" нажатой
        btn_minus.GetComponent<Image>().sprite = img_btn_minus_normal;
        btn_mult.GetComponent<Image>().sprite = img_btn_mult_normal;
        btn_div.GetComponent<Image>().sprite = img_btn_div_normal;

        // запоминаем выбранный математический символ
        math_sym = '+';
        
        // отображаем первое число и "+"
        text_result.text = input_1.text + math_sym;
        
        // если есть второе число, также отображаем его в конце строки
        if (input_2.text != "" && input_2.text != "??")
            text_result.text += input_2.text;
    }

    // кнопка минус
    public void btn_MinusClicked()
    {
        Debug.Log("Click -");

        btn_plus.enabled = true;
        btn_minus.enabled = false; // <-- делаем кнопку недоступной
        btn_mult.enabled = true;
        btn_div.enabled = true;
        
        btn_plus.GetComponent<Image>().sprite = img_btn_plus_normal;
        btn_minus.GetComponent<Image>().sprite = img_btn_minus_selected; // <-- фиксируем кнопку "-" нажатой
        btn_mult.GetComponent<Image>().sprite = img_btn_mult_normal;
        btn_div.GetComponent<Image>().sprite = img_btn_div_normal;
        
        // запоминаем выбранный математический символ
        math_sym = '-';
        
        // отображаем первое число и "-"
        text_result.text = input_1.text + math_sym;
        
        // если есть второе число, также отображаем его в конце строки
        if (input_2.text != "" && input_2.text != "??")
            text_result.text += input_2.text;
    }

    // кнопка умножить
    public void btn_MultClicked()
    {
        Debug.Log("Click *");

        btn_plus.enabled = true;
        btn_minus.enabled = true;
        btn_mult.enabled = false; // <-- делаем кнопку недоступной
        btn_div.enabled = true;
        
        btn_plus.GetComponent<Image>().sprite = img_btn_plus_normal;
        btn_minus.GetComponent<Image>().sprite = img_btn_minus_normal;
        btn_mult.GetComponent<Image>().sprite = img_btn_mult_selected; // <-- фиксируем кнопку "x" нажатой
        btn_div.GetComponent<Image>().sprite = img_btn_div_normal;
        
        // запоминаем выбранный математический символ ("xs" вместо "*")
        math_sym = 'x';
        
        // отображаем первое число и "x"
        text_result.text = input_1.text + math_sym;

        // если есть второе число, также отображаем его в конце строки
        if (input_2.text != "" && input_2.text != "??")
            text_result.text += input_2.text;
    }

    // кнопка деление
    public void btn_DivClicked()
    {
        Debug.Log("Click /");

        btn_plus.enabled = true;
        btn_minus.enabled = true;
        btn_mult.enabled = true;
        btn_div.enabled = false; // <-- делаем кнопку недоступной
        
        btn_plus.GetComponent<Image>().sprite = img_btn_plus_normal;
        btn_minus.GetComponent<Image>().sprite = img_btn_minus_normal;
        btn_mult.GetComponent<Image>().sprite = img_btn_mult_normal;
        btn_div.GetComponent<Image>().sprite = img_btn_div_selected; // <-- фиксируем кнопку "/" нажатой
        
        // запоминаем выбранный математический символ
        math_sym = '/';
        
        // отображаем первое число и "/"
        text_result.text = input_1.text + math_sym;
        
        // если есть второе число, также отображаем его в конце строки
        if (input_2.text != "" && input_2.text != "??")
            text_result.text += input_2.text;
    }

    // кнопка равно
    public void btn_ResultClicked()
    {
        Debug.Log("Click =");
        
        // были ли введены оба числа? (если пусто или ?? - выкинет за пределы функции)
        if (input_1.text == "" || input_1.text == "??")
            return;
        if (input_2.text == "" || input_2.text == "??")
            return;       
        // не выбрана математическая операция? - выкинет за пределы функции
        if (math_sym == ' ')
            return;

        // конвертировать введенные тексты в числа  (!)
        float a = float.Parse(input_1.text);
        float b = float.Parse(input_2.text);
        
        // создаем переменную для результата
        float res = 0;

        // если выбрана кнопка сумма
        if (!btn_plus.enabled)
            res = a + b;

        // если выбрано кнопка вычитание
        if (!btn_minus.enabled)
            res = a - b;

        // если выбрано кнопка умножение
        if (!btn_mult.enabled)
            res = a * b;

        // если выбрана кнопка деление
        // причем делить на ноль нельзя
        if (!btn_div.enabled)
            if (b == 0)
            {
                text_result.text = "Oops!"; 
                return;  // выходим из метода
            }
            else
                // делим и округляем float до тысячных
                res = (float)Math.Round(a / b, 3);

        // отбражаем окончательный результат, например, "2+2=4"
        text_result.text = input_1.text + math_sym + input_2.text + '=' + res;
    }
}