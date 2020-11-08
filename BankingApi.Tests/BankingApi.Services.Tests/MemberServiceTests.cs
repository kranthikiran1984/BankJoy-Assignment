using BankingApi.Core.Domain;
using BankingApi.Data;
using BankingApi.Services.Contracts;
using BankingApi.Services.Implementations;
using BankingApi.Services.Validations;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApi.BankingApi.Services.Tests
{
    [TestFixture]
    public class MemberServiceTests
    {
        private IMemberService _memberService;
        private Mock<IJsonRepository<Member>> _memberRepository;
        private MemberValidator _memberValidator;

        [SetUp]
        public void SetUp()
        {
            _memberRepository = new Mock<IJsonRepository<Member>>();
            _memberValidator = new MemberValidator();
            _memberService = new MemberService(_memberRepository.Object, _memberValidator);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void Ensure_GetMemebersByInstitution_Works(int institutionId)
        {
            var allmembers = new List<Member>();
            allmembers.Add(new Member() { Id = 1, GivenName = "John", SurName = "Doe", InstitutionId = 1 });
            allmembers.Add(new Member() { Id = 2, GivenName = "Jane", SurName = "Doe", InstitutionId = 2 });
            allmembers.Add(new Member() { Id = 3, GivenName = "Dan", SurName = "Rather", InstitutionId = 1 });

            _memberRepository.Setup(x => x.GetAll()).Returns(allmembers);

            var actualMembers = _memberService.GetAllMembersByInstitution(institutionId);
            var expectedMembers = allmembers.Where(x => x.InstitutionId == institutionId).ToList();

            Assert.AreEqual(expectedMembers, Is.EquivalentTo(actualMembers.Object));
        }

        [TestCase(1)]
        [TestCase(2)]
        public void Ensure_AddMember_Works(int institutionId)
        {
            var allMembers = new List<Member>();
            
            var newMember = new Member() { Id = 3, GivenName = "Dan", SurName = "Rather", InstitutionId = institutionId };

            _memberRepository.Setup(m => m.Insert(newMember)).Callback(() => allMembers.Add(newMember));
            _memberRepository.Setup(m => m.GetAll()).Returns(allMembers);

            _memberService.AddMember(newMember, institutionId);

            var actualMembers = _memberService.GetAllMembersByInstitution(institutionId);
            var expectedMembers = allMembers.Where(x => x.InstitutionId == institutionId).ToList();

            Assert.AreEqual(actualMembers, Is.EquivalentTo(expectedMembers));
            Assert.AreEqual(expectedMembers.Count, 1);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void Ensure_DeleteMember_Works(int memberId)
        {
            var allmembers = new List<Member>();
            allmembers.Add(new Member() { Id = 1, GivenName = "John", SurName = "Doe", InstitutionId = 1 });
            allmembers.Add(new Member() { Id = 2, GivenName = "Jane", SurName = "Doe", InstitutionId = 2 });
            allmembers.Add(new Member() { Id = 3, GivenName = "Dan", SurName = "Rather", InstitutionId = 1 });

            _memberRepository.Setup(x => x.GetAll()).Returns(allmembers);
            _memberRepository.Setup(x => x.Delete(memberId)).Callback(() => allmembers.Remove(allmembers.FirstOrDefault(z => z.Id == memberId)));

            var membersActualCount = allmembers.Count;
            _memberService.DeleteMember(memberId).GetAwaiter().GetResult();

            var expectNull = _memberService.GetMemberById(memberId);
            Assert.That(membersActualCount - 1 == allmembers.Count);
            Assert.IsNull(expectNull);
        }

        [TestCaseSource("UpdateMembersGreaterTestCases")]
        public void Ensure_UpdateMember_Works(Member member)
        {
            var allmembers = new List<Member>();
            allmembers.Add(new Member() { Id = 1, GivenName = "John", SurName = "Doe", InstitutionId = 1 });
            allmembers.Add(new Member() { Id = 2, GivenName = "Jane", SurName = "Doe", InstitutionId = 2 });
            allmembers.Add(new Member() { Id = 3, GivenName = "Dan", SurName = "Rather", InstitutionId = 1 });

            _memberRepository.Setup(x => x.GetAll()).Returns(allmembers);
            _memberRepository.Setup(x => x.Update(member)).Callback(() => { if (member.Id > 0) { allmembers.Remove(allmembers.FirstOrDefault(x => x.Id == member.Id)); allmembers.Add(member); } });
            _memberRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => allmembers.FirstOrDefault(x => x.Id == id));

            _memberService.UpdateMember(member).GetAwaiter().GetResult();

            var actualMember = _memberService.GetMemberById(member.Id);

            Assert.Equals(actualMember, member);
        }

        private static IEnumerable<TestCaseData> UpdateMembersGreaterTestCases
        {
            //
            get
            {
                yield return new TestCaseData(new Member() { Id = 1, GivenName = "Peter", SurName = "Doe", InstitutionId = 1 });
                yield return new TestCaseData(new Member() { Id = 2, GivenName = "Peter", SurName = "Doe", InstitutionId = 1 });
                yield return new TestCaseData(new Member() { Id = 3, GivenName = "Peter", SurName = "Doe", InstitutionId = 1 });
                yield return new TestCaseData(new Member() { Id = 0, GivenName = "Peter", SurName = "Doe", InstitutionId = 1 });
            }
        }
    }
}
