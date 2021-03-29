using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Sat.Recruitment.Api.Services
{
    public class CsvRepositoryService : IRepositoryService
    {
        private readonly string _filePath;
        private readonly CsvConfiguration _csvConfiguration;

        public CsvRepositoryService(string filePath, CultureInfo cultureInfo, bool hasHeaderRecord)
        {
            _filePath = filePath;
            _csvConfiguration = new CsvConfiguration(cultureInfo)
            {
                HasHeaderRecord = hasHeaderRecord
            };
        }

        public IEnumerable<T> GetEntities<T>(Func<T, bool> predicate = null)
        {
            using var streamReader = new StreamReader(_filePath);
            using var csvReader = new CsvReader(streamReader, _csvConfiguration);
            csvReader.Context.RegisterClassMap<CsvUserMap>();

            var entities = csvReader.GetRecords<T>();
            if (predicate != null)
            {
                entities = entities.Where(predicate);
            }

            return entities.ToList();
        }

        public async Task CreateEntityAsync<T>(T entity)
        {
            using var stream = File.Open(_filePath, FileMode.Append);
            using var streamWriter = new StreamWriter(stream);
            using var csvWriter = new CsvWriter(streamWriter, _csvConfiguration);
            
            csvWriter.Context.RegisterClassMap<CsvUserMap>();

            csvWriter.WriteRecord(entity);

            await csvWriter.NextRecordAsync();
        }
    }
}
