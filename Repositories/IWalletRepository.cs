using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWallet.Models;

namespace UserWallet.Repositories
{
    public interface IWalletRepository
    {
        IEnumerable<Accaunt> GetAccounts(int id);//состояние своего кошелька

        Task<bool> Update(Accaunt accaunt);

        Task<bool> Update(List<Accaunt> accaunt);

    }
}
