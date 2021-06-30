using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWallet.Models;
using UserWallet.Repositories;
using UserWallet.Services;

namespace UserWallet.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class OperationsController : ControllerBase
    {
        private readonly IWalletServices _walletServices;
        private readonly IWalletRepository _walletRepository;
        public OperationsController(IWalletServices walletServices, IWalletRepository walletRepository)
        {
            _walletServices = walletServices;
            _walletRepository = walletRepository;
        }

        [HttpGet("GetAccounts/{id}")]
        public IEnumerable<Accaunt> GetAccounts([FromRoute]int id)
        {
            return  _walletRepository.GetAccounts(id);
        }

        [HttpPost("TransferMoney")]
        public async Task<string> TransferMoney([FromBody] RequestModel model)
        {
            if (ModelState.IsValid)
            {
                return await _walletServices.TransferMoney(model);
            }
            return "Ошибка";
        }

        [HttpPost("WithDrawMoney")]
        public async Task<string> WithDrawMoney([FromBody] RequestModel model)
        {
            if (ModelState.IsValid)
            {
                return await _walletServices.WithDrawMoney(model);
            }
            return "Ошибка";
        }

        [HttpPost("FillUp")]
        public async Task<string> FillUp([FromBody] RequestModel model)
        {
            if (ModelState.IsValid)
            {
                return await _walletServices.FillUp(model);
            }
            return "Ошибка";
        }
       
    }
}
