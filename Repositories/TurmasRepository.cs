using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using sge_escola_api.Models;

namespace sge_escola_api.Repositories
{
    public class TurmasRepository
    {
        private readonly string _filePath = "Data/turmas.json";

        public TurmasRepository()
        {
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
        }

        public List<Turma> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Turma>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Turma>>(json) ?? new List<Turma>();
        }

        public void Save(Turma turma)
        {
            var turmas = GetAll();
            turma.Id = turmas.Count > 0 ? turmas.Max(t => t.Id) + 1 : 1;
            turmas.Add(turma);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(turmas, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void Update(Turma turmaAtualizada)
        {
            var turmas = GetAll();
            var index = turmas.FindIndex(t => t.Id == turmaAtualizada.Id);
            if (index != -1)
            {
                turmas[index] = turmaAtualizada;
                File.WriteAllText(_filePath, JsonSerializer.Serialize(turmas, new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        public void Delete(int id)
        {
            var turmas = GetAll();
            var turma = turmas.FirstOrDefault(t => t.Id == id);
            if (turma != null)
            {
                turmas.Remove(turma);
                File.WriteAllText(_filePath, JsonSerializer.Serialize(turmas, new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}