
using Load.Mapper.RowFormat;

namespace Load.Mapper
{
    public abstract class AMapper
    {
        protected int _rowsCount;
        protected int _colsCount;
        protected object[,] _rawData;

        protected abstract FormatRow ConvertTo(ref object[,] targetData, int index);
        public abstract void Map();
        public abstract FormatRow[] GetMappedData();
    }
}
