using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using UnityEngine;

public class UserManagement : MonoBehaviour
{
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
        Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");

        // return true if doesn't match regex
        return !regex.IsMatch(password);
    }

    public bool Login(string username, string password)
    {
        Debug.Log(username + "\n" + password);

        // check empty input
        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            // do some rest api stuff here

            //if success
                return true;
            //else
                //return false
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
            //=========!!!!PUT USERNAME TAKEN ERROR CHECKING HERE!!!!!========================
            if (username == "loremipsum") //retrieve using rest api
            {
                return 2;
            }
            else if (!IsValidEmail(email))
            {
                return 3;
            }
            //========!!!!!PUT EMAIL EXIST ERROR CHECKING HERE!!!!!===========================
            else if (email == "lorem@ipsum.com") //retrieve using rest api
            {
                return 4;
            }
            else if (IsWeakPassword(password))
            {
                return 5;
            }
            else if (password != confirmPassword)
            {
                return 6;
            }
            else
            {
                return 0;
                // do some rest api here to register
                // if response success, return 0, else return 7
            }
        }
    }
}
