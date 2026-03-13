using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using sge_escola_api.Models;

namespace sge_escola_api.Repositories
{
    public class FrequenciaRepository
    {
        private readonly string _filePath = "Data/frequencias.json";
        private List<Frequencia> _cache; // Nova lista em memória

        public FrequenciaRepository()
        {
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
            _cache = GetAll(); // Carrega tudo para a memória assim que instanciar
        }

        public List<Frequencia> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Frequencia>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Frequencia>>(json) ?? new List<Frequencia>();
        }

        // Novo método: Apenas altera a lista na memória
        public void AdicionarSemSalvar(Frequencia frequencia)
        {
            var registroExistente = _cache.FirstOrDefault(f =>
                f.AlunoId == frequencia.AlunoId &&
                f.TurmaId == frequencia.TurmaId &&
                f.Data.Date == frequencia.Data.Date);

            if (registroExistente != null)
            {
                registroExistente.Presente = frequencia.Presente;
            }
            else
            {
                frequencia.Id = _cache.Count > 0 ? _cache.Max(a => a.Id) + 1 : 1;
                _cache.Add(frequencia);
            }
        }

        // Novo método: Grava o estado atual da memória no arquivo uma única vez
        public void SalvarNoDisco()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(_cache, options));
        }
    }
}