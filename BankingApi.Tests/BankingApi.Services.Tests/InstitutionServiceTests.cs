using BankingApi.Core.Domain;
using BankingApi.Data;
using BankingApi.Services.Contracts;
using BankingApi.Services.Implementations;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.BankingApi.Services.Tests
{
    [TestFixture]
    public class InstitutionServiceTests
    {
        private IInstitutionDataService _institutionService;
        private Mock<IJsonRepository<Institution>> _institutionRepository;
        public void SetUp()
        {
            _institutionRepository = new Mock<IJsonRepository<Institution>>();
            _institutionService = new InstitutionDataService(_institutionRepository.Object);
        }

        public void Ensure_GetAllInstitutions_Works()
        {
            var expectedInstitutionsList = new List<Institution>();
            expectedInstitutionsList.Add(new Institution() { Id = 1, Name = "ABC Credit Union" });
            expectedInstitutionsList.Add(new Institution() { Id = 2, Name = "PQR Credit Union" });
            expectedInstitutionsList.Add(new Institution() { Id = 3, Name = "XYZ Credit Union" });
            expectedInstitutionsList.Add(new Institution() { Id = 5, Name = "RBG Credit Union" });

            _institutionRepository.Setup(m => m.GetAll()).Returns(expectedInstitutionsList);

            var actualInstitutionsList = _institutionService.GetAllInstitutions();

            Assert.AreEqual(expectedInstitutionsList, Is.EquivalentTo(actualInstitutionsList.Object));
        }

        public void Ensure_AddInstitution_Works()
        {
            var expectedInstitutionsList = new List<Institution>();
            expectedInstitutionsList.Add(new Institution() { Id = 1, Name = "ABC Credit Union" });
            expectedInstitutionsList.Add(new Institution() { Id = 2, Name = "PQR Credit Union" });
            expectedInstitutionsList.Add(new Institution() { Id = 3, Name = "XYZ Credit Union" });
            expectedInstitutionsList.Add(new Institution() { Id = 5, Name = "RBG Credit Union" });

            var newInstitution = new Institution() { Name = "ITP Credit Union" };
            _institutionRepository.Setup(m => m.Insert(newInstitution)).Callback(() => expectedInstitutionsList.Add(newInstitution));
            _institutionRepository.Setup(m => m.GetAll()).Returns(expectedInstitutionsList);

            _institutionService.AddInstitution(newInstitution);

            var actualInstitutionsList = _institutionService.GetAllInstitutions();

            Assert.AreEqual(expectedInstitutionsList, Is.EquivalentTo(actualInstitutionsList.Object));

            Assert.AreEqual(expectedInstitutionsList.Count, 6);

        }


    }
}
