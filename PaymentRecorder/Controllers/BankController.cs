using AutoMapper;
using Common.Dto;
using Common.Models;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using System.Net;
using PaymentRecorder.Controllers.Base;

namespace PaymentRecorder.Controllers
{
    [Route($"/api/{nameof(Bank)}")]
    public class BankController : AppBaseController
    {
        private readonly IBankService _bankService;

        public BankController(IMapper mapper, IBankService bankService) : base(mapper)
        {
            _bankService = bankService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id:long}")]
        public async Task<ActionResult<BankModel>> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            var entity = await _bankService.GetByIdAsNoTrackingAsync(id, cancellationToken);


            Response.Headers.ETag = entity.Version.ToString();
            return Ok(Mapper.Map<BankModel>(entity));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("get-by-bank-code/{bankCode}")]
        public async Task<ActionResult<BankModel>> GetByCodeAsync(string bankCode, CancellationToken cancellationToken)
        {
            var entity = await _bankService.GetByCodeAsync(bankCode, cancellationToken);

            Response.Headers.ETag = entity.Version.ToString();
            return Ok(Mapper.Map<BankModel>(entity));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<BankModel>> AddNewBank([FromBody] BankDto request,
            CancellationToken cancellationToken)
        {
            var dto = Mapper.Map<Bank>(request);


            var entity = await _bankService.Add(dto, cancellationToken);

            return Mapper.Map<BankModel>(entity);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:long}")]
        public async Task<ActionResult<BankModel>> EditBankById(long id,[FromBody] BankDto request, CancellationToken cancellationToken)
        {
            var dto = Mapper.Map<Bank>(request);
            dto.Version = Guid.Parse(HttpContext.Request.Headers.IfMatch);
            dto.Id = id;

            var updatedEntity = await _bankService.UpdateAsync(dto, cancellationToken);

            Response.Headers.ETag = updatedEntity.Version.ToString();
            return Mapper.Map<BankModel>(updatedEntity);
        }

        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult> DeleteBankById([FromRoute] long id, CancellationToken cancellationToken)
        {
            await _bankService.RemoveAsync(id, Guid.Parse(HttpContext.Request.Headers.IfMatch), cancellationToken);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
