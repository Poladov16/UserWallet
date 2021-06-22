using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWallet.Data;
using UserWallet.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserWallet.Services;
using System.Xml;
using System.Xml.Serialization;

namespace UserWallet.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private WalletContext _context;
        public WalletRepository(WalletContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Accaunt>> GetAccounts(int id)
        {
            return await _context.Accounts.Where(x => x.UserId == id).ToListAsync();
        }

        public async Task<string> FillUp(RequestModel model)
        {
            try
            {
                model.ToCurrency = model.ToCurrency.ToUpper();
                Accaunt accaunt = _context.Accounts.Where(x => x.UserId == model.UserID && x.CuurrencyCode == model.ToCurrency).FirstOrDefault();
                if (accaunt != null)
                {
                    //task= new Accaunt
                    // {
                    //     AccountId = task.AccountId,
                    //     AccountNumber = task.AccountNumber,
                    //     Amount = model.Amount,
                    //     CurrencyName = task.CurrencyName,
                    //     CuurencyCode = task.CuurencyCode,
                    //     IsActive = task.IsActive,
                    //     UserId = task.UserId
                    // };
                    accaunt.Amount += model.Amount;
                    _context.Accounts.Update(accaunt);
                    //_context.Entry(task).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return $"Деньги добавлены. Ваш баланс: {accaunt.Amount} {accaunt.CurrencyName}";

                }
                else
                {
                    return "У вас нет такого счета";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }

        public async Task<string> TransferMoney(RequestModel model)
        {
            if (model.FromCurrency == model.ToCurrency)
            {
                return "Это одна и та же валюта. Перевод невозможен!";
            }

            try
            {
                model.ToCurrency = model.ToCurrency.ToUpper();
                model.FromCurrency = model.FromCurrency.ToUpper();

                var userAccaunts = await _context.Accounts
                    .Where(x => x.UserId == model.UserID)
                    .Where(x => x.CuurrencyCode == model.FromCurrency || x.CuurrencyCode == model.ToCurrency)
                    .ToListAsync();

                if (userAccaunts is null || userAccaunts.Count < 2)
                {
                    return "У вас нет одной из этих валют, чтобы узнать ваши валюты обрашайтесь к методу GetAccounts(id)";
                }

                string url = $"https://www.cbar.az/currencies/{ DateTime.Now.ToString("dd.MM.yyyy")}.xml";

                using (var reader = new XmlTextReader(url))
                {
                    var serializer = new XmlSerializer(typeof(ValCurs));

                    var result = (ValCurs)serializer.Deserialize(reader);

                    Accaunt fromAccaunt = userAccaunts.Where(x => x.CuurrencyCode == model.FromCurrency).First();

                    if (model.Amount > fromAccaunt.Amount)
                    {
                        return $"У вас нет столько денег в этой валюте. Ваш баланс:{fromAccaunt.Amount} {fromAccaunt.CuurrencyCode}";
                    }

                    var bankValutes = result.ValType[1];

                    var from = bankValutes.Valute.Where(x => x.Code == model.FromCurrency).First();

                    float fromResult = from.Value * model.Amount;//get rate AZN amount

                    var to = bankValutes.Valute.Where(x => x.Code == model.ToCurrency).First();

                    float toResult = fromResult / to.Value;

                    fromAccaunt.Amount -= model.Amount;

                    Accaunt toAccaunt = userAccaunts.Where(x => x.CuurrencyCode == model.ToCurrency).First();
                    toAccaunt.Amount += toResult;

                    _context.UpdateRange(fromAccaunt, toAccaunt);

                    await _context.SaveChangesAsync();
                    return "Перевод успешно выполнен";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> WithDrawMoney(RequestModel model)
        {
            model.FromCurrency = model.FromCurrency.ToUpper();
            Accaunt accaunt = _context.Accounts.Where(x => x.UserId == model.UserID && x.CuurrencyCode == model.FromCurrency).FirstOrDefault();
            if (accaunt != null)
            {
                if (accaunt.Amount < model.Amount)
                {
                    return $"У вас нет столько денег в этой валюте. Ваш баланс: {accaunt.Amount} {accaunt.CuurrencyCode}";
                }
                accaunt.Amount -= model.Amount;
                _context.Accounts.Update(accaunt);
                //_context.Entry(task).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return $"Деньги сняты с счета. Ваш баланс: {accaunt.Amount} {accaunt.CurrencyName}";
            }
            else
            {
                return "У вас нет такого счета";
            }
        }
    }
}
