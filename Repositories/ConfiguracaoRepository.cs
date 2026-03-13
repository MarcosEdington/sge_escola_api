using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using sge_escola_api.Models;

namespace sge_escola_api.Repositories
{
    public class ConfiguracaoRepository
    {
        private readonly string _filePath = "Data/configuracoes.json";

        public ConfiguracaoRepository()
        {
            // Garante que a pasta Data existe
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
        }

        public List<ConfiguracaoHorario> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<ConfiguracaoHorario>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<ConfiguracaoHorario>>(json) ?? new List<ConfiguracaoHorario>();
        }

        public void SaveAll(List<ConfiguracaoHorario> horarios)
        {
            // Serializa a lista inteira com indentação (formatação bonita no JSON)
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(horarios, options);

            File.WriteAllText(_filePath, jsonString);
        }
    }
}