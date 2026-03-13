using System.Text.Json;
using sge_escola_api.Models;

namespace sge_escola_api.Repositories
{
    public class DisciplinaRepository
    {
        private readonly string _filePath = "Data/disciplinas.json";

        public DisciplinaRepository()
        {
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
        }

        public List<Disciplina> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Disciplina>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Disciplina>>(json) ?? new List<Disciplina>();
        }

        public void Save(Disciplina disciplina)
        {
            var disciplinas = GetAll();
            disciplina.Id = disciplinas.Count > 0 ? disciplinas.Max(d => d.Id) + 1 : 1;
            disciplinas.Add(disciplina);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(disciplinas, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void Delete(int id)
        {
            var disciplinas = GetAll();
            var disciplina = disciplinas.FirstOrDefault(d => d.Id == id);
            if (disciplina != null)
            {
                disciplinas.Remove(disciplina);
                File.WriteAllText(_filePath, JsonSerializer.Serialize(disciplinas, new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}