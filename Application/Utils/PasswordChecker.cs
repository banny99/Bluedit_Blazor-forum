﻿using System.Text.RegularExpressions;

namespace Application.Utils;

public class PasswordChecker
{
    public static int CheckStrength(string password)
    {
        int score = 1;

        if (password.Length < 1)
            return -1;
        if (password.Length < 4)
            score++;
        if (password.Length >= 8)
            score++;
        if (password.Length >= 12)
            score++;
        if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
            score++;
        if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
            Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
            score++;
        if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
            score++;

        return score;
    }
}