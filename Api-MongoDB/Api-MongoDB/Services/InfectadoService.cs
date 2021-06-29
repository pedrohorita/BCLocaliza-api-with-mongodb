using Api_MongoDB.Data.Collections;
using Api_MongoDB.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Api_MongoDB.Services
{
    public class InfectadoService
    {
        private readonly Data.MongoDB _mongoDB;
        private readonly IMongoCollection<Infectado> _infectadoCollection;

        public InfectadoService(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadoCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }


        public List<Infectado> Obter() =>
            _infectadoCollection.Find(Builders<Infectado>.Filter.Empty).ToList();

        public Infectado Obter(string Id) =>
            _infectadoCollection.Find<Infectado>(i => i.Id == Id).FirstOrDefault();

        public Infectado Salvar(InfectadoDto dto)
        {
            var infectado = new Infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _infectadoCollection.InsertOne(infectado);

            return infectado;
        }

        public Infectado Atualizar(string id, InfectadoDto dto)
        {
            var infectado = new Infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _infectadoCollection.ReplaceOne(Builders<Infectado>.Filter.Where(w => w.Id == id), infectado);

            return infectado;
        }

        public void Excluir(string id)
        {
            _infectadoCollection.DeleteOne(Builders<Infectado>.Filter.Where(w => w.Id == id));
        }
    }
}
