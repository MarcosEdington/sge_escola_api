using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using sge_escola_api.Models;

namespace sge_escola_api.Repositories
{
    public class CalendarioRepository
    {
        private readonly string _filePath = "Data/calendario.json";

        public CalendarioRepository()
        {
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
        }

        public List<EventoCalendario> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<EventoCalendario>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<EventoCalendario>>(json) ?? new List<EventoCalendario>();
        }

        public void Save(EventoCalendario evento)
        {
            var eventos = GetAll();
            if (evento.Id == 0)
            {
                evento.Id = eventos.Count > 0 ? eventos.Max(e => e.Id) + 1 : 1;
                eventos.Add(evento);
            }
            else
            {
                var index = eventos.FindIndex(e => e.Id == evento.Id);
                if (index != -1) eventos[index] = evento;
            }
            File.WriteAllText(_filePath, JsonSerializer.Serialize(eventos, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void Delete(int id)
        {
            var eventos = GetAll();
            var evento = eventos.FirstOrDefault(e => e.Id == id);
            if (evento != null)
            {
                eventos.Remove(evento);
                File.WriteAllText(_filePath, JsonSerializer.Serialize(eventos, new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}