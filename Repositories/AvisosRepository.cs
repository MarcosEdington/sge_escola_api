using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using sge_escola_api.Models;

namespace sge_escola_api.Repositories
{
    public class AvisosRepository
    {
        private readonly string _filePath = "Data/avisos.json";

        public AvisosRepository()
        {
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
        }

        public List<Aviso> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Aviso>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Aviso>>(json) ?? new List<Aviso>();
        }

        public void Save(Aviso aviso)
        {
            var avisos = GetAll();
            aviso.Id = avisos.Count > 0 ? avisos.Max(a => a.Id) + 1 : 1;
            avisos.Add(aviso);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(avisos, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void Update(Aviso avisoAtualizado)
        {
            var avisos = GetAll();
            var index = avisos.FindIndex(a => a.Id == avisoAtualizado.Id);
            if (index != -1)
            {
                avisos[index] = avisoAtualizado;
                File.WriteAllText(_filePath, JsonSerializer.Serialize(avisos, new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        public void Delete(int id)
        {
            var avisos = GetAll();
            var aviso = avisos.FirstOrDefault(a => a.Id == id);
            if (aviso != null)
            {
                avisos.Remove(aviso);
                File.WriteAllText(_filePath, JsonSerializer.Serialize(avisos, new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}