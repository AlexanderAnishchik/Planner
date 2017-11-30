using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Load.Mapper;
using Load.Mapper.RowFormat;
using Load.Reader;

namespace Load.Services
{
    public class ImportDataService
    {
        private Dictionary<EntryFormatType, object[,]> _entryDataDictionary;
        private AMapper _mapper;
        private readonly FormatMapper _asyncMapper = new FormatMapper();
        private DayFormatRow[] _mappedDayFormat;
        private ExtraFormatRow[] _mappedExtraFormat;

        public void Import(string pathToFile)
        {
            using (var reader = new InteropReader())
            {
                _entryDataDictionary = reader.Extract(pathToFile);
            }

            _mapper = new DayFormatMapper(_entryDataDictionary[EntryFormatType.Day]);
            _mapper.Map();
            _mappedDayFormat = (DayFormatRow[])_mapper.GetMappedData();

            _mapper = new ExtraFormatMapper(_entryDataDictionary[EntryFormatType.Extra]);
            _mapper.Map();
            _mappedExtraFormat = (ExtraFormatRow[])_mapper.GetMappedData();
        }

        public async Task<Tuple<List<DayFormatRow>, List<ExtraFormatRow>>> ImportAsync(string pathToFile)
        {
            List<DayFormatRow> dayFormatRows = new List<DayFormatRow>();
            List<ExtraFormatRow> extraFormatRows = new List<ExtraFormatRow>();

            try
            {
                Dictionary<EntryFormatType, object[,]> rawDataDictionary;
                using (InteropReader reader = new InteropReader())
                    rawDataDictionary = await reader.ExtractAsync(pathToFile);

                dayFormatRows = await GetMappedDayFormatRowsAsync(rawDataDictionary[EntryFormatType.Day]);
                extraFormatRows = await GetMappedExtraFormatRowsAsync(rawDataDictionary[EntryFormatType.Extra]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return Tuple.Create(dayFormatRows, extraFormatRows);
        }
        private async Task<List<DayFormatRow>> GetMappedDayFormatRowsAsync(object[,] rawDataObjects)
        {
            return await _asyncMapper.ToDayFormat(rawDataObjects);
        }
        private async Task<List<ExtraFormatRow>> GetMappedExtraFormatRowsAsync(object[,] rawDataObjects)
        {
            return await _asyncMapper.ToExtraFormat(rawDataObjects);
        }

        public DayFormatRow[] GetDayFormatRowsAsArray()
        {
            return _mappedDayFormat;
        }
        public ExtraFormatRow[] GetExtraFormatRowsAsArray()
        {
            return _mappedExtraFormat;
        }
        public List<DayFormatRow> GetDayFormatRowsAsList()
        {
            return _mappedDayFormat.ToList();
        }
        public List<ExtraFormatRow> GetExtraFormatRowsAsList()
        {
            return _mappedExtraFormat.ToList();
        }
    }
}
