using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using UserWallet.Domain;
using UserWallet.Models;
using UserWallet.Repositories;

namespace UserWallet.Services
{
    public class WalletServices:IWalletServices
    {
        IWalletRepository repo;
        public WalletServices(IWalletRepository repo)
        {
            this.repo = repo;
        }

        public async Task<string> FillUp(RequestModel model)
        {

            try
            {
                model.ToCurrency = model.ToCurrency.ToUpper();
               IEnumerable<Accaunt> accaunt =  repo.GetAccounts(model.UserID);
                
                Accaunt newAcc = accaunt.Where(x => x.UserId == model.UserID && x.CuurrencyCode == model.ToCurrency).FirstOrDefault();

                if (newAcc != null)
                {
                    newAcc.Amount += model.Amount;
                    await repo.Update(newAcc);
                    return $"Деньги добавлены. Ваш баланс: {newAcc.Amount} {newAcc.CurrencyName}";
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

                IEnumerable<Accaunt> accaunt = repo.GetAccounts(model.UserID);

                accaunt
                    .Where(x => x.UserId == model.UserID)
                    .Where(x => x.CuurrencyCode == model.FromCurrency || x.CuurrencyCode == model.ToCurrency)
                    .ToList();

                if (accaunt is null || accaunt.Count() < 2)
                {
                    return "У вас нет одной из этих валют, чтобы узнать ваши валюты обрашайтесь к методу GetAccounts(id)";
                }

                string url = $"https://www.cbar.az/currencies/{ DateTime.Now.ToString("dd.MM.yyyy")}.xml";

                using (var reader = new XmlTextReader(url))
                {
                    var serializer = new XmlSerializer(typeof(ValCurs));

                    var result = (ValCurs)serializer.Deserialize(reader);

                    Accaunt fromAccaunt = accaunt.Where(x => x.CuurrencyCode == model.FromCurrency).First();

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

                    Accaunt toAccaunt = accaunt.Where(x => x.CuurrencyCode == model.ToCurrency).First();
                    toAccaunt.Amount += toResult;

                    List<Accaunt> newList = new List<Accaunt>() { fromAccaunt, toAccaunt };
                    await repo.Update(newList);
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
            IEnumerable<Accaunt> accaunt = repo.GetAccounts(model.UserID);

            Accaunt newAcc = accaunt.Where(x => x.UserId == model.UserID && x.CuurrencyCode == model.FromCurrency).FirstOrDefault();
            if (newAcc != null)
            {
                if (newAcc.Amount < model.Amount)
                {
                    return $"У вас нет столько денег в этой валюте. Ваш баланс: {newAcc.Amount} {newAcc.CuurrencyCode}";
                }
                newAcc.Amount -= model.Amount;
                await repo.Update(newAcc);
                return $"Деньги сняты с счета. Ваш баланс: {newAcc.Amount} {newAcc.CurrencyName}";
            }
            else
            {
                return "У вас нет такого счета";
            }
        }
    }
}
