using Godot;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

public record Formula(string Source)
{
    private static readonly Regex PROPERTY_REGEX =
        new Regex("([a-zA-Z][a-zA-Z0-9]*)\\.([a-zA-Z][a-zA-Z0-9]*)+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public int ParseInt(params (string, IInterpretableObject)[] args)
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

    private string Parse(List<(string, IInterpretableObject)> args)
    {
        string result = Source;
        MatchCollection matches = PROPERTY_REGEX.Matches(Source);
        for (int i = 0; i < matches.Count; i++)
        {
            List<string> values = matches[i].Groups.Values.ToList().ConvertAll(a => a.Value);
            GD.Print(string.Join(", ", values));
            IInterpretableObject obj = args.Find(a => a.Item1 == values[1]).Item2;
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
