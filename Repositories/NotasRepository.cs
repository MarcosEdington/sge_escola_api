using System.Text.Json;
using sge_escola_api.Models;

namespace sge_escola_api.Repositories
{
    public class NotasRepository
    {
        private readonly string _filePath = "Data/notas.json";

        public List<Nota> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Nota>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Nota>>(json) ?? new List<Nota>();
        }

        


        public void SaveMassa(List<Nota> novasNotas)
        {
            var notasExistentes = GetAll();

            foreach (var nota in novasNotas)
            {
                // O erro estava aqui: ele encontrava pelo AlunoId E TurmaId, 
                // mas se o AlunoId estivesse trocado, ele sobrescrevia o aluno errado!
                var index = notasExistentes.FindIndex(n =>
                    n.AlunoId == nota.AlunoId &&
                    n.TurmaId == nota.TurmaId &&
                    n.Disciplina == nota.Disciplina);

                if (index != -1)
                {
                    nota.Id = notasExistentes[index].Id;
                    notasExistentes[index] = nota;
                }
                else
                {
                    // Gera um ID novo e seguro
                    nota.Id = notasExistentes.Any() ? notasExistentes.Max(n => n.Id) + 1 : 1;
                    notasExistentes.Add(nota);
                }
            }
            File.WriteAllText(_filePath, JsonSerializer.Serialize(notasExistentes, new JsonSerializerOptions { WriteIndented = true }));
        }


    }
}