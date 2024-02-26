// קבלת שם הקובץ
using practycode_22;

void PrintTree(HtmlElement element)
{
    Console.WriteLine($"<{element.Name}>");

    foreach (var child in element.Child)
    {
        PrintTree(child);
    }

    Console.WriteLine($" {element.InnerHtml}");
    //Console.WriteLine($"</{element.Name}>  {element.InnerHtml}");
}




string fileName = "example.html";

// קריאה של כל תוכן הקובץ
List<string> htmlContent = new List<string>(File.ReadAllLines(fileName));

HtmlTreeBuilder treeBuilder = new HtmlTreeBuilder();

// יצירת עץ HTML
HtmlElement rootElement = treeBuilder.BuildTree(htmlContent.ToList<string>());


PrintTree(rootElement);

// חיפוש אלמנטים
Console.WriteLine("---------");
var results = HtmlQuery.Search(rootElement, "main");

// הדפסת התוכן הפנימי של כל אלמנט שנמצא
foreach (var element in results)
{
    Console.WriteLine(element.InnerHtml);
}
