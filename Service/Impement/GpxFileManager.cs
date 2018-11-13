using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Engineering_Project.Models.Domian;
using Engineering_Project.Models.Domian.Workout;
using Engineering_Project.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class GpxFileManager
{
    public static WorkoutDomain DecodeGpxFile(IFormFile file)
    {
        XmlDocument gpxDoc = new XmlDocument();
        gpxDoc.Load(new StreamReader(file.OpenReadStream()));
        
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(gpxDoc.NameTable);
        nsmgr.AddNamespace("x", "http://www.topografix.com/GPX/1/1");            
        XmlNodeList nl = gpxDoc.SelectNodes("//x:trkpt", nsmgr);

        WorkoutDomain workoutDomain = new WorkoutDomain();

        DateTimeOffset? dto = null;
        
        for (int i = 0; i < nl.Count; i++)
        {
            workoutDomain.Localizations.Add(new Coordinate(
                    double.Parse(nl[i].Attributes["lat"].InnerText, CultureInfo.InvariantCulture),
                    double.Parse(nl[i].Attributes["lon"].InnerText, CultureInfo.InvariantCulture),
                    DateTimeOffset.Parse(nl[i]["time"].InnerText).DateTime
                )
            );
        }

        workoutDomain.TrainingTime = workoutDomain.Localizations.First().Measurement;
        workoutDomain.Type = ExtractTrainingDetailType(gpxDoc, nsmgr);
        var duration = ExtractTrainingDetailDuration(workoutDomain);
        var distance = CalculateDistance(workoutDomain);
        workoutDomain.WorkoutDetail = new WorkoutDetail(distance, duration);

        return workoutDomain;
    }

    private static double CalculateDistance(WorkoutDomain workoutDomain)
    {
        double distance = default;
        for (int i = 1; i < workoutDomain.Localizations.Count; i++)
        {
            distance += workoutDomain.Localizations[i].DistanceTo(workoutDomain.Localizations[i - 1]);
        }

        return distance;
    }

    private static double ExtractTrainingDetailDuration(WorkoutDomain workoutDomain)
    {
        return (workoutDomain.Localizations.Last().Measurement - workoutDomain.Localizations.First().Measurement)
            .TotalSeconds;
    }

    private static TrainingType ExtractTrainingDetailType(XmlDocument gpxDoc, XmlNamespaceManager nsmgr)
    {
        return (TrainingType)Enum.Parse(typeof(TrainingType), gpxDoc.SelectSingleNode("//x:type", nsmgr).InnerText);
    }
}