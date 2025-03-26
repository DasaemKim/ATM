using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.RestService;
using UnityEngine.UI;
using System.IO;
using UnityEditor.PackageManager;
public class Popup_Bank : MonoBehaviour
{
    public Text UserNameText;
    public TextMeshProUGUI Balance;
    public TextMeshProUGUI Cash_text;

    public GameObject DipositUI;
    public GameObject WithdrawUI;
    public GameObject DepositButton;
    public GameObject WithdrawButton;
    public Button DipositBackButton;
    public Button WithdrawBackButton;
    public GameObject Cash_Lack;
    public Button OK_button;
    public GameObject LoginErrorUI;
    public Button Error_BackButton;

    public TMP_InputField DepositInputField;
    public TMP_InputField WithdrawInputField;
    public TMP_InputField IDInputField;
    public TMP_InputField PasswordInputField;

    private void Start()
    {   
        GameManager.Instance.LoadData();
        UpdateUI();
        DepositInputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        WithdrawInputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        DepositInputField.onValueChanged.AddListener(ValidateInput);
        WithdrawInputField.onValueChanged.AddListener(ValidateInput);
        
    }

    public void UpdateUI()
    {
        UserNameText.text = GameManager.Instance.userData.Name;
        Balance.text = GameManager.Instance.userData.Balance.ToString("N0");
        Cash_text.text = GameManager.Instance.userData.Cash.ToString("N0");
    }

    public void DipositUIScreen()
    {
        DipositUI.SetActive(true);
        WithdrawUI.SetActive(false);
        DepositButton.SetActive(false);
        WithdrawButton.SetActive(false);
    }

    public void WtihdrawUIScreen()
    {
        DipositUI.SetActive(false);
        WithdrawUI.SetActive(true);
        DepositButton.SetActive(false);
        WithdrawButton.SetActive(false);
    }

    public void Buttons()
    {
        DepositButton.SetActive(true);
        WithdrawButton.SetActive(true);
    }

    public void DipositUIBackButton()
    {
        DipositUI.SetActive(false);
        DepositButton.SetActive(true);
        WithdrawButton.SetActive(true);
    }

    public void WithdrawUIBackButton()
    {
        WithdrawUI.SetActive(false);
        DepositButton.SetActive(true);
        WithdrawButton.SetActive(true);
    }

    private void ValidateInput(string input)
    {
        string filtered = System.Text.RegularExpressions.Regex.Replace(input, "[^0-9]", "");
        if (input != filtered)
        {
            DepositInputField.text = filtered;
            WithdrawInputField.text = filtered;
        }
    }

    public void QuickDeposit(int amount)
    {
        TryDeposit(amount);
    }

    public void QuickWithdraw(int amount)
    {
        TryWithdraw(amount);
    }

    public void DepositCash()
    {
        if (int.TryParse(DepositInputField.text, out int amount) && amount > 0)
        {
            TryDeposit(amount);
        }
    }

    public void WithdrawCash()
    {
        if(int.TryParse(WithdrawInputField.text, out int amount) && amount > 0)
        {
            if (GameManager.Instance.userData.Balance >= amount)
            {
                GameManager.Instance.userData.Balance -= amount;
                GameManager.Instance.userData.Cash += amount;
                GameManager.Instance.SaveData();
                WithdrawInputField.text = "";
                UpdateUI();
            }

            else
            {
                Cash_Lack.SetActive(false);
            }
        }
    }

    public void OKButton()
    {
        Cash_Lack.SetActive(false);
        DepositInputField.text = "";
        WithdrawInputField.text = "";
    }

    private void TryDeposit(int amount)
    {
        if (GameManager.Instance.userData.Cash >= amount)
        {
            GameManager.Instance.userData.Cash -= amount;
            GameManager.Instance.userData.Balance += amount;
            GameManager.Instance.SaveData();
            DepositInputField.text = "";
            UpdateUI();
        }

        else
        {
            Cash_Lack.SetActive(true);
        }
    }

    private void TryWithdraw(int amount)
    {
        if (GameManager.Instance.userData.Cash >= amount)
        {
            GameManager.Instance.userData.Cash += amount;
            GameManager.Instance.userData.Balance -= amount;
            GameManager.Instance.SaveData();
            WithdrawInputField.text = "";
            UpdateUI();
        }

        else
        {
            Cash_Lack.SetActive(true);
        }
    }

    public void LoginButton()
    {
        bool suceess =GameManager.Instance.Login(IDInputField.text, PasswordInputField.text);

        if (!suceess)
        {
            LoginErrorUI.SetActive(true); 
        }
    }

    public void ErrorBackButton()
    {
        LoginErrorUI.SetActive(false);
    }
}
