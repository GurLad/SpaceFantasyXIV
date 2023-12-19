using Godot;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

public class Formula
{
    private static readonly Regex PROPERTY_REGEX =
        new Regex("([a-zA-Z][a-zA-Z0-9]*)\\.([a-zA-Z][a-zA-Z0-9]*)+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Source { get; set; }

    public Formula(string source) => Source = source;

    public int ParseInt(params (string, IPhantasyObject)[] args)
    {
        int res;
        if (int.TryParse(Parse(args.ToList()), out res))
        {
            return res;
        }
        else
        {
            throw new Exception("Formula error! " + Source);
        }
    }

    public float ParseFloat(params (string, IPhantasyObject)[] args)
    {
        float res;
        if (float.TryParse(Parse(args.ToList()), out res))
        {
            return res;
        }
        else
        {
            throw new Exception("Formula error! " + Source);
        }
    }

    public string ParseString(params (string, IPhantasyObject)[] args) => Parse(args.ToList());

    private string Parse(List<(string, IPhantasyObject)> args)
    {
        string result = Source.ToLower();
        MatchCollection matches = PROPERTY_REGEX.Matches(result);
        for (int i = 0; i < matches.Count; i++)
        {
            List<string> values = matches[i].Groups.Values.ToList().ConvertAll(a => a.Value);
            GD.Print(string.Join(", ", values));
            IPhantasyObject obj = args.Find(a => a.Item1 == values[1]).Item2;
            string res = obj.GetProperty(values[2]);
            if (res != null)
            {
                result = result.Replace(values[0], res);
            }
            else
            {
                throw new Exception(values[1] + " doesn't have property " + values[2] + "!");
            }
        }
        return result;
    }
}
