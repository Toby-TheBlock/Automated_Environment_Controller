using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Data_Monitoring_Application.Pages
{
    public class IndexModel : PageModel
    {
        private DatabaseManager dbm = DatabaseManager.Singleton;

        public List<Dictionary<string, string>> CurrentDataAndThreshold { get; set; }

        public void OnGet()
        {
            CurrentDataAndThreshold = GetCurrentDataAndThreshold();
        }

        private List<Dictionary<string, string>> GetCurrentDataAndThreshold()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "ViewName", "CurrentDataAndThreshold" },
                { "NumberOfRows", "1000" }
            };

            return dbm.ConvertDataTableToDictionary(dbm.CallProcedureWithReturn(dbm.DbName, "SelectAllFromView", parameters));
        }
    }
}
