using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using sge_escola_api.Models;

namespace sge_escola_api.Repositories
{
    public class MensalidadeRepository
    {
        private readonly string _filePath = "Data/mensalidades.json";

        public MensalidadeRepository()
        {
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
        }

        public List<Mensalidade> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Mensalidade>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Mensalidade>>(json) ?? new List<Mensalidade>();
        }

        private void SaveAll(List<Mensalidade> mensalidades)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(mensalidades, options));
        }

        public void Adicionar(Mensalidade mensalidade)
        {
            var lista = GetAll();
            mensalidade.Id = lista.Count > 0 ? lista.Max(m => m.Id) + 1 : 1;
            lista.Add(mensalidade);
            SaveAll(lista);
        }

        public void AtualizarStatus(int id, string novoStatus, DateTime? dataPagamento = null)
        {
            var lista = GetAll();
            var item = lista.FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                item.Status = novoStatus;
                if (dataPagamento.HasValue) item.DataPagamento = dataPagamento;
                SaveAll(lista);
            }
        }

        public List<Mensalidade> GetPorAluno(int alunoId)
        {
            return GetAll().Where(m => m.AlunoId == alunoId).ToList();
        }
    }
}