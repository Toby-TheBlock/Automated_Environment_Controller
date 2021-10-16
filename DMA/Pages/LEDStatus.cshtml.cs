using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Data_Monitoring_Application.Pages
{
    public class LEDStatusModel : PageModel
    {
        private DatabaseManager dbm = DatabaseManager.Singleton;

        public List<Dictionary<string, string>> LEDInformation { get; set; }
   
        public void OnGet()
        {
            LEDInformation = GetLEDInformation();
        }

        private List<Dictionary<string, string>> GetLEDInformation()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "TableName", "LED" },
                { "NumberOfRows", "100" }
            };

            return dbm.ConvertDataTableToDictionary(dbm.CallProcedureWithReturn(dbm.DbName, "SelectAllFromTable", parameters));
        }
    }
}
