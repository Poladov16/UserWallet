using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWallet.Models;

namespace UserWallet.Services
{
    public interface IWalletServices
    {

        Task<string> TransferMoney(RequestModel model);//Перевести деньги из одной валюты в другую

        Task<string> WithDrawMoney(RequestModel model);//Снять деньги в одной из валют

        Task<string> FillUp(RequestModel model);//Пополнить кошелек в одной из валют
    }
}
