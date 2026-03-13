using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using sge_escola_api.Models;

namespace sge_escola_api.Repositories
{
    public class AlunoRepository
    {
        private readonly string _filePath = "Data/alunos.json";

        public AlunoRepository()
        {
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
        }

        public List<Aluno> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Aluno>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Aluno>>(json) ?? new List<Aluno>();
        }

        public void Save(Aluno aluno)
        {
            var alunos = GetAll();
            aluno.Id = alunos.Count > 0 ? alunos.Max(a => a.Id) + 1 : 1;
            alunos.Add(aluno);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(alunos, new JsonSerializerOptions { WriteIndented = true }));
        }

        // NOVO MÉTODO: Atualizar
        public void Update(Aluno alunoAtualizado)
        {
            var alunos = GetAll();
            var index = alunos.FindIndex(a => a.Id == alunoAtualizado.Id);
            if (index != -1)
            {
                alunos[index] = alunoAtualizado;
                File.WriteAllText(_filePath, JsonSerializer.Serialize(alunos, new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        // NOVO MÉTODO: Deletar
        public void Delete(int id)
        {
            var alunos = GetAll();
            var aluno = alunos.FirstOrDefault(a => a.Id == id);
            if (aluno != null)
            {
                alunos.Remove(aluno);
                File.WriteAllText(_filePath, JsonSerializer.Serialize(alunos, new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}