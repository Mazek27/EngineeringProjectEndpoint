using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Engineering_Project.Models.Domian;
using Engineering_Project.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class GpxFileManager
{
    public static void DecodeGpxFile(IFormFile file)
    {
//        XDocument gpxDoc = XDocument.Load(new StreamReader(file.OpenReadStream()));
//        gpxDoc.Load(file);
        
        
        XmlDocument gpxDoc = new XmlDocument();
        gpxDoc.Load(new StreamReader(file.OpenReadStream()));
        
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(gpxDoc.NameTable);
        nsmgr.AddNamespace("x", "http://www.topografix.com/GPX/1/1");            
        XmlNodeList nl = gpxDoc.SelectNodes("//x:trkpt", nsmgr);

        TrainingDetail trainingDetail = new TrainingDetail();

        DateTimeOffset? dto = null;
        for (int i = 0; i < nl.Count; i++)
        {
            trainingDetail.Localizations.Add(new GeoCoordinate
            {
                Lat = double.Parse(nl[i].Attributes["lat"].InnerText),
                Lng = double.Parse(nl[i].Attributes["lon"].InnerText),
                Measurement = DateTimeOffset.Parse(nl[i]["time"].InnerText).DateTime
            });
        }

        trainingDetail.TrainingTime = trainingDetail.Localizations.First().Measurement;
        trainingDetail.Type = (TrainingType)Enum.Parse(typeof(TrainingType), gpxDoc.SelectSingleNode("//x:type", nsmgr).InnerText);
        trainingDetail.Duration =
            (trainingDetail.Localizations.Last().Measurement - trainingDetail.Localizations.First().Measurement)
            .TotalSeconds;
//        trainingDetail.
        
        
        
        
        var x = 0;
    }
}