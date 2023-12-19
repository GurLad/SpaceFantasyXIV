using Godot;
using System;
using System.Collections.Generic;

public interface IPhantasyObject
{
    public string GetProperty(string name);

    public void RunFunction(List<object> args);
}
