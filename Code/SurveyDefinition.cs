using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace surveyjs_aspnet_mvc
{

    [Serializable]
    public class SurveyDefinition
    {
        public string id { get; set; }
        public string name { get; set; }
        public string json { get; set; }

        private static int currentId = 3;
        public static SurveyDefinition Create()
        {
            return new SurveyDefinition { id = "" + currentId, name = "New Survey " + currentId++, json = "{}" };
        }
        public static SurveyDefinition FindById(IEnumerable<SurveyDefinition> collection, string id)
        {
            return collection.Where(s => s.id == id).FirstOrDefault();
        }
        /// <summary>
        /// https://jsonformatter.org/json-editor
        /// https://jsonformatter.curiousconcept.com/#
        /// </summary>
        /// <returns></returns>
        public static List<SurveyDefinition> GetDefaultSurveys()
        {

            //iterate survey files
            var filepaths = Directory.GetFiles(@"./Surveys", "*.json")
                .ToList();
            int counter = 0;

            List<SurveyDefinition> defaultSurveys = new List<SurveyDefinition>();

            foreach (var filepath in filepaths)
            {
                SurveyDefinition survey = new SurveyDefinition
                {
                    id = counter.ToString(),
                    name = Path.GetFileNameWithoutExtension(filepath),// "Product Feedback Survey",
                    json = File.ReadAllText(filepath)
                };

                defaultSurveys.Add(survey);
                counter++;
            }

            return defaultSurveys;
        }
    }

}