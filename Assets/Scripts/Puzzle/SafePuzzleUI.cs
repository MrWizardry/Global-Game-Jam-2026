using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SafePuzzleUI : MonoBehaviour
{
    [Header("Combinação correta")]
    [Tooltip("Digite a combinação de 3 números do cofre.")]
    [Range(0, 999)]
    public int correctCombination = 123;

    [Header("Números dos slots")]
    [SerializeField] private int number1 = 0;
    [SerializeField] private int number2 = 0;
    [SerializeField] private int number3 = 0;

    [Header("Textos dos slots")]
    [SerializeField] private TMP_Text number1Text;
    [SerializeField] private TMP_Text number2Text;
    [SerializeField] private TMP_Text number3Text;

    [Header("Botões - Slot 1")]
    [SerializeField] private Button increaseButton1;
    [SerializeField] private Button decreaseButton1;

    [Header("Botões - Slot 2")]
    [SerializeField] private Button increaseButton2;
    [SerializeField] private Button decreaseButton2;

    [Header("Botões - Slot 3")]
    [SerializeField] private Button increaseButton3;
    [SerializeField] private Button decreaseButton3;

    [Header("Botão Confirmar")]
    [SerializeField] private Button confirmButton;


    private void Start()
    {
        // Atualiza os números na interface
        UpdateUI();

        // Slot 1
        increaseButton1.onClick.AddListener(IncreaseNumber1);
        decreaseButton1.onClick.AddListener(DecreaseNumber1);

        // Slot 2
        increaseButton2.onClick.AddListener(IncreaseNumber2);
        decreaseButton2.onClick.AddListener(DecreaseNumber2);

        // Slot 3
        increaseButton3.onClick.AddListener(IncreaseNumber3);
        decreaseButton3.onClick.AddListener(DecreaseNumber3);

        // Confirmar
        confirmButton.onClick.AddListener(ConfirmCombination);
    }


    // =========================
    // SLOT 1
    // =========================

    private void IncreaseNumber1()
    {
        number1++;

        if (number1 > 9)
            number1 = 0;

        UpdateUI();
    }

    private void DecreaseNumber1()
    {
        number1--;

        if (number1 < 0)
            number1 = 9;

        UpdateUI();
    }


    // =========================
    // SLOT 2
    // =========================

    private void IncreaseNumber2()
    {
        number2++;

        if (number2 > 9)
            number2 = 0;

        UpdateUI();
    }

    private void DecreaseNumber2()
    {
        number2--;

        if (number2 < 0)
            number2 = 9;

        UpdateUI();
    }


    // =========================
    // SLOT 3
    // =========================

    private void IncreaseNumber3()
    {
        number3++;

        if (number3 > 9)
            number3 = 0;

        UpdateUI();
    }

    private void DecreaseNumber3()
    {
        number3--;

        if (number3 < 0)
            number3 = 9;

        UpdateUI();
    }


    // =========================
    // ATUALIZAR INTERFACE
    // =========================

    private void UpdateUI()
    {
        if (number1Text != null)
            number1Text.text = number1.ToString();

        if (number2Text != null)
            number2Text.text = number2.ToString();

        if (number3Text != null)
            number3Text.text = number3.ToString();
    }


    // =========================
    // CONFIRMAR COMBINAÇÃO
    // =========================

    private void ConfirmCombination()
    {
        int playerCombination =
            (number1 * 100) +
            (number2 * 10) +
            number3;


        if (playerCombination == correctCombination)
        {
            Debug.Log("COFRE ABERTO! Combinação correta: " + playerCombination);
        }
        else
        {
            Debug.Log("Combinação incorreta! Jogador colocou: " + playerCombination);
        }
    }
}