using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using sge_escola_api.Models;

namespace sge_escola_api.Repositories
{
    public class MatriculaRepository
    {
        private readonly string _filePath = "Data/matriculas.json";

        public MatriculaRepository()
        {
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
        }

        public List<Matricula> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Matricula>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Matricula>>(json) ?? new List<Matricula>();
        }

        public void Save(Matricula matricula)
        {
            var matriculas = GetAll();
            matricula.Id = matriculas.Count > 0 ? matriculas.Max(m => m.Id) + 1 : 1;
            matriculas.Add(matricula);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(matriculas, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}