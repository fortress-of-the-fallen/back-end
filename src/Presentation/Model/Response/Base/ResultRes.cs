﻿namespace WebApi.Models.Responses.Base;

public class ResultRes<T> : ExecutionRes
{
    public T? Result { get; set; }
}