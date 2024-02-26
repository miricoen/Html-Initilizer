//using practycode_22;
//using System.Text.RegularExpressions;

//public class HtmlTreeBuilder
//{
//    private HtmlElement _rootElement; // The root element of the HTML tree
//    private HtmlElement _currentElement; // The current element being processed

//    public HtmlTreeBuilder()
//    {
//        _rootElement = new HtmlElement("html", null, new List<string>(), new List<string>(), "", null, new List<HtmlElement>());
//        _currentElement = _rootElement; // Set current element to root initially
//    }

//    public HtmlElement BuildTree(List<string> lines)
//    {
//        foreach (string line in lines)
//        {
//            string remainingLine = line.Trim();

//            while (!string.IsNullOrEmpty(remainingLine))
//            {
//                if (IsComment(line))
//                {
//                    remainingLine = "";
//                    continue;
//                }

//                if (remainingLine.StartsWith("<html"))
//                {
//                    _currentElement = _rootElement;
//                    remainingLine = remainingLine.Substring(6).Trim();
//                }
//                else if (remainingLine.StartsWith("</"))
//                {
//                    if (_currentElement != null)
//                    {
//                        _currentElement = _currentElement.Parent;
//                    }
//                    remainingLine = remainingLine.Substring(2).Trim();
//                }
//                else if (remainingLine.StartsWith("<"))
//                {
//                    remainingLine = ParseTag(remainingLine);
//                }
//                else
//                {
//                    if (_currentElement != null)
//                    {
//                        _currentElement.InnerHtml += remainingLine;
//                    }
//                    remainingLine = "";
//                }
//            }
//        }

//        return _rootElement;
//    }

//    private string ParseTag(string line)
//    {
//        string tagName = Regex.Match(line, @"<([^>\s/]+)").Groups[1].Value;
//        line = line.Substring(tagName.Length).Trim();

//        if (!IsValidTag(tagName))
//        {
//            Console.WriteLine($"Invalid HTML tag: {tagName}");
//            return "";
//        }

//        HtmlElement newElement = new HtmlElement(tagName, null, new List<string>(), new List<string>(), "", _currentElement, new List<HtmlElement>());

//        Match idMatch = Regex.Match(line, @"id=""([^""]*)""");
//        if (idMatch.Success)
//        {
//            newElement.Id = idMatch.Groups[1].Value;
//        }

//        Match classMatch = Regex.Match(line, @"class=""([^""]*)""");
//        if (classMatch.Success)
//        {
//            string classes = classMatch.Groups[1].Value; // Extract and set the classes
//            newElement.Classes.AddRange(classes.Split(' '));
//        }

//        //line = line.Replace(idMatch.Value, "").Replace(classMatch.Value, "").Trim();

//        if (!string.IsNullOrEmpty(idMatch.Value))
//        {
//            line = line.Replace(idMatch.Value, "");
//        }

//        if (!string.IsNullOrEmpty(classMatch.Value))
//        {
//            line = line.Replace(classMatch.Value, "");
//        }

//        line = line.TrimStart('>');

//        if (line.EndsWith("/>") && IsSelfClosingTag(tagName))
//        {
//            _currentElement.Child.Add(newElement);
//        }
//        else
//        {
//            _currentElement.Child.Add(newElement);
//            _currentElement = newElement;
//        }

//        return line.TrimStart('>');
//    }

//    public bool IsValidTag(string tag)
//    {
//        return HtmlHelper.Helper.IsValidHtmlTag(tag);
//    }

//    public bool IsSelfClosingTag(string tag)
//    {
//        return HtmlHelper.Helper.IsSelfClosingTag(tag);
//    }

//    private bool IsComment(string line)
//    {
//        return line.Trim().StartsWith("<!--");
//    }
//}

using practycode_22;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class HtmlTreeBuilder
{
    private HtmlElement _rootElement; // The root element of the HTML tree
    private HtmlElement _currentElement; // The current element being processed

    public HtmlTreeBuilder()
    {
        _rootElement = new HtmlElement("html", null, new List<string>(), new List<string>(), "", null, new List<HtmlElement>());
        _currentElement = _rootElement; // Set current element to root initially
    }

    public HtmlElement BuildTree(List<string> lines)
    {
        foreach (string line in lines)
        {
            string remainingLine = line.Trim();

            while (!string.IsNullOrEmpty(remainingLine))
            {
                if (IsComment(line))
                {
                    remainingLine = "";
                    continue;
                }

                if (remainingLine.StartsWith("</"))
                {
                    if (_currentElement != null)
                    {
                        _currentElement = _currentElement.Parent;
                    }

                    remainingLine = remainingLine.Substring(2).Trim();
                }
                else if (remainingLine.StartsWith("<"))
                {
                    remainingLine = ParseTag(remainingLine);
                }
                else
                {
                    if (_currentElement != null)
                    {
                        _currentElement.InnerHtml += remainingLine;
                    }

                    remainingLine = "";
                }
            }
        }

        return _rootElement;
    }

    private string ParseTag(string line)
    {
        string tagName = Regex.Match(line, @"<([^>\s/]+)").Groups[1].Value;
        line = line.Substring(tagName.Length).Trim();

        if (!IsValidTag(tagName))
        {
            Console.WriteLine($"Invalid HTML tag: {tagName}");
            return "";
        }

        HtmlElement newElement = new HtmlElement(tagName, null, new List<string>(), new List<string>(), "", _currentElement, new List<HtmlElement>());

        Match idMatch = Regex.Match(line, @"id=""([^""]*)""");
        if (idMatch.Success)
        {
            newElement.Id = idMatch.Groups[1].Value;
        }

        Match classMatch = Regex.Match(line, @"class=""([^""]*)""");
        if (classMatch.Success)
        {
            string classes = classMatch.Groups[1].Value; // Extract and set the classes
            newElement.Classes.AddRange(classes.Split(' '));
        }

        //line = line.Replace(idMatch.Value, "").Replace(classMatch.Value, "").Trim();

        if (IsSelfClosingTag(tagName))
        {
            _currentElement.Child.Add(newElement);
        }
        else
        {
            _currentElement.Child.Add(newElement);
            _currentElement = newElement;
        }

        return line.TrimStart('>');
    }

    public bool IsValidTag(string tag)
    {
        return HtmlHelper.Helper.IsValidHtmlTag(tag);
    }

    public bool IsSelfClosingTag(string tag)
    {
        return HtmlHelper.Helper.IsSelfClosingTag(tag);
    }

    private bool IsComment(string line)
    {
        return line.Trim().StartsWith("<!--");
    }
}


