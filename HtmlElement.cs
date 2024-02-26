

namespace practycode_22
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; private set; }
        public List<string> Classes { get; private set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Child { get; set; }

        public IEnumerable<HtmlElement> Descendants()
        {
            // Use a hashset to avoid duplicates and improve performance
            var visited = new HashSet<HtmlElement>();
            return Descendants(this, visited);
        }

        private IEnumerable<HtmlElement> Descendants(HtmlElement element, HashSet<HtmlElement> visited)
        {
            if (!visited.Add(element))
            {
                yield break;
            }

            foreach (var child in element.Child)
            {
                yield return child;
                foreach (var descendant in Descendants(child, visited))
                {
                    yield return descendant;
                }
            }
        }

        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement currentElement = this;
            while (currentElement != null)
            {
                yield return currentElement;
                currentElement = currentElement.Parent;
            }
        }

        public HashSet<HtmlElement> FindElementsBySelector(Selector selector)
        {
            var result = new HashSet<HtmlElement>();
            FindElementsBySelectorRecursive(this, selector, result);
            return result;
        }

        private void FindElementsBySelectorRecursive(HtmlElement currentElement, Selector selector, HashSet<HtmlElement> result)
        {
            // Check if the current element matches the selector criteria
            if (MatchesSelector(currentElement, selector))
            {
                result.Add(currentElement);
            }

            // Recursively search through children elements
            foreach (var child in currentElement.Child)
            {
                FindElementsBySelectorRecursive(child, selector, result);
            }
        }

        private bool MatchesSelector(HtmlElement element, Selector selector)
        {
            // Check tag name
            if (!string.IsNullOrEmpty(selector.TagName) && element.Name != selector.TagName)
            {
                return false;
            }

            // Check id
            if (!string.IsNullOrEmpty(selector.Id) && element.Id != selector.Id)
            {
                return false;
            }

            // Check classes
            if (selector.Classes != null && selector.Classes.Any())
            {
                if (element.Classes == null || !selector.Classes.All(c => element.Classes.Contains(c)))
                {
                    return false;
                }
            }

            // Additional conditions can be added here based on the requirements

            return true;
        }

        public HtmlElement(string name, string id = "", List<string> attributes = null, List<string> classes = null, string innerHtml = "", HtmlElement parent = null, List<HtmlElement> child = null)
        {
            Id = id;
            Name = name;
            Attributes = attributes ?? new List<string>();
            Classes = classes ?? new List<string>();
            InnerHtml = innerHtml;
            Parent = parent;
            Child = child ?? new List<HtmlElement>();
        }

        public override string ToString()
        {
            return $"<{Name}{GetAttributesString()}{GetClassesString()}>{InnerHtml}</{Name}>";
        }

        private string GetAttributesString()
        {
            if (Attributes.Count == 0)
            {
                return "";
            }

            return " " + string.Join(" ", Attributes.Select(a => $"{a}"));
        }

        private string GetClassesString()
        {
            if (Classes.Count == 0)
            {
                return "";
            }

            return $" class=\"{string.Join(" ", Classes)}\"";
        }
    }


}
