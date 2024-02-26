using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace practycode_22
{
    public class HtmlHelper
    {
        private static readonly HtmlHelper _htmlHelper = new HtmlHelper();

        public static HtmlHelper Helper { get { return _htmlHelper; } }

        public List<string> HtmlTags { get; set; }
        public List<string> HtmlVoidTags { get; set; }
       
        private List<string> ReadTagsFromFile(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);
                List<string> tags =JsonConvert.DeserializeObject<List<string>>(jsonContent);
                return tags;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading file '{filePath}': {ex.Message}");
                return new List<string>();
            }
        }

        private HtmlHelper()
        {
            HtmlTags = ReadTagsFromFile("json/HtmlTags.json");
            HtmlVoidTags = ReadTagsFromFile("json/HtmlVoidTags.json");
        }


        public bool IsValidHtmlTag(string tag)
        {
            return HtmlTags.Contains(tag);
        }

        public bool IsSelfClosingTag(string tag)
        {
            return HtmlVoidTags.Contains(tag);
        }
    }

}
