using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class PhantasyScript
{
    private static readonly Regex FUNCTION_REGEX =
        new Regex("([a-zA-Z][a-zA-Z0-9]*)\\.\\(([^,\\(\\)]*)?(,[^,\\(\\)]*)*\\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Source { get; set; }

    public PhantasyScript(string source) => Source = source;

    public void Run(params (string, IPhantasyObject)[] args)
    {
        if (string.IsNullOrEmpty(Source))
        {
            return;
        }
        Parse(args.ToList());
    }

    private void Parse(List<(string, IPhantasyObject)> args)
    {
        MatchCollection matches = FUNCTION_REGEX.Matches(Source.ToLower());
        for (int i = 0; i < matches.Count; i++)
        {
            List<string> values = matches[i].Groups.Values.ToList().ConvertAll(a => a.Value);
            GD.Print(string.Join(", ", values));
            IPhantasyObject obj = args.Find(a => a.Item1 == values[1]).Item2;
            List<string> funcArgs = new List<string>();
            for (int j = 2; j < values.Count; j++)
            {
                funcArgs.Add(new Formula(values[i]).ParseString(args.ToArray()));
            }
            obj.RunFunction(funcArgs);
        }
    }
}
