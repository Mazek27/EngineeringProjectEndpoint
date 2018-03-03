using System.Collections.Generic;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using Microsoft.Net.Http.Headers;

namespace Engineering_Project.Models.Domian
{
    public class DayData
    {
        public char Type { get; set; }

        public List<Training> TrainingList { get; set; }
    }
}