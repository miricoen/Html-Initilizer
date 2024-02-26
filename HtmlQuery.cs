namespace practycode_22
{
    public class HtmlQuery
    {
        public static HashSet<HtmlElement> Search(HtmlElement rootElement, string query)
        {
            var result = new HashSet<HtmlElement>();
            SearchElements(rootElement, query, result);
            return result;
        }

        private static void SearchElements(HtmlElement element, string query, HashSet<HtmlElement> result)
        {
            // Split the query into individual components
            var selectors = query.Split(' ');

            // Check if the current element matches all selectors
            bool matchesAllSelectors = true;
            foreach (var selector in selectors)
            {
                if (!IsMatching(element, selector))
                {
                    matchesAllSelectors = false;
                    break;
                }
            }

            if (matchesAllSelectors)
            {
                result.Add(element);
            }

            // Recursively search through children elements
            foreach (var child in element.Child)
            {
                SearchElements(child, query, result);
            }
        }

        private static bool IsMatching(HtmlElement element, string selector)
        {
            // Check for tag name
            if (selector.StartsWith("."))
            {
                return element.Classes.Contains(selector.TrimStart('.'));
            }
            else if (selector.StartsWith("#"))
            {
                return element.Id == selector.TrimStart('#');
            }
            else
            {
                return element.Name == selector;
            }
        }
    }

}
