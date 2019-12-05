using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using UnityEngine;

public class UserManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            MailAddress mailAddress = new MailAddress(email);

            return true;
        }
        catch
        {
            return false;
        }
    }

    // check for weak password
    private bool IsWeakPassword(string password)
    {
        Regex regex = new Regex(@"\d+");

        // return true if doesn't match regex
        return !regex.IsMatch(password);
    }

    public bool Login(string username, string password)
    {
        Debug.Log(username + "\n" + password);

        if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)) // check empty input
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int Register(string username, string email, string password, string confirmPassword)
    {
        /* --Registration Result Code--
        0: Success
        1: Empty input
        2: Username taken
        3: Invalid email
        4: Email exists
        5: Weak password
        6: Confirm password not match
        7: Unknown error
        */

        Debug.Log(username + "\n" + email + " " + password + " " + confirmPassword);

        // check empty input
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            return 1;
        }
        else
        {
            //XXXX!!!!PUT USERNAME TAKEN ERROR CHECKING HERE!!!!!XXXX
            if (!IsValidEmail(email))
            {
                return 3;
            }
            //XXXX!!!!!PUT EMAIL EXIST ERROR CHECKING HERE!!!!!XXXX
            else if (IsWeakPassword(password))
            {
                return 5;
            }
        }

        return 7;
    }
}
