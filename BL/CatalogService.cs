﻿using System.Reflection;
using System.Text.Json.Nodes;
using ZelekWieclaw.VisualProgrammingProject.Interfaces;


namespace ZelekWieclaw.VisualProgrammingProject.BL
{
    public class CatalogService // Products and Producers separated or together?
    {
        public CatalogService()
        {
            _dao = LoadDao();
        }

        public IEnumerable<IBeerProduct> GetAllBeerProducts()
        {
            return _dao.GetAllBeerProducts();
        }

        public IEnumerable<IBeerProducer> GetAllBeerProducers()
        {
            return _dao.GetAllBeerProducers();
        }

        public void UpdateBeerProducer(IBeerProducer producer)
        {
            _dao.UpdateBeerProducer(producer);
        }

        public IBeerProducer GetProducerById(int id)
        {
            return _dao.GetBeerProducerById(id);
        }

        public void DeleteBeerProducer(int id)
        {
            _dao.DeleteBeerProducer(id);
        }

        private readonly IDataAccessObject _dao;

        private IDataAccessObject LoadDao()
        {
            var jsonFilePath = "C:\\Users\\pc\\source\\repos\\bartosz-zelek\\.NETVisualProgramming\\BL\\jsconfig1.json";
            var json = File.ReadAllText(jsonFilePath);
            var jsonNode = JsonNode.Parse(json);
            var configuration = jsonNode?["DAOpath"];
            if (configuration == null)
            {
                throw new InvalidOperationException("DAOpath configuration is missing in the JSON file.");
            }

            var pathToDll = configuration.ToString();

            var assembly = Assembly.UnsafeLoadFrom(pathToDll);

            var type = assembly.GetTypes().FirstOrDefault(t => typeof(IDataAccessObject).IsAssignableFrom(t));
            if (type == null)
            {
                throw new InvalidOperationException(
                    "The assembly does not contain a class implementing IDataAccessObject.");
            }

            var instanceProperty = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);

            if (instanceProperty == null)
            {
                throw new InvalidOperationException("The class does not contain a static Instance property.");
            }

            var dao = instanceProperty.GetValue(null) as IDataAccessObject;

            if (dao == null)
            {
                throw new InvalidOperationException("Failed to get the instance of IDataAccessObject.");
            }

            return dao;
        }
    }
}