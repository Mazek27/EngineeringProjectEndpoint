using System.Collections.Generic;
using System.Reflection;
using Engineering_Project.Models.Languages;
using Engineering_Project.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Remotion.Linq.Clauses;

namespace Engineering_Project.Service.Impement
{
    public class InternationalizationService : IInternationalizationService
    {
        
        public object SelectLanguage(string languages)
        {
            string language = getPrimaryLanguage(languages);

            if (Languages.DictionaryLanguage.ContainsKey(language))
            {
                return JsonConvert.DeserializeObject((string) Languages.DictionaryLanguage[language]);
            }
            else
            {
                return JsonConvert.DeserializeObject((string) Languages.DictionaryLanguage["en_US"]);
            }
                
        }

        private string getPrimaryLanguage(string languages)
        {
            string[] arrayLanguages = languages.Split(";");
            return arrayLanguages[0].Split(',')[0];
        }
    }
}