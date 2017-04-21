﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ArchiveExtractorBusinessCode.Resources
{
    class TextResource : IBlackBoardResource
    {
        public string RefId { get; set; }
        public string Text { get; set; }
        public string PathToResourceFile { get; set; }

        public TextResource(string text, string refId)
        {
            RefId = refId;
            Text = text;
        }

        public TextResource(string PathToResourceFile)
        {
            string xml = File.ReadAllText(PathToResourceFile);
            XElement xele = XElement.Parse(xml);
            if (xele.Descendants("TEXT").Any())
            {
                List<XElement> texts = xele.Descendants("TEXT").ToList();
                this.Text = texts[0].Value;
            }
            else
            {
                this.Text = "";
            }
            this.RefId = Path.GetFileNameWithoutExtension(PathToResourceFile);
            this.PathToResourceFile = PathToResourceFile;

            if (xele.Descendants("URL").Any())
            {
                List<XElement> urls = xele.Descendants("URL").ToList();
                for(var i = 0; i < urls.Count; i++)
                {
                    string val = urls[i].Attribute("value").Value;
                    if (val.Length != 0)
                    {
                        ParseLinks parser = new ParseLinks(val);
                        Console.WriteLine(parser.Uri);
                        if (parser.IsAbsoluteUri())
                        {
                            Console.WriteLine($"Length:{val.Length} Ref {this.RefId} CONTAINS URL: '{val}'");
                            var isAlive = parser.RequestUrl();
                            Console.WriteLine(PathToResourceFile, "is alive:", isAlive);
                            if (!isAlive)
                            {
                                Console.WriteLine(val, "For resource", RefId, "is not alive.");
                                // Remove element if considered dead
                                //xele.Descendants("URL").Where(x => x.Value == target).Remove();
                            }
                        }
                    }
                }
            }
        }
    }
}
