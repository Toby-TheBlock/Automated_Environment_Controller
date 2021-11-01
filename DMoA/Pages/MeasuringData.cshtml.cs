using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Data_Monitoring_Application.Pages
{
    public class MeasuringDataModel : PageModel
    {
        private DatabaseManager dbm = DatabaseManager.Singleton;

        public List<Dictionary<string, string>> MeasuringData { get; set; }

        public void OnGet()
        {
            MeasuringData = GetMeasuringData();
        }

        private List<Dictionary<string, string>> GetMeasuringData()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "ViewName", "GraphData" },
                { "NumberOfRows", "1000" }
            };

            return dbm.ConvertDataTableToDictionary(dbm.CallProcedureWithReturn(dbm.DbName, "SelectAllFromView", parameters));
        }
    }
}
