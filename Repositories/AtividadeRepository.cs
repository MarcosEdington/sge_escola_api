using System.Text.Json;
using sge_escola_api.Models;

namespace sge_escola_api.Repositories
{
    public class AtividadeRepository
    {
        private readonly string _filePath = "Data/atividades.json";

        public List<Atividade> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Atividade>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Atividade>>(json) ?? new List<Atividade>();
        }

        public void Save(Atividade novaAtividade)
        {
            var atividades = GetAll();

            if (novaAtividade.Id == 0)
            {
                novaAtividade.Id = atividades.Count > 0 ? atividades.Max(a => a.Id) + 1 : 1;
                atividades.Add(novaAtividade);
            }
            else
            {
                var index = atividades.FindIndex(a => a.Id == novaAtividade.Id);
                if (index != -1) atividades[index] = novaAtividade;
            }

            File.WriteAllText(_filePath, JsonSerializer.Serialize(atividades, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void Delete(int id)
        {
            var atividades = GetAll();
            var atividade = atividades.FirstOrDefault(a => a.Id == id);
            if (atividade != null)
            {
                atividades.Remove(atividade);
                File.WriteAllText(_filePath, JsonSerializer.Serialize(atividades, new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}