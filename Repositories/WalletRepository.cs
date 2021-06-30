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

        public IEnumerable<Accaunt> GetAccounts(int id)
        {
            return _context.Accounts.Where(x => x.UserId == id).ToList();
        }

        public async Task<bool> Update(Accaunt accaunt)
        {
            _context.Accounts.Update(accaunt);
            await _context.SaveChangesAsync();
            return (await _context.SaveChangesAsync())>0;
        }

        public async Task<bool> Update(List<Accaunt> accaunt)
        {
            _context.Accounts.UpdateRange(accaunt);
            await _context.SaveChangesAsync();
            return (await _context.SaveChangesAsync()) > 0;
        }
       
    }
}
