using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllegalGridPlacmentException : Exception
{
    public IllegalGridPlacmentException(string message) : base(message)
    {
    }
}
