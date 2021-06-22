using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWallet.Models;
using UserWallet.Repositories;

namespace UserWallet.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class OperationsController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;
        public OperationsController(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        [HttpGet("GetAccounts/{id}")]
        public async Task<IEnumerable<Accaunt>> GetAccounts([FromRoute]int id)
        {
            return await _walletRepository.GetAccounts(id);
        }

        [HttpPost("TransferMoney")]
        public async Task<string> TransferMoney([FromBody] RequestModel model)
        {
            if (ModelState.IsValid)
            {
                return await _walletRepository.TransferMoney(model);
            }
            return "Ошибка";
        }

        [HttpPost("WithDrawMoney")]
        public async Task<string> WithDrawMoney([FromBody] RequestModel model)
        {
            if (ModelState.IsValid)
            {
                return await _walletRepository.WithDrawMoney(model);
            }
            return "Ошибка";
        }

        [HttpPost("FillUp")]
        public async Task<string> FillUp([FromBody] RequestModel model)
        {
            if (ModelState.IsValid)
            {
                return await _walletRepository.FillUp(model);
            }
            return "Ошибка";
        }
       
    }
}
