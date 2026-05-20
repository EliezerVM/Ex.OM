using System.Collections.Generic;
using System.Linq;
using static Ex.OM.Extentions.TextBoxFieldAssistOM;

namespace Ex.OM.Models
{
    public class Format
    {
        public PositionSymbol PositionSymbol { get; set; } = PositionSymbol.BeforeSymbol;
        
        public Format Load(string parameters)
        {
            var parameter = parameters.Split(',');

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            foreach (var item in parameter)
            {
                var split = item.Split(':');
                keyValuePairs.Add(split[0], split[1]);  
            }

            PositionSymbol = keyValuePairs.Where(x => x.Key == "PositionSymbol").Select(x=> 
            {
               return  x.Value == PositionSymbol.BeforeSymbol.ToString() ? PositionSymbol.BeforeSymbol : PositionSymbol.AfterSymbol;
            }).FirstOrDefault();

            return this;

        }
    }
}
