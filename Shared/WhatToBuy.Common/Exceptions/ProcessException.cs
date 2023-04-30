namespace WhatToBuy.Common.Exceptions;

using Microsoft.AspNetCore.Http;
using System;

/// <summary>
/// Base exception for transferred error in the work process
/// </summary>
public class ProcessException : Exception
{
    /// <summary>
    ///Error code
    /// </summary>
    public int Code { get; } = StatusCodes.Status400BadRequest;

    /// <summary>
    /// Error name
    /// </summary>
    public string Name { get; }

    #region Constructors

    public ProcessException()
    {
    }

    public ProcessException(string message) : base(message)
    {
    }

    public ProcessException(Exception inner) : base(inner.Message, inner)
    {
    }

    public ProcessException(string message, Exception inner) : base(message, inner)
    {
    }

    public ProcessException(int code, string message) : base(message)
    {
        Code = code;
    }

    public ProcessException(int code, string message, Exception inner) : base(message, inner)
    {
        Code = code;
    }

    #endregion

    public static void ThrowIf(Func<bool> predicate, string message)
    {
        if (predicate.Invoke())
            throw new ProcessException(message);
    }

    public static void ThrowIf(Func<bool> predicate, int statusCode, string message)
    {
        if (predicate.Invoke())
            throw new ProcessException(statusCode, message);
    }
}

