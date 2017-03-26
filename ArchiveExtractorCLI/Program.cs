﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using ArchiveExtractorBusinessCode;
using System.Security.Permissions;

namespace ArchiveExtractorCLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var archiveLocation = "";
            var extractDestination = "";

            try
            {
                archiveLocation = args[0];
                extractDestination = args[1];
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Usage: ArchiveExtractor 'ArchiveToExtract' 'TargetLocation'");
            }

            Console.WriteLine("Extracting Zip...");
            Archive.ExtractArchive(archiveLocation, extractDestination);
            Console.WriteLine("Done");
            var xml = File.ReadAllText(extractDestination + "/imsmanifest.xml");
            XElement manifest = XElement.Parse(xml);

            Directory.Delete(extractDestination,true);
            List<XElement> xele = ManifestParser.GetOrganizationElements(manifest);

            foreach (XElement x in xele)
            {
                System.Console.WriteLine(x);
            }
            System.Console.ReadKey();
        }
    }
}
