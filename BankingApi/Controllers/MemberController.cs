using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApi.Core.Domain;
using BankingApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: api/<MemberController>
        [HttpGet]
        public IActionResult Get()
        {
            var pagedMembers = _memberService.GetAllMembers();

            return Ok(pagedMembers);
        }

        // GET: api/<MemberController>
        [HttpGet]
        public IActionResult Get(int institutionId)
        {
            var pagedMembers = _memberService.GetAllMembersByInstitution(institutionId);

            return Ok(pagedMembers);
        }

        // GET: api/<MemberController>
        [HttpGet]
        public IActionResult GetMember(int memebrId)
        {
            var member = _memberService.GetMemberById(memebrId);

            return Ok(member);
        }

        // GET: api/<MemberController>
        [HttpPost]
        public async Task<IActionResult> Add(Member member, int institutionId)
        {
            await _memberService.AddMember(member, institutionId);

            return Ok(member);
        }

        // GET: api/<MemberController>
        [HttpDelete]
        public IActionResult Delete(int memebrId)
        {
            var member = _memberService.DeleteMember(memebrId);

            return Ok(member);
        }

        // GET: api/<MemberController>
        [HttpPut]
        public async Task<IActionResult> Delete(Member member)
        {
            await _memberService.UpdateMember(member);

            return Ok(member);
        }
    }
}
