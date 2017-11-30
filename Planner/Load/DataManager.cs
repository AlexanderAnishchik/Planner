using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Load.Mapper.RowFormat;
using Load.Services;

namespace Load
{
    public class DataManager
    {
        private readonly ImportDataService _importService = new ImportDataService();
        private ProcessDataService _processService;
        private DataPreparer _dataPreparer;

        public void UploadEntryFile(string pathToFile)
        {
            _importService.Import(pathToFile);

            _processService = new ProcessDataService(
                _importService.GetDayFormatRowsAsArray(),
                _importService.GetExtraFormatRowsAsArray());

            _processService.Process();

            _dataPreparer = new DataPreparer(
                _importService.GetDayFormatRowsAsList(),
                _importService.GetExtraFormatRowsAsList(),
                _processService.FirstDaySemesterLoad,
                _processService.SecondDaySemesterLoad,
                _processService.FirstExtraSemesterLoad,
                _processService.SecondExtraSemesterLoad);

            _dataPreparer.MergeEntryLoad(new LoadingList() {Year = DateTime.Now.Year, Comment = pathToFile/*, DepartmentId = "2ed97bc7-932e-4683-ab76-e1890d3bbe2d"*/});
            }
    }
}
