using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using sge_escola_api.Models;

namespace sge_escola_api.Repositories
{
    public class ProfessoresRepository
    {
        private readonly string _filePath = "Data/professores.json";

        public ProfessoresRepository()
        {
            // Garante que a pasta Data existe
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
        }

        public List<Professor> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Professor>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Professor>>(json) ?? new List<Professor>();
        }

        private void SaveAll(List<Professor> professores)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(professores, options));
        }

        public void Add(Professor professor)
        {
            var professores = GetAll();
            // Lógica de ID autoincremento baseada no arquivo
            professor.Id = professores.Count > 0 ? professores.Max(p => p.Id) + 1 : 1;
            professores.Add(professor);
            SaveAll(professores);
        }

        public void Update(int id, Professor professor)
        {
            var professores = GetAll();
            var index = professores.FindIndex(p => p.Id == id);
            if (index != -1)
            {
                professor.Id = id; // Garante que o ID não mude
                professores[index] = professor;
                SaveAll(professores);
            }
        }

        public void Delete(int id)
        {
            var professores = GetAll();
            var novoLista = professores.Where(p => p.Id != id).ToList();
            SaveAll(novoLista);
        }
    }
}